
using AutoMapper;
using MediatR;
using WebAPI.Application.Features.Examples.DTOs;
using WebAPI.Domain.Interfaces;

namespace WebAPI.Application.Features.Examples.Queries.GetAllExamplesQuerry
{
    public class ListExamplesQueryHandler : IRequestHandler<ListExamplesQuery, List<ExampleDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ListExamplesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ExampleDTO>> Handle(ListExamplesQuery request, CancellationToken cancellationToken)
        {
            var examples = await _unitOfWork.Examples.GetAllAsync();

            return _mapper.Map<List<ExampleDTO>>(examples);
        }
    }
}
