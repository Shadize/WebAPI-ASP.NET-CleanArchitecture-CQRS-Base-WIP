 using MediatR;
using Microsoft.Extensions.Logging;

namespace WebAPI.Application.Features.Examples.Notifications
{
    public class RandomHandler(ILogger<RandomHandler> logger) : INotificationHandler<CreateExampleNotification>
    {
        public Task Handle(CreateExampleNotification notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"handling notification for product creation with id : {notification.Id}. performing random action.");
            return Task.CompletedTask;
        }
    }
}
