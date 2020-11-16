using Microsoft.AspNetCore.Mvc.Filters;
using ProductService.Core.Models;
using ProductService.Core.Services.Interfaces;
using System;
using System.IO;

namespace ProductService.API.Infrastructure
{
    public class ErrorLogFilter : IExceptionFilter
    {
        private readonly IElasticsearchService _elasticsearchService;
        private ErrorLogModel _errorLogModel;

        public ErrorLogFilter(IElasticsearchService elasticsearchService)
        {
            _elasticsearchService = elasticsearchService;
            _errorLogModel = new ErrorLogModel();
        }
        public async void OnException(ExceptionContext context)
        {
            _errorLogModel.Path = context.HttpContext.Request.Path;
            _errorLogModel.Method = context.HttpContext.Request.Method;
            _errorLogModel.QueryString = context.HttpContext.Request.QueryString.ToString();

            if (context.HttpContext.Request.Method != "GET")
            {
                context.HttpContext.Request.Body.Position = 0;

                using (StreamReader reader = new StreamReader(context.HttpContext.Request.Body))
                {
                    _errorLogModel.Payload = await reader.ReadToEndAsync();
                }
            }

            _errorLogModel.ErrorStack = context.Exception.StackTrace;
            _errorLogModel.ErrorMessage = context.Exception.Message;
            _errorLogModel.ErrorAt = DateTime.Now;

            _elasticsearchService.InsertErrorLog(_errorLogModel);
        }
    }
}
