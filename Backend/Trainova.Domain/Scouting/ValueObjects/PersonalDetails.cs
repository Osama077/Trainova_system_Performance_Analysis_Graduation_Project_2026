namespace Trainova.Domain.Scouting.ValueObjects
{
    /// <summary>
    /// Value object representing player's personal details
    /// </summary>
    public class PersonalDetails
    {
        public DateTime? DateOfBirth { get; private set; }
        public int? Height { get; private set; } // in cm
        public int? Weight { get; private set; } // in kg
        public string PreferredFoot { get; private set; } // "Right" or "Left"

        public PersonalDetails(
            DateTime? dateOfBirth = null,
            int? height = null,
            int? weight = null,
            string preferredFoot = "Right")
        {
            DateOfBirth = dateOfBirth;
            Height = height;
            Weight = weight;
            PreferredFoot = preferredFoot ?? "Right";
        }

        public void Update(
            DateTime? dateOfBirth = null,
            int? height = null,
            int? weight = null,
            string? preferredFoot = null)
        {
            if (dateOfBirth.HasValue)
                DateOfBirth = dateOfBirth;
            if (height.HasValue)
                Height = height;
            if (weight.HasValue)
                Weight = weight;
            if (!string.IsNullOrEmpty(preferredFoot))
                PreferredFoot = preferredFoot;
        }
    }
}
