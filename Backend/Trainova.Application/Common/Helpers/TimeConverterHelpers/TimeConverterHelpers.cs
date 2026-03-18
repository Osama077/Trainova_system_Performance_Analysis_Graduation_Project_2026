namespace Trainova.Application.Common.Helpers.TimeConverterHelpers
{
    public class TimeConverterHelper
    {
        public string TimeType { get; set; } = "Days";
        public decimal Amount { get; set; } = 20;
        public TimeSpan? ToTimeSpan ()
        {
            if (this is null)
                return null;
            return TimeType switch
            {
                "Days" => TimeSpan.FromDays((double)Amount),
                "Weeks" => TimeSpan.FromDays((double)Amount * 7),
                "Months" => TimeSpan.FromDays((double)Amount * 30),
                _ => throw new ArgumentException("Invalid TimeType. Allowed values are 'Days', 'Weeks', or 'Months'.")
            };
        }

    }


}
