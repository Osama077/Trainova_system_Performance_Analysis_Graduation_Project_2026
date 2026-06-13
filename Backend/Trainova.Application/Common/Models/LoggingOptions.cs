namespace Trainova.Application.Common.Models
{
    public class LoggingOptions
    {
        public string LogFilePath { get; set; }
        public string DomainEventLogFilePath { get; set; }
        public string RequestResponseLogFilePath => DomainEventLogFilePath;
        public string AuthorizationLogFilePath => DomainEventLogFilePath;
    }
}
