﻿using Domain;
using Domain.Events;
using MassTransit;

namespace CustomerService.Services
{
    public class CustomerHandler
    {
        private readonly IBus Bus;
        public CustomerHandler(IBus bus)
        {
            Bus = bus;
        }

        public async Task RegisterCustomerService(RegisterCustomerService registerEvent)
        {
            // Do something with the customer info, like saving it to a database or sending it to another service
            // For now, we'll just publish the created event
            Console.WriteLine("Registering customer service...");
            var createdEvent = new CustomerServiceRegistered(Guid.Parse(registerEvent.Id),
                                                             registerEvent.Name,
                                                             registerEvent.Email,
                                                             registerEvent.Phone,
                                                             registerEvent.Message,
                                                             Guid.NewGuid());

            await Bus.Publish(createdEvent);
        }
    }
}
