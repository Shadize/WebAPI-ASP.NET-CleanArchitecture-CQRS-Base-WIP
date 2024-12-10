using MediatR;
using WebAPI.Application.Features.Examples.DTOs;

namespace WebAPI.Application.Features.Examples.Queries.GetAllExamplesQuerry
{
    public record ListExamplesQuery : IRequest<List<ExampleDTO>>;
}
