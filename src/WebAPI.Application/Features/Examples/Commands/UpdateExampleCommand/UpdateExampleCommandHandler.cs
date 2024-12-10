

using MediatR;
using System.Reflection.Metadata;
using WebAPI.Domain.Interfaces;

namespace WebAPI.Application.Features.Examples.Commands.UpdateExampleCommand
{
    public class UpdateExampleCommandHandler : IRequestHandler<UpdateExampleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateExampleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateExampleCommand request, CancellationToken cancellationToken)
        {
            var example = await _unitOfWork.Examples.GetByIdAsync(request.Id);
            if (example == null) return;
            example.Name = request.Name;
            example.Description = request.Description;
            await _unitOfWork.CompleteAsync();
        }
    }
}
