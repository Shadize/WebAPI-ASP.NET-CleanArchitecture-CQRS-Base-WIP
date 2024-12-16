using FluentResults;
using MediatR;
using WebAPI.Application.Features.Examples.DTOs;
using WebAPI.Application.Interfaces;

namespace WebAPI.Application.Features.Examples.Queries.GetAllExamplesQuerry
{
    public record ListExamplesQuery(bool BypassCache = false) : IRequest<Result<List<ExampleDTO>>>, ICacheable
    {
        public string CacheKey => $"all-examples";

        public int SlidingExpirationInMinutes => 30;

        public int AbsoluteExpirationInMinutes => 60;
    }
}
