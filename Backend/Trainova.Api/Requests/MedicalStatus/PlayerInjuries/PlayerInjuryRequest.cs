using Trainova.Application.Common.Helpers.TimeConverterHelpers;
using Trainova.Application.MedicalStatus.PlayerInjuries.Commands.CreatePlayerInjury;
using Trainova.Domain.MedicalStatus.PlayerInjuries;

namespace Trainova.Api.Requests.MedicalStatus.PlayerInjuries
{
    public class PlayerInjuryRequest
    {
        public Guid InjuryId { get; set; }
        public Guid PlayerId { get; set; }
        public InjuryStatus Status { get; set; } = InjuryStatus.InHealing;
        public DateTime? HappendAt { get; set; }
        public InjuryCause Cause { get; set; } = InjuryCause.Unknown;
        public SevertiyGrade SevertiyGrade { get; set; } = SevertiyGrade.Mild;
        public BodyPart BodyPart { get; set; }
        public string? Notes { get; set; }
        public bool IsNew { get; set; } = false;

        public DateTime? ExpectedReturnDate { get; set; }


        public CreatePlayerInjuryCommand ToCommand()
        {
            return new CreatePlayerInjuryCommand(
                InjuryId,
                PlayerId,
                Status,
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

}
