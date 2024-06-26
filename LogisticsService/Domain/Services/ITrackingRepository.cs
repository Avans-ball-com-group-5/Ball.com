using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ITrackingRepository
    {
        public Tracking GetTrackingById(Guid id);
        public void AddTracking(Tracking tracking);
    }
}
