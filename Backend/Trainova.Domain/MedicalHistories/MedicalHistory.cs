using Trainova.Domain.Common;

namespace Trainova.Domain.MedicalHistories
{
    public class MedicalHistory : IAuditable
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdate { get; private set; }
        public Guid? Owner { get; private set; }

    }



}
