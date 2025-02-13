namespace StatementIQ.Common.Web.Models
{
    public class DefaultCreatedResponse<T>
    {
        public DefaultCreatedResponse(T id)
        {
            Id = id;
        }

        public DefaultCreatedResponse()
        {
        }

        public T Id { get; set; }
    }
}