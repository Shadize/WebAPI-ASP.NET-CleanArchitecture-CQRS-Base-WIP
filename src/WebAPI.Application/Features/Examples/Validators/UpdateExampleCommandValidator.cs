using FluentValidation;
using WebAPI.Application.Features.Examples.Commands.CreateExampleCommand;

namespace WebAPI.Application.Features.Examples.Validators
{
    public class UpdateExampleCommandValidator : AbstractValidator<CreateExampleCommand>
    {
        public UpdateExampleCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
        }
    }
}
