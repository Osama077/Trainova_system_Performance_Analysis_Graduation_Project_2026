using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Enums;

namespace Trainova.Application.Profiles.Commands.CreatePlayerProfile;

public record CreatePlayerProfileCommand(
    string ShowName,
    string FullName,
    string? PhotoPath,
    string? Email,
    string Password,
    int PlayerNumber,
    string TShirtName,
    PlayerMedicalStatus MedicalStatus,
    Position CurrentMainPosition,
    Position OtherAvailablePositions,
    decimal PerformanceLevel,
    DateOnly DateOfEnrolment
) : IRequest<ResultOf<CreatePlayerProfileResponse>>;
