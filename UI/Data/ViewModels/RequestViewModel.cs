namespace UI.Data.ViewModels
{
    public class RequestViewModel
    {
        public int Id { get; set; }
        public Guid RequestId { get; set; }
        public string ServiceName { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
        public string Headers { get; set; }
        public string Body { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
