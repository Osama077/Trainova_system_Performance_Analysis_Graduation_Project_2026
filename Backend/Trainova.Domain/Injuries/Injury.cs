using System;
using System.Collections.Generic;
using System.Text;
using Trainova.Domain.Common;

namespace Trainova.Domain.Injuries
{
    public class Injury : IAuditable
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public TimeSpan AverageRecoveryTime { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdate { get; private set; }
        public Guid? Owner { get; private set; }


    }
}
