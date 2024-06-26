﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Services
{
    public interface ILogisticsRepository
    {
        LogisticsCompany GetLogisticsCompanyById(Guid id);
        (LogisticsCompany Company, int Distance) GetCheapestLogisticsCompany();
    }
}
