using Domain;
using Microsoft.EntityFrameworkCore;

namespace PaymentSQLInfrastructure
{
    public class PaymentDbContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }

        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>().ToTable("Payments");

            modelBuilder.Entity<Payment>()
            .Property(e => e.Id)
            .IsRequired();

            modelBuilder.Entity<Payment>()
            .Property(e => e.OrderId)
            .IsRequired();
        }
    }
}