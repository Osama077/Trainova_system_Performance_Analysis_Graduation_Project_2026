using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.Common.Services;
using Trainova.Domain.Profiles.Players;
using Trainova.Domain.Profiles.TeamsStaff;
using Trainova.Domain.SeasonsAnalyses.Teams;
using Trainova.Domain.UserAuth.DomainEvents;
using Trainova.Domain.UserAuth.UserRoles;

namespace Trainova.Domain.UserAuth.Users
{
    public class User :AuditableEntity<Guid>
    {
        public Guid? TeamId { get; private set; }
        public Team Team { get; private set; } = null!;
        public string ShowName { get; private set; } = null!;
        public string FullName { get; private set; } = null!;
        public string? PhotoPath { get; private set; } = null!;
        public bool IsTFAEnabled { get; private set; } = false;
        public DateTime? TFAEnabledAt {  get; private set; }
        public string Email { get; private set; } = null!;
        public bool IsEmailConfirmed { get; private set; } = false;
        public DateTime? ConfirmedAt { get; private set; }

        private string _passwordHash;
        public bool IsActive { get; private set; }

        public ICollection<UserRole> Roles { get; private set; } = new List<UserRole>();

        public Player? Player { get; private set; } = null!;
        public TeamStaff? TeamStaff { get; private set; } = null!;
        public User(
            string showName,
            string fullName,
            string email,
            string? photoPath = null,
            Guid? teamId = null) : base(Guid.NewGuid())
        {
            ShowName = showName;
            FullName = fullName;
            PhotoPath = photoPath;
            Email = email;
            IsActive = true;
            TeamId = teamId;
        }


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
                    message: "Can't confirm already confirmed email");
            IsEmailConfirmed = true;
            ConfirmedAt = DateTime.UtcNow;
        }

        public void UpdateUserData(
            string? showName = null,
            string? fullName = null,
            string? photoPath = null,
            string? email = null)
        {
            MarkUpdatedNow();
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
        public void ChangeTFAStatus(bool isEnabled, string password, IPasswordHasher passwordHasher)
        {
            MarkUpdatedNow();
            if (!IsCorrectPasswordHash(password, passwordHasher))
                throw new DomainException(
                    code: "InvalidPassword",
                    message: "The provided password is incorrect");

            if (IsTFAEnabled == isEnabled)
                throw new DomainException(
                    code: "TFAStatusUnchanged",
                    message: $"TFA status is already {(isEnabled ? "enabled" : "disabled")}");

            if (isEnabled && TFAEnabledAt is null)
                throw new DomainException(
                    code: "InvalidTFAStatus",
                    message: $"is enabled With no date.");

            if (!isEnabled && TFAEnabledAt is not null)
                throw new DomainException(
                    code: "InvalidTFAStatus",
                    message: $"is disabled With a date.");

            if (isEnabled)
                TFAEnabledAt = DateTime.UtcNow;
            else
                TFAEnabledAt = null;

            IsTFAEnabled = isEnabled;
        }

        public void CreateTFAToken()
        {
            AddDomainEvent(new TFATokenCreateEvent(this));
        }


    }
}
