namespace Core.Data.Entities
{
    public class Response : DefaultTenantEntity
    {
        public int StatusCode { get; set; }
        public string Headers { get; set; }
        public string Body { get; set; }
    }
}
