using Microsoft.Extensions.Configuration;
using StatementIQ.Common.Autofac;
using StatementIQ.Common.ElasticSearch.Interfaces;
using StatementIQ.Common.ElasticSearch.Tools;

namespace StatementIQ.Common.ElasticSearch
{
    [SingleInstance]
    public class ElasticSearchSettings : IElasticSearchSettings
    {
        private readonly IConfiguration _config;

        public ElasticSearchSettings(IConfiguration config)
        {
            _config = config;
        }
        
        public string EndpointUris => _config["AppSettings:ES:EndpointUris"];
        public ConnectionPoolType ConnectionPoolType =>EnumUtilities.GetEnum<ConnectionPoolType>(_config["AppSettings:ES:ConnectionPoolType"]) ?? ConnectionPoolType.Single ;
        public int RequestTimeout => Converter.ConvertTo(_config["AppSettings:ES:RequestTimeout"], 15);
    }
}