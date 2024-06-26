using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsServiceApi.Controllers
{
    [ApiController]
    public class TrackingController : ControllerBase
    {
        private readonly ITrackingRepository trackingRepository;

        public TrackingController(ITrackingRepository trackingRepository)
        {
            this.trackingRepository = trackingRepository;
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public Tracking GetTrackingById([FromRoute]Guid id)
        {
            return trackingRepository.GetTrackingById(id);
        }
    }
}