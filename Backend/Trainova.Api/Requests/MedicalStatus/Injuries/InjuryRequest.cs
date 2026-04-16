using Trainova.Application.Common.Helpers.TimeConverterHelpers;
using Trainova.Application.MedicalStatus.Injuries.Commands.CreateInjury;
using Trainova.Application.MedicalStatus.Injuries.Commands.UpdateInjury;

namespace Trainova.Api.Requsts.MedicalStatus.Injuries
{
    public class InjuryRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; } = "no Data";
        public string? InjuryType { get; set; } = "Muscular";
        public TimeConverterHelper? AverageRecoveryTime { get; set; }
        public CreateInjuryCommand ToCommand()
        {
            return new CreateInjuryCommand(
                Name,
                Description,
                InjuryType,
                AverageRecoveryTime.TimeType,
                AverageRecoveryTime.Amount);
        }
        public UpdateInjuryCommand ToUpdateCommand(Guid id)
        {
            return new UpdateInjuryCommand(
                id,
                Name,
                Description,
                InjuryType,
                AverageRecoveryTime.TimeType,
                AverageRecoveryTime.Amount);
        }
    }
    /*
    {
        Name : "Hamstring Strain",
        Description : "A common injury among athletes, especially those involved in sports that require sudden starts and stops.",
        InjuryType : "Muscle",
        AverageRecoveryTime : {
            TimeType : "Weeks",
            Amount : 4
        }
    }
    *
    * TimeAttacher: new TimeAttacher(TimeType: "Weeks", Amount: 4)
    *
    * TimeTypes [ Days, Weeks, Months]
    * InjuryType [ Muscular, Bone]
    */
}
