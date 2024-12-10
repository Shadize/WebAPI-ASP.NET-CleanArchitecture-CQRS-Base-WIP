using MediatR;
using WebAPI.Application.Features.Examples.DTOs;

namespace WebAPI.Application.Features.Examples.Queries.GetExampleByIdQuerry
{
    public record GetExampleQuery(Guid Id) : IRequest<ExampleDTO>;
}
