using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Trainova.Domain.UserAuth.Users;

namespace Trainova.Domain.UserAuth.DomainEvents
{
    public record TFATokenCreateEvent(User User) : INotification;

}
