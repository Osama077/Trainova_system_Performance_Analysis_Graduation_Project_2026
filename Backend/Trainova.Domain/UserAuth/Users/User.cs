using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.Common.Services;
using Trainova.Domain.Profiles.Players;
using Trainova.Domain.Profiles.TeamsStaff;
using Trainova.Domain.UserAuth.UserRoles;

namespace Trainova.Domain.UserAuth.Users
{
    public class User :AuditableEntity<Guid>
    {
        public string ShowName { get; private set; } = null!;
        public string FullName { get; private set; } = null!;
        public string PhotoPath { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public bool IsEmailConfirmed { get; private set; } = false;
        public DateTime? ConfirmedAt { get; private set; }

        private string _passwordHash;
        public bool IsActive { get; private set; }

        public ICollection<UserRole> Roles { get; private set; } = new List<UserRole>();

        public Player? Player { get; private set; } = null!;
        public TeamStaff? TeamStaff { get; private set; } = null!;



        private User() :base() { }


        public bool IsCorrectPasswordHash(string password, IPasswordHasher passwordHasher)
        {
            return passwordHasher.IsCorrectPassword(password, _passwordHash);
        }
        public ResultOf<Done> SetNewPassword(string password, IPasswordHasher passwordHasher)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return Error.Conflict(description:"can't change password to nothing");
            }
            var PasswordHashresult = passwordHasher.HashPassword(password);
            if (PasswordHashresult.IsFailure)
            {
                return PasswordHashresult.Errors;
            }
            _passwordHash = PasswordHashresult.Value;
            return Done.done;
        }

        public void ConfirmEmail()
        {
            if (IsEmailConfirmed)
                throw new DomainException(
                    code: "EmailAlreadyConfirmed",
                    massage:"Can't confirm already confirmed email");
            IsEmailConfirmed = true;
            ConfirmedAt = DateTime.UtcNow;
        }

        public void UpdateUserData(
            string? showName = null,
            string? fullName = null,
            string? photoPath = null,
            string? email = null)
        {
            ShowName = showName?? ShowName;
            FullName = fullName?? FullName;
            PhotoPath = photoPath?? PhotoPath;
            if (email is not null)
            {
                Email = email;
                IsEmailConfirmed = false;
                ConfirmedAt = null;
            }
        }
    }
}
