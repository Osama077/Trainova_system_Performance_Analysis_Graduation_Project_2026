using Trainova.Domain.Common.BaseEntity;

namespace Trainova.Application.Common.Authorization
{
    public interface IPhysicalMachineAuthraizedRequest
    {
        CreationType CreationType { get; set; }
    }
}
