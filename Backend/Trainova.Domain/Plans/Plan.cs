using Trainova.Domain.AccessPolicies;
using Trainova.Domain.Common;
using Trainova.Domain.Events;

namespace Trainova.Domain.Plans
{
    public class Plan : IAuditable
    {
        public Guid Id { get; private set; }
        public string PlanName { get; private set; }
        public string PlanGoul { get; private set; }
        public PlanState State { get; private set; }
        public Guid AccessPolicyId { get; private set; }
        public ManageableBy ManageableBy { get; private set; } = ManageableBy.Owner;
        public AccessPolicy AccessPolicy { get; private set; }


        //public TrainingPlan? TrainingPlan { get; private set; }
        //public FitnessPlan? FitnessPlan { get; private set; }
        //public MedicalPlan? MedicalPlan { get; private set; }

        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdate { get; private set; }
        public ICollection<Event> Events { get; private set; } = new List<Event>();

        public Guid? Owner { get; private set; }
    }

    public enum PlanState
    {
    }
}
