using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common;

namespace Trainova.Domain.Users
{
    public class User :IAuditable
    {
        public Guid Id { get; private set; }

        public string ShowName { get; private set; } = null!;
        public string FullName { get; private set; } = null!;
        public string PhotoPath { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public bool IsEmailConfirmed { get; private set; } = false;
        public DateTime? ConfirmedAt { get; private set; }

        public string PasswordHash { get; private set; } = null!;
        public bool IsActive { get; private set; }

        public ICollection<UserRole> Roles { get; private set; } = new List<UserRole>();

        public DateTime CreatedAt { get; private set; }

        public DateTime? LastUpdate { get; private set; }

        public Guid? Owner { get; private set; }

        private User() { }


        public bool IsCorrectPasswordHash(string password, IPasswordHasher passwordHasher)
        {
            return passwordHasher.IsCorrectPassword(password, PasswordHash);
        }
        public ResultOf<Done> SetPassword(string password, IPasswordHasher passwordHasher)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return Error.Failure();
            }
            var PasswordHashresult = passwordHasher.HashPassword(password);
            if (PasswordHashresult.IsFailure)
            {
                return PasswordHashresult.Errors;
            }
            PasswordHash = PasswordHashresult.Value;
            return Done.done;
        }

        public void ConfirmEmail()
        {
            if (IsEmailConfirmed)
                throw new InvalidOperationException(message:"Can't confirm already confirmed email");
            IsEmailConfirmed = true;
            ConfirmedAt = DateTime.UtcNow;
        }

        public void UpdateUserData(string? showName, string? fullName, string? photoPath, string? email)
        {
            if (showName is not null)
                ShowName = showName;
            if (fullName is not null)
                FullName = fullName;
            if (photoPath is not null)
                PhotoPath = photoPath;
            if (email is not null)
            {
                Email = email;
                IsEmailConfirmed = false;
                ConfirmedAt = null;
            }
        }
    }
}
