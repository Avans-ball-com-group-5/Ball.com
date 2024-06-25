using Domain;
using Microsoft.EntityFrameworkCore;

namespace CustomerSQLInfastructure
{
    public class CustomerDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Ticket> Tickets { get; set; }
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customers");

            modelBuilder.Entity<Customer>()
            .Property(e => e.Id)
            .IsRequired();

            modelBuilder.Entity<Customer>()
            .Property(e => e.Address)
            .HasColumnType("nvarchar(500)")
            .IsRequired();

            modelBuilder.Entity<Ticket>().ToTable("Tickets");

            modelBuilder.Entity<Ticket>()
            .Property(e => e.Message)
            .HasColumnType("nvarchar(200)")
            .IsRequired();
        }
    }
}
