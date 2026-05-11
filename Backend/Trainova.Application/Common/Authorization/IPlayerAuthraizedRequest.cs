namespace Trainova.Application.Common.Authorization
{
    public interface IPlayerAuthraizedRequest
    {
        Guid? PlayerId { get; set; }
    }
}
