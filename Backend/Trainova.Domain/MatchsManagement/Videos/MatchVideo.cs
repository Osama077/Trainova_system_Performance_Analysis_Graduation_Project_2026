using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.TrainingSessionsAccessibility.TrainingSessions;

namespace Trainova.Domain.MatchsManagement.Videos
{
    public class MatchVideo : AuditableEntity<Guid>
    {

        public string VideoUrl { get; private set; }
        public string ObjectStoregeProviderId { get; private set; }
        public string ProviderName { get; private set; }
        public Guid? RelatedTrainingSessionId { get; private set; }
        public TrainingSession? RelatedTrainingSession { get; private set; }
        private MatchVideo() : base() { }
        public MatchVideo(
            string videoUrl,
            string objectStoregeProviderId = null,
            string providerName = null,
            Guid? relatedTrainingSessionId = null,
            Guid? createdBy = null) : base(Guid.NewGuid(), createdBy)
        {
            VideoUrl = videoUrl;
            ObjectStoregeProviderId = objectStoregeProviderId;
            ProviderName = providerName;
            RelatedTrainingSessionId = relatedTrainingSessionId;
        }

        public void Update(
            string videoUrl = null,
            string objectStoregeProviderId = null,
            string providerName = null,
            Guid? relatedTrainingSessionId = null)
        {
            VideoUrl = videoUrl ?? VideoUrl;
            ObjectStoregeProviderId = objectStoregeProviderId ?? ObjectStoregeProviderId;
            ProviderName = providerName ?? ProviderName;
            RelatedTrainingSessionId = relatedTrainingSessionId ?? RelatedTrainingSessionId;
        }
    }
}
