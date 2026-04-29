namespace Trainova.Application.MedicalStatus.Common
{
    public static class InjuryValues
    {




        public static readonly string[] AllowedTimeTypes = new[] { "Days", "Weeks", "Months" };

        public static readonly string MsgAllowedTimeTypes = "TimeType must be either 'Days', 'Weeks', or 'Months'.";
    }
}
