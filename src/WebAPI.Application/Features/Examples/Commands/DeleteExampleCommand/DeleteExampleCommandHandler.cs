using MediatR;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces;

namespace WebAPI.Application.Features.Examples.Commands.DeleteExampleCommand
{
    public class DeleteExampleCommandHandler : IRequestHandler<DeleteExampleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteExampleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteExampleCommand request, CancellationToken cancellationToken)
        {
            var example = await _unitOfWork.Examples.GetByIdAsync(request.Id);

            if (example == null) return;
            await _unitOfWork.Examples.DeleteAsync(example);
            await _unitOfWork.CompleteAsync();
        }
    }
}
