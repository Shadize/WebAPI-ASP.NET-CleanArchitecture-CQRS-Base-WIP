using FluentResults;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces;

namespace WebAPI.Application.Features.Examples.Commands.CreateExampleCommand
{
    public class CreateExampleCommandHandler : IRequestHandler<CreateExampleCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IDistributedCache _distributedCache;

        public CreateExampleCommandHandler(IUnitOfWork unitOfWork, IDistributedCache distributedCache)
        {
            _unitOfWork = unitOfWork;
            _distributedCache = distributedCache;
        }

        public async Task<Result<Guid>> Handle(CreateExampleCommand request, CancellationToken cancellationToken)
        {
            var example = new Example(request.Name, request.Description);
            await _unitOfWork.Examples.AddAsync(example);
            var result = await _unitOfWork.CompleteAsync();
            if(result <= 0)
            {
                return Result.Fail<Guid>("Une erreur s'est produite lors de la sauvegarde des données.");
            }

            await _distributedCache.RemoveAsync("all-examples"); // Assuming you cache the "GetAllExamples"
            await _distributedCache.RemoveAsync($"example:{example.Id}"); // Remove cache for GetById


            return Result.Ok(example.Id).WithSuccess("Example crée avec succès.");
        }
    }
}
