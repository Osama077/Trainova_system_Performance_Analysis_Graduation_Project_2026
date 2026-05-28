using Trainova.Application.MedicalStatus.Injuries.Commands.CreateInjury;
using Trainova.Application.MedicalStatus.Injuries.Commands.UpdateInjury;
using Trainova.Domain.MedicalStatus;

namespace Trainova.Api.Requests.MedicalStatus
{
    public class InjuryRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; } = "no Data";
        public InjuryType InjuryType { get; set; }
        public int? AverageRecoveryTimeInDayes { get; set; }
        public CreateInjuryCommand ToCommand()
        {
            return new CreateInjuryCommand(
                Name,
                Description,
                InjuryType,
                AverageRecoveryTimeInDayes);
        }

    }
    public class UpdateInjuryRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; } = "no Data";
        public InjuryType InjuryType { get; set; }
        public int? AverageRecoveryTimeInDayes { get; set; }
        public UpdateInjuryCommand ToCommand(Guid id)
        {
            return new UpdateInjuryCommand(
                id,
                Name,
                Description,
                InjuryType,
                AverageRecoveryTimeInDayes);
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
