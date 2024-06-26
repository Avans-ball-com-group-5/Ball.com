using Microsoft.AspNetCore.Mvc;
using PaymentServiceApi.Handlers;

namespace PaymentServiceApi.Controllers
{
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentHandler handler;

        public PaymentController(PaymentHandler handler)
        {
            this.handler = handler;
        }

        [HttpPost]
        [Route("/api/[controller]/")]
        public async Task PaymentCompleted([FromBody] Guid id)
        {
            await handler.HandleAfterPayCompletedEvent(id);
        }
    }
}