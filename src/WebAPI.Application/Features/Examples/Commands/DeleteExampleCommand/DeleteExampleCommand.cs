using MediatR;

namespace WebAPI.Application.Features.Examples.Commands.DeleteExampleCommand
{
    public record DeleteExampleCommand(Guid Id) : IRequest;

}
