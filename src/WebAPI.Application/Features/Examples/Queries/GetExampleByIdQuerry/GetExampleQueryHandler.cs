using AutoMapper;
using FluentResults;
using MediatR;
using WebAPI.Application.Features.Examples.DTOs;
using WebAPI.Application.Interfaces;

namespace WebAPI.Application.Features.Examples.Queries.GetExampleByIdQuerry
{
    public class GetExampleQueryHandler : IRequestHandler<GetExampleQuery, Result<ExampleDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;


        public GetExampleQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<ExampleDTO>> Handle(GetExampleQuery request, CancellationToken cancellationToken)
        {
            var example = await _context.Examples.FindAsync(request.Id);

            if (example == null)
                return Result.Fail("Pas d'exemple");

            var mapeedExample = _mapper.Map<ExampleDTO>(example);

            return Result.Ok(mapeedExample);
        }
    }
}
