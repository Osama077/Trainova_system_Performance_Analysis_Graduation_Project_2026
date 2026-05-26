using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Domain.Common.AuditLogs;
using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Outbox;
using Trainova.Domain.FitnessStatus;
using Trainova.Domain.FitnessStatus.PhysicalCapacityTests;
using Trainova.Domain.MedicalStatus;
using Trainova.Domain.Profiles;
using Trainova.Domain.TrainingSessionsAccessibility;
using Trainova.Domain.UserAuth;

namespace Trainova.Infrastructure.DataAccess
{
    public class TrainovaWriteDbContext :DbContext,IUnitOfWork
    {
        private readonly CurrentUser _currentUser;
        private IDbContextTransaction _dbTransaction;
        private string _logFilePath;

        public TrainovaWriteDbContext(
            DbContextOptions<TrainovaWriteDbContext> options,
            CurrentUser currentUser,
            EFCoreLoggingOptions eFCoreLoggingOptions) : base(options)
        {
            _currentUser = currentUser;
            _logFilePath = eFCoreLoggingOptions.LogFilePath;
        }

        public bool IsInTransaction { get; private set; } = false;
        // Authentication and Authorization
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }


        // Outbox
        public DbSet<EmailOutbox> EmailOutboxes { get; set; }
        public DbSet<DomainEventOutbox> DomainEventOutboxes { get; set; }

        // Domain Entities

        //plans and events
        public DbSet<AccessPolicy> AccessPolicies { get; set; }
        public DbSet<UserAccessPolicy> UserAccessPolicies { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<TrainingSession> TrainingSessions { get; set; }
        // Fitness 
        public DbSet<PhysicalCapacityTest> CapacityTests { get; set; }
        public DbSet<SessionMovement> SessionMovements { get; set; }
        public DbSet<FitnessExercise> FitnessExercises { get; set; }
        public DbSet<FitnessSessionExercise> FitnessSessionExercises { get; set; }


        //medical
        public DbSet<PlayerInjury> PlayerInjuries { get; set; }
        public DbSet<Injury> Injuries { get; set; }
        public DbSet<RecoveryPlanPhase> PlanPhases { get; set; }

        //profiles
        public DbSet<Player> Players { get; set; }
        public DbSet<TeamStaff> Coaches { get; set; }

        public DbSet<AuditLog> AuditLoges { get; set; }


        public async Task StartTransactionAsync()
        {
            if (_dbTransaction == null)
            {
                _dbTransaction = await Database.BeginTransactionAsync();
                IsInTransaction = true;
            }
        }

        public async Task CommitTransactionAsync()
        {


            if (_dbTransaction != null)
            {
                await _dbTransaction.CommitAsync();
                await _dbTransaction.DisposeAsync();
                _dbTransaction = null;
                IsInTransaction = false;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_dbTransaction != null)
            {
                _dbTransaction.Rollback();
                await _dbTransaction.DisposeAsync();
                _dbTransaction = null;
                IsInTransaction = false;
            }
        }
        //ahmed remove the following shit if you wany
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply all IEntityTypeConfiguration implementations from this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TrainovaWriteDbContext).Assembly);
            ConfigureEventsOutboxEntity(modelBuilder.Entity<DomainEventOutbox>());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Logging في ملف
            optionsBuilder.LogTo(logMessage =>
            {
                try
                {
                    File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
                }
                catch
                {
                    // ignore errors في الكتابة عشان ما توقفش EF Core
                }
            }, LogLevel.Debug);
        }



        private List<AuditLog> HandleAuditLogs()
        {
            ChangeTracker.DetectChanges();

            var logs = new List<AuditLog>();

            var entries = ChangeTracker
                .Entries<IAuditable>()
                .Where(e =>
                    e.Entity is IAuditable &&
                    (e.State == EntityState.Added ||
                     e.State == EntityState.Modified ||
                     e.State == EntityState.Deleted));

            foreach (var entry in entries)
            {
                if(entry.State == EntityState.Deleted)
                {
                    var deletedLog = entry.Entity.CreateDeletionAudit();
                    deletedLog.SetUser(_currentUser?.Id??Guid.Empty);
                    logs.Add(deletedLog);
                }

                else if(entry.State == EntityState.Added)
                {
                    var createdLog = entry.Entity.AddedAudit;
                    createdLog.SetUser(_currentUser?.Id ?? Guid.Empty);
                    logs.Add(createdLog);
                }
                else if(entry.State == EntityState.Modified)
                {
                    var updatedLog = entry.Entity.UpdatedAudit;
                    updatedLog.SetUser(_currentUser?.Id ?? Guid.Empty);
                    logs.Add(updatedLog);
                }
            }





            return logs;
        }
        private void HandleCreationLogs()
        {
            var entries = ChangeTracker
                            .Entries<ICreatorLogable>()
                            .Where(e => e.State == EntityState.Added)
                            .Select(e => e.Entity);
            foreach (var entry in entries)
            {
                entry.SetCreator(_currentUser?.Id ?? Guid.Empty);
            }
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            HandleCreationLogs();
            var auditLogs = HandleAuditLogs();

            if (auditLogs.Any())
            {
                await AuditLoges.AddRangeAsync(auditLogs);
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        private void ConfigureEventsOutboxEntity(EntityTypeBuilder<DomainEventOutbox> modelBuilder)
        {
            modelBuilder.Property(e => e.Id)
                .IsRequired();
            modelBuilder.Property(e => e.EventType)
                .IsRequired()
                .HasMaxLength(200);
            modelBuilder.Property(e => e.Notification)
                .HasMaxLength(5000)
                .IsRequired();
            modelBuilder.Property(e => e.IsHandled)
                .IsRequired();
        }
    }
}
