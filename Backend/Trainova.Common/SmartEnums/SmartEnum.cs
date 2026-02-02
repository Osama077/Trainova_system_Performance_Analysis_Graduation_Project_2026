using System.Collections.Concurrent;
using System.Reflection;

namespace Trainova.Common.SmartEnums
{
    public abstract class SmartEnum<TEnum>
    where TEnum : SmartEnum<TEnum>
    {
        public string Name { get; }
        public int Value { get; }

        private static readonly Lazy<IReadOnlyList<TEnum>> _all =
            new Lazy<IReadOnlyList<TEnum>>(LoadAll);

        private static readonly ConcurrentDictionary<int, TEnum> _fromValueCache = new();
        private static readonly ConcurrentDictionary<string, TEnum> _fromNameIgnoreCaseCache =
            new(StringComparer.OrdinalIgnoreCase);

        private static readonly ConcurrentDictionary<string, TEnum> _fromNameCaseSensitiveCache =
            new(StringComparer.Ordinal);

        protected SmartEnum(string name, int value)
        {
            Name = name;
            Value = value;

            _fromValueCache.TryAdd(value, (TEnum)this);
            _fromNameIgnoreCaseCache.TryAdd(name, (TEnum)this);
            _fromNameCaseSensitiveCache.TryAdd(name, (TEnum)this);
        }

        private static IReadOnlyList<TEnum> LoadAll()
        {
            return typeof(TEnum)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(f => f.FieldType == typeof(TEnum))
                .Select(f => (TEnum)f.GetValue(null)!)
                .ToList();
        }

        public static IReadOnlyList<TEnum> GetAll() => _all.Value;

        public static TEnum? FromValue(int value)
            => _fromValueCache.TryGetValue(value, out var result) ? result : null;

        public static TEnum? FromName(string name, bool caseSensitive = false)
        {
            if (caseSensitive)
            {
                return _fromNameCaseSensitiveCache.TryGetValue(name, out var result)
                    ? result
                    : null;
            }

            return _fromNameIgnoreCaseCache.TryGetValue(name, out var ignoreCaseResult)
                ? ignoreCaseResult
                : null;
        }

        public override string ToString() => Name;

        public override bool Equals(object? obj)
        {
            if (obj is not SmartEnum<TEnum> other)
                return false;

            return Value == other.Value;
        }

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(SmartEnum<TEnum>? left, SmartEnum<TEnum>? right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left is null || right is null)
                return false;

            return left.Value == right.Value;
        }

        public static bool operator !=(SmartEnum<TEnum>? left, SmartEnum<TEnum>? right)
            => !(left == right);
        public static bool TryFromValue(int value, out TEnum result)
        {
            return _fromValueCache.TryGetValue(value, out result!);
        }


        public static bool TryFromName(
        string name,
        out TEnum result,
        bool caseSensitive = false)
        {
            if (caseSensitive)
            {
                return _fromNameCaseSensitiveCache.TryGetValue(name, out result!);
            }

            return _fromNameIgnoreCaseCache.TryGetValue(name, out result!);
        }

    }
}
