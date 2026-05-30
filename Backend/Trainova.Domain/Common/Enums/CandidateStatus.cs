using System;

namespace Trainova.Domain.Common.Enums
{
    [Flags]
    public enum CandidateStatus
    {
        None = 0,
        Shortlisted = 1,
        OnTrial = 2,
        Watched = 4,
        Rejected = 8,
        Signed = 16
    }
}
