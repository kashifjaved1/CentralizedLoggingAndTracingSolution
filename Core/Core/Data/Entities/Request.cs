namespace Core.Data.Entities
{
    public class Request : DefaultTenantEntity
    {
        public string ServiceName { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
        public string Headers { get; set; }
        public string Body { get; set; }
    }

}
