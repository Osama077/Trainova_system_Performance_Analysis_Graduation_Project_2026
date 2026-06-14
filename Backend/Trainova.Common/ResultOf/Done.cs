using System.Text.Json.Serialization;

namespace Trainova.Common.ResultOf;

public record struct Done
{
    public string Message { get; set; } = "The action has been Done.";
    public Guid Id { get; set; }

    public Done(string message = null, Guid? id = null)
    {
        Message = message ?? "The action has been Done.";
        Id = id ?? Guid.Empty;
    }
    [JsonIgnore]
    public static Done Default => new Done();
    [JsonIgnore]
    public ResultOf<Done> NoContent => Id != Guid.Empty
        ? new Done($"The action has been Done with no content but Id = {Id}.", Id)
        : new Done("The action has been Done with no content.", Id);
}
