using FluentResults;
using MediatR;
using WebAPI.Application.Interfaces;

namespace WebAPI.Application.Features.Examples.Commands.DeleteExampleCommand
{
    public record DeleteExampleCommand(Guid Id) : IRequest<Result>, ICacheInvalidator
    {
        public string CacheKey => $"example:{Id}";

    }

}
