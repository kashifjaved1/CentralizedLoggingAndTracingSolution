namespace UI.Data.ViewModels
{
    public class ResponseViewModel
    {
        public int Id { get; set; }
        public Guid RequestId { get; set; }
        public int StatusCode { get; set; }
        public string Headers { get; set; }
        public string Body { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
