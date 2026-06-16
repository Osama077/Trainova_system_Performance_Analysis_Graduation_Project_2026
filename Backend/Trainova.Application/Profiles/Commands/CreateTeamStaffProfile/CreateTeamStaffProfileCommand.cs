using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Domain.Profiles;

namespace Trainova.Application.Profiles.Commands.CreateTeamStaffProfile;

public record CreateTeamStaffProfileCommand(
    string ShowName,
    string FullName,
    string? PhotoPath,
    string? Email,
    string Password,
    string? InsuranceFilesLink,
    string? ContractFilesLink,
    TeamStaffRole Role
) : IRequest<ResultOf<CreateTeamStaffProfileResponse>>;
