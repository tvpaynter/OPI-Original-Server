namespace StatementIQ.Common.Web.Managers
{
    public interface ITraceIdentifierProvider
    {
        string GetRequestIdentifier();
    }
}