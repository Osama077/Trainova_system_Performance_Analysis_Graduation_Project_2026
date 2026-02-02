namespace Trainova.Domain.Common
{
    [Flags]
    public enum ManageableBy
    {
        None = 0,
        Owner = 1,
        Players = 2,
        Coaches = 4,
        Doctors = 8,
    }
}
