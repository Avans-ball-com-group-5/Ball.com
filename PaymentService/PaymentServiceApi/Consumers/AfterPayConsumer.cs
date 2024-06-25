using Domain.Events;
using MassTransit;
using PaymentServiceApi.Handlers;

namespace PaymentServiceApi.Consumers
{
    public class AfterPayConsumer : IConsumer<PayAfterPayRequest>
    {
        private readonly PaymentHandler paymentHandler;
        public AfterPayConsumer(PaymentHandler paymentHandler)
        {
            this.paymentHandler = paymentHandler;
        }
        public async Task Consume(ConsumeContext<PayAfterPayRequest> context)
        {
            await paymentHandler.HandleAfterPayCompletedEvent(context.Message.PaymentId);
        }
    }
}
