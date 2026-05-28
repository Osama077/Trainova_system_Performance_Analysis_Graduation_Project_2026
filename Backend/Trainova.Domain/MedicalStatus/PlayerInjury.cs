using System.Text.Json.Serialization;
using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.Profiles;

namespace Trainova.Domain.MedicalStatus
{
    public class PlayerInjury : AuditableEntity<Guid>
    {
        public Guid InjuryId { get; private set; }
        [JsonIgnore]
        public Injury Injury { get; private set; }
        public Guid PlayerId { get; private set; }
        [JsonIgnore]
        public Player Player { get; private set; }
        public InjuryStatus Status { get; private set; } = InjuryStatus.InHealing;
        public InjuryCause Cause { get; private set; }
        public SeverityGrade SevertiyGrade { get; private set; }
        public BodyPart BodyPart { get; private set; }
        public string? Notes { get; private set; }
        public bool IsNew { get; private set; }

        public DateTime? HappendAt { get; private set; }
        public DateTime? ReturnedAt { get; private set; }
        public DateTime? ExpectedReturnDate { get; private set; }
        [JsonIgnore]
        public List<RecoveryPlanPhase> Phases { get; private set; } = new List<RecoveryPlanPhase>();


        public PlayerInjury(
            Player player,
            Injury injury,
            InjuryStatus status,
            DateTime? happendAt = null,
            InjuryCause cause = default,
            SeverityGrade severtiyGrade = default,
            BodyPart bodyPart = default,
            string notes = null,
            bool isNew = false,
            DateTime? expectedReturnDate = null,
            Guid? createdBy = null) : base(Guid.NewGuid(), createdBy)
        {
            if (player.MedicalStatus != PlayerMedicalStatus.Injured)
            {
                player!.MarkAsInjuried();
            }
            PlayerId = player.Id;
            InjuryId = injury.Id;
            Status = status;
            HappendAt = happendAt ?? DateTime.UtcNow;
            Cause = cause;
            SevertiyGrade = severtiyGrade;
            BodyPart = bodyPart;
            Notes = notes;
            IsNew = isNew;
            ExpectedReturnDate = expectedReturnDate ?? DateTime.UtcNow.AddDays(injury.AverageRecoveryTimeInDayes ?? 0);
        }

        public void Update(
            DateTime? happendAt = null,
            InjuryCause? cause = null,
            SeverityGrade? severtiyGrade = null,
            BodyPart? bodyPart = null,
            string? notes = null,
            bool? isNew = null,
            InjuryStatus? newStatus = null,
            DateTime? returnedAt = null,
            DateTime? expectedReturnDate = null)
        {
            MarkUpdatedNow();
            HappendAt = happendAt ?? HappendAt;
            Cause = cause ?? Cause;
            SevertiyGrade = severtiyGrade ?? SevertiyGrade;
            BodyPart = bodyPart ?? BodyPart;
            Notes = notes ?? Notes;
            IsNew = isNew ?? IsNew;
            ExpectedReturnDate = expectedReturnDate ?? ExpectedReturnDate;
            if (newStatus.HasValue)
            {
                Status = newStatus.Value;
                if (newStatus == InjuryStatus.Ended)
                {
                    ReturnedAt = returnedAt ?? DateTime.UtcNow;
                }
            }
        }




        public void UpdateRecoveryPlanPhase(
            Guid phaseId,
            string? name,
            string? description,
            int? durationInDays,
            List<string>? activities)
        {
            var phase = Phases.FirstOrDefault(p => p.Id == phaseId);
            if (phase == null)
                throw new DomainException("Phase not found in this injury case.", "PhaseNotFound");

            MarkUpdatedNow();

            if (durationInDays.HasValue)
            {
                if (durationInDays.Value <= 0)
                    throw new DomainException("Phase duration must be at least 1 day.", "InvalidPhaseDuration");

                DateTime originalTo = phase.To;
                DateTime newTo = phase.From.AddDays(durationInDays.Value);
                TimeSpan durationDifference = newTo - originalTo;

                phase.UpdateForReOrder(phase.Order, phase.From, newTo);

                var subsequentPhases = Phases.Where(p => p.Order > phase.Order).OrderBy(p => p.Order);
                foreach (var subPhase in subsequentPhases)
                {
                    DateTime updatedFrom = subPhase.From.Add(durationDifference);
                    DateTime updatedTo = subPhase.To.Add(durationDifference);

                    subPhase.UpdateForReOrder(subPhase.Order, updatedFrom, updatedTo);
                }
            }

            phase.Update(name, description, phase.To, activities);

            ExpectedReturnDate = Phases.MaxBy(p => p.Order)!.To;
        }


