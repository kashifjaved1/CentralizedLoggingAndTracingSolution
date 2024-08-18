namespace UI.Data.ViewModels
{
    public class LogDetailViewModel
    {
        //public List<LogViewModel> Logs { get; set; } = null;
        public LogViewModel Log { get; set; } = null;
        public List<TraceViewModel> Traces { get; set; }
    }
}
