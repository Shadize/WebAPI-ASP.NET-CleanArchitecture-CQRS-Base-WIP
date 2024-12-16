using FluentResults;
using MediatR;
using WebAPI.Application.Features.Examples.DTOs;
using WebAPI.Application.Interfaces;

namespace WebAPI.Application.Features.Examples.Queries.GetExampleByIdQuerry
{
    public record GetExampleQuery(Guid Id, bool BypassCache = false) : IRequest<Result<ExampleDTO>>, ICacheable
    {
        public string CacheKey => $"example:{Id}";
        public int SlidingExpirationInMinutes => 30;
        public int AbsoluteExpirationInMinutes => 60;
    }

}
