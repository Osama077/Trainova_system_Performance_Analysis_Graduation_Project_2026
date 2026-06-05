using Trainova.Common.SmartEnums;

namespace Trainova.Domain.UserAuth
{
    public class DeviceRole : SmartEnum<DeviceRole>
    {
        public string NormalizedName { get; private set; }
        public static readonly DeviceRole Internal = new DeviceRole("Internal", 1);
        public static readonly DeviceRole External = new DeviceRole("External", 2);
        public static readonly DeviceRole Integrated = new DeviceRole("Integrated", 3);

        private DeviceRole(string name, int value) : base(name, value)
        {
            NormalizedName = name.ToLower();
        }
    }
}
