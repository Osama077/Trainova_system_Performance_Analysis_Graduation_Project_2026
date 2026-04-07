using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.MatchsManagement.Matches;

namespace Trainova.Domain.MatchsManagement.Videos
{
    public class MatchVideo : AuditableEntity<Guid>
    {
        public string VideoUrl { get; private set; }
        public string ObjectStoregeProviderId { get; private set; }
        public string ProviderName { get; private set; }
        public Guid? RelatedMatchId { get; private set; }
        public Match? RelatedMatch { get; private set; }
        private MatchVideo() : base() { }
        public MatchVideo(
            string videoUrl,
            string objectStoregeProviderId = null,
            string providerName = null,
            Guid? relatedTrainingSessionId = null,
            Guid? createdBy = null) : base(createdBy)
        {
            VideoUrl = videoUrl;
            ObjectStoregeProviderId = objectStoregeProviderId;
            ProviderName = providerName;
            RelatedMatchId = relatedTrainingSessionId;
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
            RelatedMatchId = relatedTrainingSessionId ?? RelatedMatchId;
        }
    }
}
