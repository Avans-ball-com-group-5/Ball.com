using Domain.Events;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Tracing;

namespace OrderSQLInfrastructure
{
    public class OrderEventDbContext : DbContext
    {
        public DbSet<OrderBaseEvent> Events { get; set; }
        public OrderEventDbContext(DbContextOptions<OrderEventDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderBaseEvent>().ToTable("Events");
            modelBuilder.Entity<OrderBaseEvent>()
            .Property(e => e.EventType)
            .IsRequired();
            modelBuilder.Entity<OrderBaseEvent>()
            .Property(e => e.Timestamp)
            .IsRequired();
            modelBuilder.Entity<OrderBaseEvent>()
            .Property(e => e.EventData)
            .HasColumnType("nvarchar(max)"); // For PostgreSQL; use "json" for other databases
        }
    }
}
