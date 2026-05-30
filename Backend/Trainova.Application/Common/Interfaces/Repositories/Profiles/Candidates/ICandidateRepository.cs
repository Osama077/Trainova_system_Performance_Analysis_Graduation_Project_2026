using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Trainova.Application.Scouting.Candidates;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.Scouting;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;

namespace Trainova.Application.Common.Interfaces.Repositories.Profiles.Candidates
{
    [System.Obsolete("Use Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates.ICandidateRepository instead.")]
    public interface ICandidateRepository : Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates.ICandidateRepository
    {
    }
}
