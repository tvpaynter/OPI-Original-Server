namespace StatementIQ.Common.ElasticSearch.Interfaces
{
    public interface IElasticSearchSettings
    {
        string EndpointUris { get; }
        ConnectionPoolType ConnectionPoolType { get; }
        int RequestTimeout { get; }
    }
}