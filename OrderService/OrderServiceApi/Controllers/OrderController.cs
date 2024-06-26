using Domain;
using Domain.Events;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using OrderServiceApi.Handlers;

namespace OrderServiceApi.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderQueryHandler orderQuery;
        private readonly OrderEventHandler orderHandler;

        public OrderController(IOrderQueryHandler orderQuery, OrderEventHandler orderHandler)
        {
            this.orderQuery = orderQuery;
            this.orderHandler = orderHandler;
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public Order GetOrderById([FromRoute] Guid id)
        {

            return orderQuery.GetOrderById(id);
        }

        [HttpPost]
        [Route("api/[controller]/")]
        public async Task NewOrder([FromBody] PlaceOrderEvent order)
        {
            await orderHandler.PlaceOrder(order);
        }
    }
}