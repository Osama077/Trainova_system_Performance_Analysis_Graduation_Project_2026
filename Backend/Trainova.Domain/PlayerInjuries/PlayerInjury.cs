using System;
using System.Collections.Generic;
using System.Text;
using Trainova.Domain.Common;
using Trainova.Domain.Injuries;
using Trainova.Domain.Players;

namespace Trainova.Domain.PlayerInjuries
{
    public class PlayerInjury : IAuditable
    {
        public Guid Id { get; private set; }
        public Guid InjuryId { get; private set; }
        public Injury Injury { get; private set; }
        public Guid PlayerId { get; private set; }
        public Player Player { get; private set; }
        public InjuryStatus Status { get; private set; }
        public DateTime HappendAt { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdate { get; private set; }
        public Guid? Owner { get; private set; }


    }
}
