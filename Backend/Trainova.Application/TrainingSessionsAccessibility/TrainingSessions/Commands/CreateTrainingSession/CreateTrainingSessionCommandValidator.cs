using FluentValidation;

namespace Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Commands.CreateTrainingSession
{
    public class CreateTrainingSessionCommandValidator : AbstractValidator<CreateTrainingSessionCommand>
    {
        public CreateTrainingSessionCommandValidator()
        {
            RuleFor(x => x.SessionName).NotEmpty().WithMessage("Session name is required.");
            RuleFor(x => x.PlanState).IsInEnum().WithMessage("Invalid plan state.");
            RuleFor(x => x.UserIds).NotNull().WithMessage("User IDs list cannot be null.");
            RuleForEach(x => x.UserIds).NotEmpty().WithMessage("User ID cannot be empty.");
            RuleFor(x => x.Place).MaximumLength(200).WithMessage("Place cannot exceed 200 characters.");
            RuleFor(x => x.WillHappenAt).GreaterThan(DateTime.UtcNow).WithMessage("HappenedAt cannot be in the past.");

        }
    }



}
