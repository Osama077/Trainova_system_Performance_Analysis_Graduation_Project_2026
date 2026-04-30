using FluentValidation;

namespace Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Commands.DeleteTrainingSession
{
    public class DeleteTrainingSessionCommandValidator : AbstractValidator<DeleteTrainingSessionCommand>
    {
        public DeleteTrainingSessionCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
