using FluentValidation;

namespace Trainova.Application.TrainingSessionsAccessibility.Plans.Commands.DeletePlan
{
    public class DeletePlanCommandValidator : AbstractValidator<DeletePlanCommand>
    {
        public DeletePlanCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
