namespace Trainova.Domain.Common.BaseEntity
{
    public interface ILogableCreator
    {
        Guid? CreatedBy { get; }
        void SetCreator(Guid creatorId);

    }
}
