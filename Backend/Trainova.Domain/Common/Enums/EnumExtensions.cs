namespace Trainova.Domain.Common.Enums
{
    public static class EnumExtensions
    {
        public static bool HasSingleFlag<TEnum>(this TEnum value)
            where TEnum : struct, Enum
        {
            var intValue = Convert.ToInt64(value);

            return intValue != 0 && (intValue & (intValue - 1)) == 0;
        }
    }

}
