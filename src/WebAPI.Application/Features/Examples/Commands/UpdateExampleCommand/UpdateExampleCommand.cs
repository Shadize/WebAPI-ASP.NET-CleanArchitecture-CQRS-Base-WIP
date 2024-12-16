using FluentResults;
using MediatR;
using WebAPI.Application.Interfaces;


namespace WebAPI.Application.Features.Examples.Commands.UpdateExampleCommand
{
    public record UpdateExampleCommand(Guid Id, string Name, string Description) : IRequest<Result>, ICacheInvalidator
    {
        public string CacheKey => $"example:{Id}";

    }

}
