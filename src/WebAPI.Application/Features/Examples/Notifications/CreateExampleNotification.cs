using MediatR;

namespace WebAPI.Application.Features.Examples.Notifications
{
    public record CreateExampleNotification(Guid Id) : INotification;
}
