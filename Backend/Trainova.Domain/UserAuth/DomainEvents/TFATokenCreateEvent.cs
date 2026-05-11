using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Trainova.Domain.UserAuth.DomainEvents
{
    public record TFATokenCreateEvent(User User) : INotification;

}
