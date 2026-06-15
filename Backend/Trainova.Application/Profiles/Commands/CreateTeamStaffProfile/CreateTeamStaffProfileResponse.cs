using System;
using Trainova.Domain.Profiles;

namespace Trainova.Application.Profiles.Commands.CreateTeamStaffProfile;

public record CreateTeamStaffProfileResponse(
    Guid Id,
    string ShowName,
    string FullName,
    TeamStaffRole Role
);
