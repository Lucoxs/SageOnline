namespace API.Identity.Models
{
    public class Error
    {
        public string ErrorMessage { get; set; }
        public string? ErrorStackTrace { get; set; }

        public Error(Exception exception)
        {
            ErrorMessage = exception.Message;
            ErrorStackTrace = exception.StackTrace;
        }
    }
}
