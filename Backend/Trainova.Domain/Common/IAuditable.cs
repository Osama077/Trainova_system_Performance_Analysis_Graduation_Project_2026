namespace Trainova.Domain.Common
{
    public interface IAuditable
    {
        public DateTime CreatedAt { get; }
        public DateTime? LastUpdate {  get; }
        public Guid? Owner { get; }
    }
}
