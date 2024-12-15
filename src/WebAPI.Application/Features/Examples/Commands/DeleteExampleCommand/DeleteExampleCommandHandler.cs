using FluentResults;
using MediatR;
using WebAPI.Domain.Interfaces;

namespace WebAPI.Application.Features.Examples.Commands.DeleteExampleCommand
{
    public class DeleteExampleCommandHandler : IRequestHandler<DeleteExampleCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteExampleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteExampleCommand request, CancellationToken cancellationToken)
        {
            var example = await _unitOfWork.Examples.GetByIdAsync(request.Id);
            if (example == null)
                return Result.Fail("Pas d'exemple");


            await _unitOfWork.Examples.DeleteAsync(example);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0)
                return Result.Fail("Pas d'exemple supprimé");

            return Result.Ok().WithSuccess("Example supprimé");
        }
    }
}
