using Trainova.Domain.Common.BaseEntity;

namespace Trainova.Domain.UserAuth.DomainEvents
{
    public record TFATokenCreateEvent(User User) : IDomainEvent;

}
