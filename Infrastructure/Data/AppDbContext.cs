using Microsoft.EntityFrameworkCore;
using TT.Backend.Core.Entities;
using TT.Backend.Modules.Maintenance.Entities;
using TT.Backend.Modules.Dev.Entities;
using TT.Backend.Modules.Telecom.Entities;
using TT.Backend.Modules.Fintech.Wallet.Entities;

namespace TT.Backend.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // Core
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;

        // Maintenance
        public DbSet<EquipmentEntity> Equipments { get; set; } = null!;
        public DbSet<PreventiveTaskEntity> PreventiveTasks { get; set; } = null!;
        public DbSet<CorrectiveTaskEntity> CorrectiveTasks { get; set; } = null!;
        public DbSet<TechnicianEntity> Technicians { get; set; } = null!;
        public DbSet<MaintenanceTaskEntity> MaintenanceTasks { get; set; } = null!;
        public DbSet<SparePartEntity> SpareParts { get; set; } = null!;
        public DbSet<SparePartUsageEntity> SparePartUsages { get; set; } = null!;

        // Dev
        public DbSet<ProjectEntity> Projects { get; set; } = null!;
        public DbSet<QuoteEntity> Quotes { get; set; } = null!;
        public DbSet<DeliveryStepEntity> DeliverySteps { get; set; } = null!;

        // Telecom
        public DbSet<PlanEntity> Plans { get; set; } = null!;
        public DbSet<SmsEntity> Sms { get; set; } = null!;
        public DbSet<TelecomUserEntity> TelecomUsers { get; set; } = null!;

        // Fintech
        public DbSet<WalletEntity> Wallets { get; set; } = null!;
        public DbSet<TransactionEntity> Transactions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectEntity>()
                .Property(p => p.Budget)
                .HasPrecision(18, 2);

            modelBuilder.Entity<QuoteEntity>()
                .Property(q => q.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<PlanEntity>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            // Fintech
            modelBuilder.Entity<WalletEntity>()
                .Property(w => w.Balance)
                .HasPrecision(18, 2);

            modelBuilder.Entity<TransactionEntity>()
                .Property(t => t.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<TransactionEntity>()
                .Property(t => t.Fees)
                .HasPrecision(18, 2);

            // Maintenance - Stock
            modelBuilder.Entity<SparePartEntity>()
                .Property(s => s.UnitPrice)
                .HasPrecision(18, 2);
        }
    }
}