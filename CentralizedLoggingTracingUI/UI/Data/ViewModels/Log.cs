namespace UI.Data.ViewModels
{
    public class LogViewModel
    {
        public int Id { get; set; }
        public Guid RequestId { get; set; }
        public string Message { get; set; }
        public string LogLevel { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
