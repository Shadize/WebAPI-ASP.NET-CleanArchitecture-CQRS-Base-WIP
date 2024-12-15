using FluentResults;
using MediatR;
using WebAPI.Application.Features.Examples.DTOs;
using WebAPI.Application.Interfaces;

namespace WebAPI.Application.Features.Examples.Queries.GetAllExamplesQuerry
{
    public record ListExamplesQuery : IRequest<Result<List<ExampleDTO>>>, ICacheable
    {
        public bool BypassCache => false;

        public string CacheKey => $"all-examples";

        public int SlidingExpirationInMinutes => 30;

        public int AbsoluteExpirationInMinutes => 60;
    }
}
