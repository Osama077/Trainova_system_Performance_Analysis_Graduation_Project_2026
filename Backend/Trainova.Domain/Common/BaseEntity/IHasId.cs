namespace Trainova.Domain.Common.BaseEntity
{
    public interface IHasId<TId>
    {
        TId Id { get; }
    }

}
