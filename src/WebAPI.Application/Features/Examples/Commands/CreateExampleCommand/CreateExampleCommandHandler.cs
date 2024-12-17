using FluentResults;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using WebAPI.Application.Interfaces;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Features.Examples.Commands.CreateExampleCommand
{
    public class CreateExampleCommandHandler : IRequestHandler<CreateExampleCommand, Result<Guid>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDistributedCache _distributedCache;


        public CreateExampleCommandHandler(IApplicationDbContext context, IDistributedCache distributedCache)
        {
            _context = context;
            _distributedCache = distributedCache;
        }

        public async Task<Result<Guid>> Handle(CreateExampleCommand request, CancellationToken cancellationToken)
        {
            var example = new Example(request.Name, request.Description);

            await _context.Examples.AddAsync(example);

            var result = await _context.SaveChangesAsync(cancellationToken);

            if(result <= 0)
                return Result.Fail<Guid>("Une erreur s'est produite lors de la sauvegarde des données.");

            await _distributedCache.RemoveAsync("all-examples"); 
            await _distributedCache.RemoveAsync($"example:{example.Id}");

            return Result.Ok(example.Id).WithSuccess("Example crée avec succès.");
        }
    }
}
