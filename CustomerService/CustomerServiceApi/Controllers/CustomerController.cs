using CustomerServiceApi.Services;
using Domain.Events;
using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceApi.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerHandler handler;

        public CustomerController(CustomerHandler customerHandler)
        {
            this.handler = customerHandler;
        }

        [HttpPost]
        [Route("api/[controller]/ticket")]
        public async Task NewTicket([FromBody]RegisterCustomerServiceTicket ticket)
        {
            await handler.RegisterCustomerServiceTicket(ticket);
        }

    }
}
