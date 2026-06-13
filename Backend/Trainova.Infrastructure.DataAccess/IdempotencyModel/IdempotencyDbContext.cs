using Microsoft.EntityFrameworkCore;

namespace Trainova.Infrastructure.DataAccess.IdempotencyModel
{
    public class IdempotencyDbContext : DbContext
    {
        public IdempotencyDbContext(DbContextOptions<IdempotencyDbContext> options) : base(options) { }

        public DbSet<IdempotentRequest> IdempotentRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdempotentRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId);
                entity.Property(e => e.Name).HasMaxLength(250).IsRequired();
                entity.Property(e => e.ResponseBody).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });
        }
    }
}
