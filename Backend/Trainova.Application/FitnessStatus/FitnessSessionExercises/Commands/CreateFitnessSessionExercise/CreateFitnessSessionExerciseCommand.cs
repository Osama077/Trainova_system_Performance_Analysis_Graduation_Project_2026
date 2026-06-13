using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.FitnessStatus;
using Trainova.Domain.FitnessStatus.Enums;

namespace Trainova.Application.FitnessStatus.FitnessSessionExercises.Commands.CreateFitnessSessionExercise
{
    // ==========================================
    // 1. CREATE OPERATION (Command, Handler, Validator)
    // ==========================================

    [Authorize(Roles = "FitnessCoach")]
    public record CreateFitnessSessionExerciseCommand(
        Guid SessionId,
        Guid ExerciseId,
        ExerciseIntensity? Intensity = null, // بقت Nullable عشان تسحب الـ Default لو متبعتتش
        int? Sets = null,
        string? RepsOrDuration = null, // تم تعديلها لـ string لتتوافق مع الموديل المحدث
        int? RestTimeSec = null,
        string? LoadDetails = null,
        int? Rounds = null,
        int? ActiveTimeSec = null) : IRequest<ResultOf<FitnessSessionExercise>>;
}
