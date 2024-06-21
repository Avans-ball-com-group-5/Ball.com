using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsDomain.Services
{
    public interface ILogisticsRepository
    {
        LogisticsCompany GetLogisticsCompanyById(Guid id);
        LogisticsCompany GetCheapestLogisticsCompany();
    }
}
