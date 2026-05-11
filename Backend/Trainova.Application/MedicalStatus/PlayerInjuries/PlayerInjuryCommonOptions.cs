namespace Trainova.Application.MedicalStatus.PlayerInjuries
{
    public class PlayerInjuryCommonOptions
    {
        public const string CreatedAtSortOption = "[PlayerInjuryCreatedAt]";
        public const string TShirtNameOption = "[TShirtName]";
        public const string PlayerMedicalStatusOption = "[PlayerMedicalStatus]";
        public const string InjuryNameOption = "[InjuryName]";

        public static readonly IReadOnlyCollection<string> ValidSortColumns = new[]
        {
            CreatedAtSortOption,
            TShirtNameOption,
            PlayerMedicalStatusOption,
            InjuryNameOption
        };
    }
}
