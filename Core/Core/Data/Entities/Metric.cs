namespace Core.Data.Entities
{
    public class Metric : DefaultEntity
    {
        public string ServiceName { get; set; }
        public string MetricName { get; set; }
        public string Value { get; set; }
    }
}
