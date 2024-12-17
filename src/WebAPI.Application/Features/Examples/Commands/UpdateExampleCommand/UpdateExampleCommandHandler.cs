using FluentResults;
using MediatR;
using WebAPI.Application.Interfaces;

namespace WebAPI.Application.Features.Examples.Commands.UpdateExampleCommand
{
    public class UpdateExampleCommandHandler : IRequestHandler<UpdateExampleCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public UpdateExampleCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateExampleCommand request, CancellationToken cancellationToken)
        {
            var example = await _context.Examples.FindAsync(request.Id);

            if (example == null)
                return Result.Fail("Exemple nul");

            example.Name = request.Name;
            example.Description = request.Description;

            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result <= 0)
                return Result.Fail("Pas de résultat");

            return Result.Ok().WithSuccess("Example correctement modifier");
        }
    }
}
