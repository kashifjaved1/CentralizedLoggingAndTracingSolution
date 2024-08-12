namespace Core.Data.Entities
{
    public class Metric : DefaultTenantEntity
    {
        public string ServiceName { get; set; }
        public string MetricName { get; set; }
        public string Value { get; set; }
    }
}
