using MassTransit;
using PaymentDomain.Events.Input;
using PaymentService.Handlers;

namespace PaymentService.Consumers
{
    public class AfterPayConsumer : IConsumer<AfterPayCompletedEvent>
    {
        private readonly PaymentHandler paymentHandler;
        public AfterPayConsumer(PaymentHandler paymentHandler)
        {
            this.paymentHandler = paymentHandler;
        }
        public async Task Consume(ConsumeContext<AfterPayCompletedEvent> context)
        {
            await paymentHandler.HandleAfterPayCompletedEvent(context.Message);
        }
    }
}
