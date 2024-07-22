namespace UI.Data.ViewModels
{
    public class MetricViewModel
    {
        public int Id { get; set; }
        public Guid RequestId { get; set; }
        public string ServiceName { get; set; }
        public string MetricName { get; set; }
        public string Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
