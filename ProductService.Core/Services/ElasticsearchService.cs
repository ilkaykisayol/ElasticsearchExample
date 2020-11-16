using Nest;
using ProductService.Core.Enums;
using ProductService.Core.Models;
using ProductService.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Core.Services
{
    public class ElasticsearchService : IElasticsearchService
    {
        private readonly IElasticClient _client;
        public ElasticsearchService(IElasticClient client)
        {
            _client = client;
        }

        public void InsertActionLog(ActionLogModel actionLogModel)
        {
            string indexName = LogIndexType.action_log.ToString();
            if (!_client.Indices.Exists(indexName).Exists)
            {
                _client.Indices.Create(indexName, x =>
                     x.Map<ActionLogModel>(m => m.AutoMap())
                     .Aliases(a => a.Alias(indexName))
                    );
            }

            _client.Index<ActionLogModel>(actionLogModel, x => x.Index(indexName));
        }

        public void InsertErrorLog(ErrorLogModel errorLogModel)
        {
            string indexName = LogIndexType.error_log.ToString();
            if (!_client.Indices.Exists(indexName).Exists)
            {
                _client.Indices.Create(indexName);
            }

            _client.Index<ErrorLogModel>(errorLogModel, x => x.Index(indexName));
        }
    }
}
