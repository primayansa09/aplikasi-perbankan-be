using API_BSS.Controllers.DTO;
using API_BSS.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace API_BSS.Data
{
    public class APIDBContext : DbContext
    {
        public APIDBContext(DbContextOptions<APIDBContext> options) : base(options) 
        { }

        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Transactions> Transactions{ get; set; }
        public DbSet<TransferResultDTO> TransferResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // USER
            modelBuilder.Entity<Users>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // ACCOUNT
            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.HasIndex(a => a.Account)
                      .IsUnique();

                entity.Property(a => a.Amount)
                      .HasPrecision(18, 2);

            });

            // TRANSACTION
            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.Property(t => t.Amount)
                      .HasPrecision(18, 2);

                entity.Property(t => t.BalanceBefore)
                      .HasPrecision(18, 2);

                entity.Property(t => t.BalanceAfter)
                      .HasPrecision(18, 2);
            });

            modelBuilder.Entity<TransferResultDTO>().HasNoKey();
        }
    }
}
