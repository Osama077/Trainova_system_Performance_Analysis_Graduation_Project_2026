namespace Trainova.Domain.Common.BaseEntity
{
    public interface IMultiWayCreatable : ICreatorLogable
    {
        CreationType CreationType { get; }
    }
}
