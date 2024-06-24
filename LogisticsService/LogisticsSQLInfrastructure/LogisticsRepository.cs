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

        public LogisticsCompany GetCheapestLogisticsCompany()
        {
            return _context.LogisticsCompanies.OrderBy(x => x.PricePerKm).FirstOrDefault();
        }

        public LogisticsCompany GetLogisticsCompanyById(Guid id)
        {
            return _context.LogisticsCompanies.FirstOrDefault(x => x.Id == id);
        }
    }
}
