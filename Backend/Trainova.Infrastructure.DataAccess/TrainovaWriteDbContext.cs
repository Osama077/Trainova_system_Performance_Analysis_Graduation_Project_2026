using Microsoft.EntityFrameworkCore;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Domain.Activities;
using Trainova.Domain.Coaches;
using Trainova.Domain.Doctors;
using Trainova.Domain.Events;
using Trainova.Domain.MedicalHistories;
using Trainova.Domain.Outbox;
using Trainova.Domain.Plans;
using Trainova.Domain.PlayerInjuries;
using Trainova.Domain.Players;
using Trainova.Domain.Users;

namespace Trainova.Infrastructure.DataAccess
{
    public class TrainovaWriteDbContext :DbContext,IUnitOfWork
    {
        public TrainovaWriteDbContext(DbContextOptions<TrainovaWriteDbContext> options) : base(options)
        {
        }


        // Authentication and Authorization
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }


        // Outbox
        public DbSet<EmailOutbox> EmailOutboxes { get; set; }


        // Domain Entities

        //plans and events
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Activity> Activities { get; set; }


        //medical
        public DbSet<PlayerInjury> PlayerInjuries { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }

        //profiles
        public DbSet<Player> Players { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Doctor> Doctors { get; set; }


        public async Task<int> CommitChangesAsync()
        {
            return await SaveChangesAsync();
        }

        public Task CommitTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task RollbackTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task StartTransactionAsync()
        {
            throw new NotImplementedException();
        }
    }
}
