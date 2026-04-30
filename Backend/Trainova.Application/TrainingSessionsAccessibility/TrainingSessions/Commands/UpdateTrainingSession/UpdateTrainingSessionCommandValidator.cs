using FluentValidation;

namespace Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Commands.UpdateTrainingSession
{
    public class UpdateTrainingSessionCommandValidator : AbstractValidator<UpdateTrainingSessionCommand>
    {
        public UpdateTrainingSessionCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.SessionName).MaximumLength(200).When(x => x.SessionName != null);
            RuleFor(x => x.Place).MaximumLength(200).When(x => x.Place != null);
            RuleFor(x => x.WillHappenAt).GreaterThan(DateTime.UtcNow).When(x => x.WillHappenAt.HasValue);
        }
    }
}
