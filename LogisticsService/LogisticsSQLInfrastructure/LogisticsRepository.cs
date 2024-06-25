using Domain.Models;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsSQLInfrastructure
{
    public class LogisticsRepository : ILogisticsRepository
    {
        private readonly LogisticsDbContext _context;

        public LogisticsRepository(LogisticsDbContext context)
        {
            _context = context;
        }

        public (LogisticsCompany Company, int Distance) GetCheapestLogisticsCompany()
        {
            var random = new Random();

            // Create a list of companies with their random distances
            var companiesWithDistances = _context.LogisticsCompanies
                .Select(company => new
                {
                    Company = company,
                    Distance = random.Next(10, 50)
                })
                .ToList();

            // Find the company with the cheapest price per km considering the random distance
            var cheapestCompanyWithDistance = companiesWithDistances
                .OrderBy(x => x.Company.PricePerKm * x.Distance)
                .FirstOrDefault();

            // Return the cheapest company along with the distance
            return (cheapestCompanyWithDistance?.Company, cheapestCompanyWithDistance?.Distance ?? 0);
        }

        public LogisticsCompany GetLogisticsCompanyById(Guid id)
        {
            return _context.LogisticsCompanies.FirstOrDefault(x => x.Id == id);
        }
    }
}
