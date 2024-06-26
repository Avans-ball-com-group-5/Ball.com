using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ProductSQLInfrastructure
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Product 1",
                    Price = 10,
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Product 2",
                    Price = 20,
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Product 3",
                    Price = 30,
                }
                );
        }
    }
}