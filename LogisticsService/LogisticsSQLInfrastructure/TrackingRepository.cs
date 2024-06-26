using Domain.Models;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
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
            return _context.Trackings
                .Include(x => x.Order)
                .ThenInclude(x => x.Items)
                .FirstOrDefault(x => x.Id == id) ?? throw new Exception("Tracking cannot be found!");
        }

        public void AddTracking(Tracking tracking)
        {
            _context.Trackings.Add(tracking);
            int changes = _context.SaveChanges();
            Console.WriteLine($"{changes} changes saved to the database."); // Debugging statement
        }
    }
}
