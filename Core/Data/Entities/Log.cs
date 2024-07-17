namespace Core.Data.Entities
{
    public class Log : DefaultEntity
    {
        public string ServiceName { get; set; }
        public string Message { get; set; }
        public string LogLevel { get; set; }
    }
}
