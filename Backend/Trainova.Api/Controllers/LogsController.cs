using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Trainova.Application.Common.Models;

namespace Trainova.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogsController : ApiController
    {
        private readonly LoggingOptions _loggingOptions;

        public LogsController(CurrentUser? currentUser, IOptions<LoggingOptions> loggingOptions) : base(currentUser)
        {
            _loggingOptions = loggingOptions.Value;
        }

        [HttpGet]
        public async Task<IActionResult> GetRemoteLogFile()
        {
            if (string.IsNullOrEmpty(_loggingOptions.DomainEventLogFilePath) || !System.IO.File.Exists(_loggingOptions.DomainEventLogFilePath))
            {
                return NotFound("Log file not found or path is empty.");
            }

            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(_loggingOptions.DomainEventLogFilePath);
            string fileName = Path.GetFileName(_loggingOptions.DomainEventLogFilePath);

            return base.File(fileBytes, "text/plain", fileName);
        }

        [HttpPost]
        public async Task<IActionResult> ResetRemoteLogFile()
        {
            if (string.IsNullOrEmpty(_loggingOptions.DomainEventLogFilePath) || !System.IO.File.Exists(_loggingOptions.DomainEventLogFilePath))
            {
                return NotFound("Log file not found or path is empty.");
            }

            using (var fs = new FileStream(_loggingOptions.DomainEventLogFilePath, FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite))
            {
                await fs.FlushAsync();
            }

            return NoContent();
        }
    }
}