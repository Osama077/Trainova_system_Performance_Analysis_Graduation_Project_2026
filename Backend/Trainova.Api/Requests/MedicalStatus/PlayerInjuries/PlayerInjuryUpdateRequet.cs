using Trainova.Application.MedicalStatus.PlayerInjuries.Commands.UpdatePlayerInjury;
using Trainova.Domain.MedicalStatus.PlayerInjuries;

namespace Trainova.Api.Requests.MedicalStatus.PlayerInjuries
{
    public class PlayerInjuryUpdateRequet
    {
        public InjuryStatus? Status { get; set; }
        public DateTime? HappendAt { get; set; }
        public InjuryCause? Cause { get; set; }
        public SevertiyGrade? SevertiyGrade { get; set; }
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
