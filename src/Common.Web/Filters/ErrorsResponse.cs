namespace StatementIQ.Common.Web.Filters
{
    public class ErrorsResponse
    {
        public decimal Code { get; set; }

        public string Message { get; set; }
        
        public dynamic Fields { get; set; }
    }
}