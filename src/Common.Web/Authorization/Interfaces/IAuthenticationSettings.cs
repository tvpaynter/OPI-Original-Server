namespace StatementIQ.Common.Web.Authorization.Interfaces
{
    public interface IAuthenticationSettings
    {
        string Password { get; }
        string User { get; }
    }
}