using Domain.Models;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsSQLInfrastructure
{
    public class TrackingRepository : ITrackingRepository
    {
        private readonly LogisticsDbContext _context;

        public TrackingRepository(LogisticsDbContext context)
        {
            _context = context;
        }

        public Tracking GetTrackingById(Guid id)
        {
            return _context.Trackings.FirstOrDefault(x => x.Id == id);
        }

        public void AddTracking(Tracking tracking)
        {
            _context.Trackings.Add(tracking);
        }
    }
}
