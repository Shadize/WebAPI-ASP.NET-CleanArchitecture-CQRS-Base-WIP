using MediatR;


namespace WebAPI.Application.Features.Examples.Commands.UpdateExampleCommand
{
    public record UpdateExampleCommand(Guid Id, string Name, string Description) : IRequest;

}
