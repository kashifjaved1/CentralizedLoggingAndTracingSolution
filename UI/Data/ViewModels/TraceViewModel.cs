namespace UI.Data.ViewModels
{
    public class TraceViewModel
    {
        public int Id { get; set; }
        public Guid RequestId { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
