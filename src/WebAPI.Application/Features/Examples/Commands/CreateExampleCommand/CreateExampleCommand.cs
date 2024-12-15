using MediatR;
using FluentResults;

namespace WebAPI.Application.Features.Examples.Commands.CreateExampleCommand
{
    public record CreateExampleCommand(string Name, string Description) : IRequest<Result<Guid>>;

}
