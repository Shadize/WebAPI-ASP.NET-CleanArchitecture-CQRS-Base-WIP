using FluentResults;
using MediatR;
using WebAPI.Domain.Interfaces;

namespace WebAPI.Application.Features.Examples.Commands.UpdateExampleCommand
{
    public class UpdateExampleCommandHandler : IRequestHandler<UpdateExampleCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateExampleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateExampleCommand request, CancellationToken cancellationToken)
        {
            var example = await _unitOfWork.Examples.GetByIdAsync(request.Id);

            if (example == null)
                return Result.Fail("Exemple nul");

            example.Name = request.Name;
            example.Description = request.Description;
            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                return Result.Fail("Pas de résultat");

            return Result.Ok().WithSuccess("Example correctement modifier");
        }
    }
}
