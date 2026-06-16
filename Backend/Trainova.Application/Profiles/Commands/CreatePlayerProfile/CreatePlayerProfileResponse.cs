using System;
using Trainova.Domain.Common.Enums;

namespace Trainova.Application.Profiles.Commands.CreatePlayerProfile;

public record CreatePlayerProfileResponse(
    Guid Id,
    string ShowName,
    string FullName,
    int PlayerNumber,
    string TShirtName,
    Position CurrentMainPosition
);
