using System;
using System.Collections.Generic;
using System.Text;

namespace Trainova.Application.Common.Authorization
{
    public interface ICreatorAuthraizedRequest
    {
        Guid? CreatorId { get; set; }
        bool IncludeCreateror { get; }

    }
}
