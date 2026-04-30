﻿using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.TrainingSessionsAccessibility.TrainingSessions;

namespace Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Commands.CreateTrainingSession
{
    [Authorize(Role = "HeadCoach,AssistantCoach,SystemAdmin")]
    public record CreateTrainingSessionCommand(
        string SessionName,
        Guid? PolicyId,
        PlanState PlanState,
        string? Place,
        DateTime? WillHappenAt,
        Guid? PlanId,
        List<Guid> UserIds)
        : IRequest<ResultOf<TrainingSession>>;



}
