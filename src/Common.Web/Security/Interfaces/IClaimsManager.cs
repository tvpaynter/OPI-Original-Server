namespace StatementIQ.Common.Web.Security.Interfaces
{
    public interface IClaimsManager
    {
        long GetCurrentUserId();
        long GetCurrentUserHierarchyId();
        long GetCurrentSessionId();
        string GetCurrentAuthorizationToken();
    }
}