        public void AddRecoveryPlanPhase(
            string name,
            string? description,
            int durationInDays,
            List<string>? activities = null,
            int? insertAtOrder = null)
        {
            MarkUpdatedNow();

            int targetOrder = insertAtOrder ?? Phases.Count;

            if (targetOrder < 0 || targetOrder > Phases.Count)
                throw new DomainException("Invalid insert order location", "InvalidInsertOrder");

            if (durationInDays <= 0)
                throw new DomainException("Phase duration must be at least 1 day.", "InvalidPhaseDuration");

            DateTime newPhaseStart;
            if (targetOrder == 0)
            {
                newPhaseStart = HappendAt ?? CreatedAt;
            }
            else
            {
                newPhaseStart = Phases.First(p => p.Order == targetOrder - 1).To;
            }

            if (newPhaseStart < DateTime.UtcNow)
            {
                throw new DomainException("Cannot insert or append a phase in a past timeframe.", "CannotInsertInPastTime");
            }

            TimeSpan newPhaseDuration = TimeSpan.FromDays(durationInDays);
            DateTime newPhaseEnd = newPhaseStart.Add(newPhaseDuration);

            var newPhase = new RecoveryPlanPhase(
                playerInjuryId: this.Id,
                name: name,
                description: description,
                to: newPhaseEnd,
                from: newPhaseStart,
                activties: activities
            );

            newPhase.SetOrder(targetOrder);

            if (targetOrder < Phases.Count)
            {
                var phasesToShift = Phases.Where(p => p.Order >= targetOrder).OrderBy(p => p.Order);

                foreach (var phase in phasesToShift)
                {
                    int updatedOrder = phase.Order + 1;
                    DateTime updatedFrom = phase.From.Add(newPhaseDuration);
                    DateTime updatedTo = phase.To.Add(newPhaseDuration);

                    phase.UpdateForReOrder(updatedOrder, updatedFrom, updatedTo);
                }
            }

            Phases.Add(newPhase);
            ExpectedReturnDate = Phases.MaxBy(p => p.Order)!.To;

        }
        public List<RecoveryPlanPhase> ReorderPhases(List<int> newOrder)
        {
            MarkUpdatedNow();
            if (newOrder == null || newOrder.Count != Phases.Count)
                throw new DomainException("Invalid reorder list", "InvalidOrder");

            var phaseByOldOrder = Phases.ToDictionary(p => p.Order);

            var datesByOrderSnapshot = Phases.ToDictionary(
                p => p.Order,
                p => new { p.From, p.To }
            );

            for (int i = 0; i < newOrder.Count; i++)
            {
                var oldOrder = newOrder[i];

                if (!phaseByOldOrder.TryGetValue(oldOrder, out var phase))
                    throw new DomainException("Phase not found", "PhaseNotFound");

                var targetLocationDates = datesByOrderSnapshot[i];

                phase.UpdateForReOrder(
                    newOrder: i,
                    newStartDate: targetLocationDates.From,
                    newEndDate: targetLocationDates.To
                );
            }
            ExpectedReturnDate = Phases.OrderBy(p => p.Order).Last().To;

            return Phases;

        }
        public void RemovePhase(int order)
        {
            if (order < 0 || order >= Phases.Count || !Phases.Any())
                throw new DomainException("Order is out of the list range", "EmptyPhasesOrOrderOutOfRange");

            var phaseToBeDeleted = Phases.FirstOrDefault(p => p.Order == order);

            if (phaseToBeDeleted == null)
                throw new DomainException("No phase with that order", "PhaseNotFound");

            MarkUpdatedNow();

            var deletedPhaseDuration = phaseToBeDeleted.To - phaseToBeDeleted.From;

            Phases.Remove(phaseToBeDeleted);

            var phasesToShift = Phases.Where(p => p.Order > order).OrderBy(p => p.Order);

            foreach (var phase in phasesToShift)
            {
                int updatedOrder = phase.Order - 1;

                DateTime updatedFrom = phase.From.Subtract(deletedPhaseDuration);
                DateTime updatedTo = phase.To.Subtract(deletedPhaseDuration);

                phase.UpdateForReOrder(updatedOrder, updatedFrom, updatedTo);
            }

            if (Phases.Any())
            {
                ExpectedReturnDate = Phases.OrderBy(p => p.Order).Last().To;
            }
            else
            {
                ExpectedReturnDate = HappendAt;
            }
        }

        private PlayerInjury() : base() { }

    }
}
