namespace Core.Data.Entities
{
    public class Log : DefaultTenantEntity
    {
        public string ServiceName { get; set; }
        public string Message { get; set; }
        public string LogLevel { get; set; }
    }
}
