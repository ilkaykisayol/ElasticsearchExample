using Microsoft.AspNetCore.Mvc.Filters;
using ProductService.Core.Models;
using ProductService.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.API.Infrastructure
{
    public class ActionLogFilter : IActionFilter
    {
        private readonly IElasticsearchService _elasticsearchService;
        private readonly Stopwatch _stopwatch;
        private ActionLogModel _actionLogModel;

        public ActionLogFilter(IElasticsearchService elasticsearchService)
        {
            _elasticsearchService = elasticsearchService;
            _actionLogModel = new ActionLogModel();
            _stopwatch = new Stopwatch();
        }
        public async void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch.Start();

            _actionLogModel.Path = context.HttpContext.Request.Path;
            _actionLogModel.Method = context.HttpContext.Request.Method;
            _actionLogModel.QueryString = context.HttpContext.Request.QueryString.ToString();

            if (context.HttpContext.Request.Method != "GET")
            {
                context.HttpContext.Request.Body.Position = 0;

                using (StreamReader reader = new StreamReader(context.HttpContext.Request.Body))
                {
                    _actionLogModel.Payload = await reader.ReadToEndAsync();
                }
            }

            _actionLogModel.RequestedAt = DateTime.Now;
        }
        public async void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();
            var elapsed = _stopwatch.Elapsed.ToString();

            string responseBody = "";

            if (context.Result != null)
            {
                var result = (ObjectResult)context.Result;

                responseBody = JsonSerializer.Serialize(result.Value); 
            }

            _actionLogModel.Response = responseBody;
            _actionLogModel.ResponseCode = context.HttpContext.Response.StatusCode.ToString();
            _actionLogModel.HttpStatusCode = context.HttpContext.Response.StatusCode;
            _actionLogModel.RespondedAt = DateTime.Now;
            _actionLogModel.ResponseTime = elapsed;

            _elasticsearchService.InsertActionLog(_actionLogModel);
        }
    }
}
