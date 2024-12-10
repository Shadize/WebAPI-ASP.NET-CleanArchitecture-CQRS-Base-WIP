using MediatR;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces;

namespace WebAPI.Application.Features.Examples.Commands.CreateExampleCommand
{
    public class CreateExampleCommandHandler : IRequestHandler<CreateExampleCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateExampleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateExampleCommand request, CancellationToken cancellationToken)
        {
            var example = new Example(request.Name, request.Description);
            await _unitOfWork.Examples.AddAsync(example);
            await _unitOfWork.CompleteAsync();

            return example.Id;
        }
    }
}
