using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Enums;

namespace Trainova.Application.Profiles.Commands.UpdatePlayerProfile;

public record UpdatePlayerProfileCommand(
    Guid PlayerId,
    string? ShowName,
    string? FullName,
    string? PhotoPath,
    string? Email,
    int? PlayerNumber,
    string? TShirtName,
    PlayerMedicalStatus? MedicalStatus,
    Position? CurrentMainPosition,
    Position? OtherAvailablePositions,
    decimal? PerformanceLevel
) : IRequest<ResultOf<object>>;
