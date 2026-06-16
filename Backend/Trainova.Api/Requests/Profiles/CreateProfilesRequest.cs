using MediatR;
using Trainova.Application.Profiles.Commands.CreatePlayerProfile;
using Trainova.Application.Profiles.Commands.CreateTeamStaffProfile;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.Profiles;

namespace Trainova.Api.Requests.Profiles
{
    public class CreatePlayerProfilesRequest
    {
        public string ShowName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? PhotoPath { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;

        public int PlayerNumber { get; set; }
        public string TShirtName { get; set; }
        public PlayerMedicalStatus MedicalStatus { get; set; } = PlayerMedicalStatus.Fit;
        public Position CurrentMainPosition { get; set; }
        public Position OtherAvailablePositions { get; set; }
        public decimal PerformanceLevel { get; set; }
        public DateOnly DateOfEnrolment { get; set; } = DateOnly.FromDateTime(DateTime.Now);


        public IRequest<ResultOf<CreatePlayerProfileResponse>> ToCommand()
        {
            return new CreatePlayerProfileCommand(
                ShowName,
                FullName,
                PhotoPath,
                Email,
                Password,
                PlayerNumber,
                TShirtName,
                MedicalStatus,
                CurrentMainPosition,
                OtherAvailablePositions,
                PerformanceLevel,
                DateOfEnrolment
            );
        }
    }
    public class CreateTeamStaffProfilesRequest
    {
        public string ShowName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? PhotoPath { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string? InsuranceFilesLink { get; set; }
        public string? ContractFilesLink { get; set; }
        public TeamStaffRole Role { get; set; }



        public IRequest<ResultOf<CreateTeamStaffProfileResponse>> ToCommand()
        {
            return new CreateTeamStaffProfileCommand(
                ShowName,
                FullName,
                PhotoPath,
                Email,
                Password,
                InsuranceFilesLink,
                ContractFilesLink,
                Role
            );
        }
    }
}
