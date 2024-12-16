using MediatR;
using FluentResults;
using WebAPI.Application.Interfaces;

namespace WebAPI.Application.Features.Examples.Commands.CreateExampleCommand
{
    public record CreateExampleCommand(string Name, string Description) : IRequest<Result<Guid>>, ICacheInvalidator
    {
        public string CacheKey => $"all-examples";
    }

}
