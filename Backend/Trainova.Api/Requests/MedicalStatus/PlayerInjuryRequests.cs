using Trainova.Application.MedicalStatus.PlayerInjuries.Commands.CreatePlayerInjury;
using Trainova.Application.MedicalStatus.PlayerInjuries.Commands.UpdatePlayerInjury;
using Trainova.Domain.MedicalStatus;

namespace Trainova.Api.Requests.MedicalStatus
{
    public class PlayerInjuryCreateRequest
    {
        public Guid InjuryId { get; set; }
        public Guid PlayerId { get; set; }
        public DateTime? HappendAt { get; set; }
        public InjuryCause Cause { get; set; } = InjuryCause.Unknown;
        public SeverityGrade SevertiyGrade { get; set; } = SeverityGrade.Mild;
        public BodyPart BodyPart { get; set; }
        public string? Notes { get; set; }
        public bool IsNew { get; set; } = false;

        public DateTime? ExpectedReturnDate { get; set; }


        public CreatePlayerInjuryCommand ToCommand()
        {
            return new CreatePlayerInjuryCommand(
                InjuryId,
                PlayerId,
                HappendAt,
                Cause,
                SevertiyGrade,
                BodyPart,
                Notes,
                IsNew,
                ExpectedReturnDate
            );
        }


    }

    public class PlayerInjuryUpdateRequet
    {
        public InjuryStatus? Status { get; set; }
        public DateTime? HappendAt { get; set; }
        public InjuryCause? Cause { get; set; }
        public SeverityGrade? SevertiyGrade { get; set; }
        public BodyPart? BodyPart { get; set; }
        public string? Notes { get; set; }
        public bool? IsNew { get; set; }
        public InjuryStatus? NewStatus { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }

        public UpdatePlayerInjuryCommand ToUpdateCommand(Guid id)
        {
            return new UpdatePlayerInjuryCommand(
                id,
                Status,
                HappendAt,
                Cause,
                SevertiyGrade,
                BodyPart,
                Notes,
                IsNew,
                NewStatus,
                ReturnedAt,
                ExpectedReturnDate
            );
        }
    }

}
