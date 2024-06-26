using Domain;
using Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace OrderSQLInfrastructure
{
    public class OrderEventDbContext : DbContext
    {
        public DbSet<OrderBaseEvent> Events { get; set; }
        public DbSet<Order> Orders { get; set; }
        public OrderEventDbContext(DbContextOptions<OrderEventDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderBaseEvent>().ToTable("Events");

            modelBuilder.Entity<OrderBaseEvent>()
                .Property(e => e.EventType)
                .IsRequired();

            modelBuilder.Entity<OrderBaseEvent>()
                .Property(e => e.Timestamp)
                .IsRequired();

            modelBuilder.Entity<OrderBaseEvent>()
                .Property(e => e.EventData)
                .HasColumnType("nvarchar(max)");

            // turn off orderid as foreign key
            modelBuilder.Entity<OrderBaseEvent>()
                .Ignore(e => e.Order);  // Ensure that no navigation property exists
            modelBuilder.Entity<OrderBaseEvent>()
                .Property(e => e.OrderId)
                .IsRequired();



            base.OnModelCreating(modelBuilder);
        }
    }
}
