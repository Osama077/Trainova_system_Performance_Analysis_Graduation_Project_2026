using Trainova.Api.Models;
using Trainova.Application.Common.Helpers.TimeConverterHelpers;
using Trainova.Application.MedicalStatus.PlayerInjuries.Commands.CreatePlayerInjury;
using Trainova.Application.MedicalStatus.PlayerInjuries.Commands.UpdatePlayerInjury;
using Trainova.Domain.MedicalStatus.PlayerInjuries;

namespace Trainova.Api.Requests.MedicalStatus.PlayerInjuries
{
    public class PlayerInjuryRequest : Paginator
    {
        // Required for creating a player injury
        public Guid InjuryId { get; set; }
        public Guid PlayerId { get; set; }
        public InjuryStatus Status { get; set; } = InjuryStatus.InHealing;
        public DateTime? HappendAt { get; set; }
        public InjuryCause Cause { get; set; } = InjuryCause.Unknown;
        public SevertiyGrade SevertiyGrade { get; set; } = SevertiyGrade.Mild;
        public string BodyPart { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public bool IsNew { get; set; } = false;

        // Fields used when updating
        public InjuryStatus? NewStatus { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }

        // kept from original file in case you need to send average recovery time
        public TimeConverterHelper? AverageRecoveryTime { get; set; }

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
                IsNew
            );
        }

        public UpdatePlayerInjuryCommand ToUpdateCommand(Guid id)
        {
            return new UpdatePlayerInjuryCommand(
                id,
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
