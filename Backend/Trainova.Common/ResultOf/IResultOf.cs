using Trainova.Common.Errors;

namespace Trainova.Common.ResultOf
{
    public interface IResultOf<out TValue>
    {
        TValue Value { get; }
    }

    public interface IResultOf
    {
        List<Error>? Errors { get; }
        bool IsFailure { get; }
    }
}
