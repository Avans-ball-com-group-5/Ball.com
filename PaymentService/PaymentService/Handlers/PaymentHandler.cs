using MassTransit;
using PaymentDomain.Events.Input;
using PaymentDomain.Events.Output;

namespace PaymentService.Handlers
{
    public class PaymentHandler
    {
        private readonly IBus bus;

        public PaymentHandler(IBus bus)
        {
            this.bus = bus;
        }

        public async Task HandlePaymentRequest(OrderPlacedEvent request)
        {
            // Simulate payment success/failure
            if (new Random().Next(0, 10) != 1)
            {
                Console.WriteLine($"Payment for order {request.OrderId} was successful.");
                // Add payment to database.
                await bus.Publish(
                    new PaymentSuccessEvent()
                    {
                        OrderId = request.OrderId
                    });
            }
            else
            {
                Console.WriteLine($"Payment for order {request.OrderId} failed.");
                await bus.Publish(
                    new PaymentFailedEvent()
                    {
                        OrderId = request.OrderId,
                        Reason = "Payment failed"
                    });
            }
        }
    }
}
