
using AutoMapper;
using MediatR;
using WebAPI.Application.Features.Examples.DTOs;
using WebAPI.Domain.Interfaces;

namespace WebAPI.Application.Features.Examples.Queries.GetExampleByIdQuerry
{
    public class GetExampleQueryHandler : IRequestHandler<GetExampleQuery, ExampleDTO?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public GetExampleQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ExampleDTO?> Handle(GetExampleQuery request, CancellationToken cancellationToken)
        {
            var example = await _unitOfWork.Examples.GetByIdAsync(request.Id);
            if(example == null)
            {
                return null;
            }
            
            return _mapper.Map<ExampleDTO>(example);
        }
    }
}
