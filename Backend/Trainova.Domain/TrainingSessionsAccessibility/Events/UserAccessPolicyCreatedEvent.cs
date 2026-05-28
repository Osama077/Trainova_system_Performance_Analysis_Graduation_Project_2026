using Trainova.Domain.Common.BaseEntity;

namespace Trainova.Domain.TrainingSessionsAccessibility.Events
{
    public record SessionUserAccessPolicyCreatedEvent(Guid UserId, Guid AccessPolicyId, string SessionName, DateTime? HappenedAt, string? Place) : IDomainEvent;
    public record PlanUserAccessPolicyCreatedEvent(Guid UserId, Guid AccessPolicyId, string PlanName, DateTime? StartDate) : IDomainEvent;

}
