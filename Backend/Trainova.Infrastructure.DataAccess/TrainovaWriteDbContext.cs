﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System.Security.AccessControl;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Domain.Common.AuditLogs;
using Trainova.Domain.Common.Outbox;
using Trainova.Domain.MedicalStatus.PlayerInjuries;
using Trainova.Domain.Profiles.Players;
using Trainova.Domain.Profiles.TeamsStaff;
using Trainova.Domain.TrainingSessionsAccessibility.Plans;
using Trainova.Domain.TrainingSessionsAccessibility.TrainingSessions;
using Trainova.Domain.UserAuth.Roles;
using Trainova.Domain.UserAuth.UserRoles;
using Trainova.Domain.UserAuth.Users;
using Trainova.Domain.UserAuth.UserTokens;

namespace Trainova.Infrastructure.DataAccess
{
    public class TrainovaWriteDbContext :DbContext,IUnitOfWork
    {
        private readonly CurrentUser _currentUser;
        private IDbContextTransaction _dbTransaction;
        private static readonly string logFilePath = @"D:\EFCoreDebugLog.txt";


        public TrainovaWriteDbContext(
            DbContextOptions<TrainovaWriteDbContext> options,
            CurrentUser currentUser) : base(options)
        {
            _currentUser = currentUser;
        }

        public bool IsInTransaction { get; private set; } = false;
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
        public DbSet<TrainingSession> TrainingSessions { get; set; }


        //medical
        public DbSet<PlayerInjury> PlayerInjuries { get; set; }
        public DbSet<Trainova.Domain.MedicalStatus.Injuries.Injury> Injuries { get; set; }

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

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Logging في ملف
            optionsBuilder.LogTo(logMessage =>
            {
                try
                {
                    File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
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
                .Entries()
                .Where(e =>
                    e.Entity is IAuditable &&
                    e.Entity is not AuditLog &&
                    (e.State == EntityState.Added ||
                     e.State == EntityState.Modified ||
                     e.State == EntityState.Deleted));

            foreach (var entry in entries)
            {
                var action = entry.State switch
                {
                    EntityState.Added => AuditActionType.Create,
                    EntityState.Modified => AuditActionType.Update,
                    EntityState.Deleted => AuditActionType.Delete,
                    _ => throw new Exception("Unsupported state")
                };

                var auditable = (IAuditable)entry.Entity;

                var log = AuditLog.Create(auditable, action);

                log.SetUser(_currentUser?.Id ?? Guid.Empty);

                logs.Add(log);
            }

            return logs;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var auditLogs = HandleAuditLogs();


            if (auditLogs.Any())
            {
                await AuditLoges.AddRangeAsync(auditLogs);
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
        

    }
}
