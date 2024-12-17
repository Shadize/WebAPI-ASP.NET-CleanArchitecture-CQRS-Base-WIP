using FluentResults;
using MediatR;
using WebAPI.Application.Interfaces;

namespace WebAPI.Application.Features.Examples.Commands.DeleteExampleCommand
{
    public class DeleteExampleCommandHandler : IRequestHandler<DeleteExampleCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteExampleCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteExampleCommand request, CancellationToken cancellationToken)
        {
            var example = await _context.Examples.FindAsync(request.Id);

            if (example == null)
                return Result.Fail("Pas d'exemple");


            _context.Examples.Remove(example);

            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result <= 0)
                return Result.Fail("Pas d'exemple supprimé");

            return Result.Ok().WithSuccess("Example supprimé");
        }
    }
}
