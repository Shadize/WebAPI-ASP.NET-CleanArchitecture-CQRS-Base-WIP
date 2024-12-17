using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAPI.Application.Features.Examples.DTOs;
using WebAPI.Application.Interfaces;

namespace WebAPI.Application.Features.Examples.Queries.GetAllExamplesQuerry
{
    public class ListExamplesQueryHandler : IRequestHandler<ListExamplesQuery, Result<List<ExampleDTO>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;


        public ListExamplesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<ExampleDTO>>> Handle(ListExamplesQuery request, CancellationToken cancellationToken)
        {
            var examples = await _context.Examples
                .Select(e => new ExampleDTO(e.Id, e.Name, e.Description))
                .ToListAsync();

            if (examples == null || !examples.Any())
                return Result.Fail<List<ExampleDTO>>("Aucun exemple n'a été trouvé.");

            var mappedExamples = _mapper.Map<List<ExampleDTO>>(examples);

            return Result.Ok(mappedExamples);

        }
    }
}
