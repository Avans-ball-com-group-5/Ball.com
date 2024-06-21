using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticsDomain;
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

            modelBuilder.Entity<LogisticsCompany>().HasData(
                new LogisticsCompany { Location = "Breda", Name = "MailNL", PricePerKm = 0.5m },
                new LogisticsCompany { Location = "Tilburg", Name = "BHL", PricePerKm = 0.4m },
                new LogisticsCompany { Location = "Eindhoven", Name = "FredEx", PricePerKm = 0.6m }
            );
        }
    }
}
