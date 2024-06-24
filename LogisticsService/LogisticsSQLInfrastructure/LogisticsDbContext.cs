using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticsDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace LogisticsSQLInfrastructure
{
    public class LogisticsDbContext : DbContext
    {
        public DbSet<LogisticsCompany> LogisticsCompanies { get; set; }
        public LogisticsDbContext(DbContextOptions<LogisticsDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LogisticsCompany>(entity =>
            {
                // Configure other entity properties

                entity.Property(e => e.PricePerKm)
                    .HasColumnType("decimal(18,2)"); // Adjust precision and scale as per your requirements
            });

            modelBuilder.Entity<LogisticsCompany>().HasData(
                new LogisticsCompany { Id = Guid.NewGuid(), Location = "Breda", Name = "MailNL", PricePerKm = 0.5M, Website = "https://tracking.postnl.nl/track-and-trace/" },
                new LogisticsCompany { Id = Guid.NewGuid(), Location = "Tilburg", Name = "BHL", PricePerKm = 0.4M, Website = "https://www.dhlexpress.nl/nl/consument/track-trace" },
                new LogisticsCompany { Id = Guid.NewGuid(), Location = "Eindhoven", Name = "FredEx", PricePerKm = 0.6M, Website = "https://www.fedex.com/nl-nl/tracking.html" }
            );
        }
    }
}
