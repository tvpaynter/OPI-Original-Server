using System;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using StatementIQ.Common.Autofac;
using StatementIQ.Common.ElasticSearch.Interfaces;

namespace StatementIQ.Common.ElasticSearch
{
    [SingleInstance]
    public class ElasticClientFactory : IElasticClientFactory
    {
        private readonly IElasticSearchSettings _settings;

        public ElasticClientFactory(IElasticSearchSettings settings)
        {
            _settings = settings;
        }
        public IElasticClient Create()
        {
            var connectionSettings = CreateConnectionSettings(
                _settings,
                TimeSpan.FromSeconds(_settings.RequestTimeout));
            var result = new ElasticClient(connectionSettings);
            return result;
        }
        

        private static ConnectionSettings CreateConnectionSettings(
            IElasticSearchSettings settings,
            TimeSpan requestTimeout)
        {
            var pool = GetEsConnectionPool(settings);
            var connectionSettings = new ConnectionSettings(pool)
                .RequestTimeout(requestTimeout).DisableDirectStreaming()
                .EnableHttpCompression();
            return connectionSettings;
        }

        private static IConnectionPool GetEsConnectionPool(IElasticSearchSettings settings)
        {
            IConnectionPool pool;
            switch (settings.ConnectionPoolType)
            {
                default:
                    pool = new SingleNodeConnectionPool(new Uri(settings.EndpointUris));
                    break;
                case ConnectionPoolType.Static:
                    var uris = settings
                        .EndpointUris
                        .Split(';')
                        .Select(uri => new Uri(uri));
                    pool = new StaticConnectionPool(uris);
                    break;
            }
            return pool;
        }


    }
;
}