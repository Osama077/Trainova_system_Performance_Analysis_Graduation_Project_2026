using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using Trainova.Application.Common.Models;
using Trainova.Domain.Common.BaseEntity;
using Trainova.Infrastructure.DataAccess;

namespace Trainova.Bootstrapper.BackgroundServises
{
    public class DomainEventOutboxProcessorBackgroundService : BackgroundService
    {
        private readonly TimeSpan _delay = TimeSpan.FromMinutes(30);
        private readonly IServiceProvider _serviceProvider;
        private readonly string _logPath;

        public DomainEventOutboxProcessorBackgroundService(IServiceProvider serviceProvider, LoggingOptions loggingOptions)
        {
            _serviceProvider = serviceProvider;
            _logPath = loggingOptions.DomainEventLogFilePath;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<TrainovaWriteDbContext>();
                        var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

                        var outboxMessages = await dbContext.DomainEventOutboxes
                            .Where(m => !m.IsHandled)
                            .Take(50)
                            .ToListAsync(stoppingToken);

                        if (outboxMessages.Any())
                        {
                            await dbContext.StartTransactionAsync();
                            foreach (var message in outboxMessages)
                            {
                                try
                                {
                                    var eventType = Type.GetType(message.EventType);
                                    if (eventType == null)
                                    {
                                        // Try to get from loaded assemblies if Type.GetType fails (e.g. not in same assembly)
                                        eventType = AppDomain.CurrentDomain.GetAssemblies()
                                            .Select(a => a.GetType(message.EventType))
                                            .FirstOrDefault(t => t != null);
                                    }

                                    if (eventType != null)
                                    {
                                        var domainEvent = JsonSerializer.Deserialize(message.Notification, eventType) as IDomainEvent;
                                        if (domainEvent != null)
                                        {
                                            await publisher.Publish(domainEvent, stoppingToken);
                                        }
                                    }

                                    message.MarkAsHandled();
                                }
                                catch (Exception ex)
                                {
                                    message.MarkAsFailed(ex.Message);
                                    await LogExceptionAsJsonAsync(ex, message.Id);
                                }
                                finally
                                {
                                    dbContext.DomainEventOutboxes.Update(message);
                                    await dbContext.SaveChangesAsync(stoppingToken);
                                }
                            }


                            await dbContext.CommitTransactionAsync();

                        }
                    }
                }
                catch (Exception ex)
                {
                    await LogExceptionAsJsonAsync(ex);
                }

                await Task.Delay(_delay, stoppingToken);
            }

        }

        private async Task LogExceptionAsJsonAsync(Exception ex, Guid? eventId = null)
        {
            try
            {
                var logEntry = new
                {
                    Timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                    Service = nameof(DomainEventOutboxProcessorBackgroundService),
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Id = (Guid?)null,
                    ActorId = (Guid?)null,
                    InnerException = ex.InnerException?.Message
                };

                var jsonLog = JsonSerializer.Serialize(logEntry, new JsonSerializerOptions { WriteIndented = true });

                var directory = Path.GetDirectoryName(_logPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var txt = jsonLog
                    + Environment.NewLine
                    + "=================================================================="
                    + Environment.NewLine
                    + (eventId.HasValue ? $"EventId: {eventId}" : "EventId: N/A")
                    + Environment.NewLine
                    + "=================================================================="
                    + Environment.NewLine;

                await File.AppendAllTextAsync(_logPath, txt);
            }
            catch
            {
            }
        }

    }
}
