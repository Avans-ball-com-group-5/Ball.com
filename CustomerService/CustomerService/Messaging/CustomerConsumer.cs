using Domain;
using MassTransit;

namespace CustomerService.Messaging
{
    public class CustomerConsumer : IConsumer<Customer>
    {
        public Task Consume(ConsumeContext<Customer> context)
        {
            // Do something with the customer info, like saving it to a database or sending it to another service
            throw new NotImplementedException();
        }
    }
}
