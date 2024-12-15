
using AutoMapper;
using FluentResults;
using MediatR;
using WebAPI.Application.Features.Examples.DTOs;
using WebAPI.Domain.Interfaces;

namespace WebAPI.Application.Features.Examples.Queries.GetAllExamplesQuerry
{
    public class ListExamplesQueryHandler : IRequestHandler<ListExamplesQuery, Result<List<ExampleDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ListExamplesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<ExampleDTO>>> Handle(ListExamplesQuery request, CancellationToken cancellationToken)
        {
            var examples = await _unitOfWork.Examples.GetAllAsync();

            if (examples == null || !examples.Any())
                return Result.Fail<List<ExampleDTO>>("Aucun exemple n'a été trouvé.");

            var mappedExamples = _mapper.Map<List<ExampleDTO>>(examples);

            return Result.Ok(mappedExamples);

        }
    }
}
