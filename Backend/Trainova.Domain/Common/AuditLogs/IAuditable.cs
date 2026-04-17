using System.Text.Json.Serialization;

namespace Trainova.Domain.Common.AuditLogs
{
    public interface IAuditable
    {
        object Id { get; }
        DateTime CreatedAt { get; }
        DateTime? LastUpdate { get; }
        Guid? CreatedBy { get; }
        [JsonIgnore]
        AuditLog UpdatedAudit { get;}
        [JsonIgnore]
        AuditLog AddedAudit { get;}

    }


    public interface IAuditable<TId> : IAuditable
    {
        new TId Id { get; }
    }

}
