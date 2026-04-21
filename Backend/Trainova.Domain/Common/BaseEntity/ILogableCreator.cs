namespace Trainova.Domain.Common.BaseEntity
{
    public interface ICreatorLogable
    {
        Guid? CreatedBy { get; }
        void SetCreator(Guid creatorId);

    }
}
