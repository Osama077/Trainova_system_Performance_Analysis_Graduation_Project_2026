using Trainova.Api.Models;
using Trainova.Application.Common.Helpers.TimeConverterHelpers;
using Trainova.Application.MedicalStatus.Injuries.Commands.CreateInjury;
using Trainova.Application.MedicalStatus.Injuries.Commands.UpdateInjury;
using Trainova.Application.MedicalStatus.PlayerInjuries.Commands.CreatePlayerInjury;
using Trainova.Application.MedicalStatus.PlayerInjuries.Commands.UpdatePlayerInjury;

namespace Trainova.Api.Requsts.MedicalStatus.PlayerInjuries
{
    public class PlayerInjuryRequest : Paginator
    {

        public TimeConverterHelper? AverageRecoveryTime { get; set; }
        public CreatePlayerInjuryCommand ToCommand()
        {
            return new CreatePlayerInjuryCommand(
);
        }
        public UpdatePlayerInjuryCommand ToUpdateCommand(Guid id)
        {
            return new UpdatePlayerInjuryCommand(
);
        }
    }

}
