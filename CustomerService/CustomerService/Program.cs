using CustomerService.Messaging;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace CustomerService
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            Console.WriteLine("Hello World!");
        }

        // This method is used to configure the host and services that the application will use, including consumers(endpoints)
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransitServices(ConfigureBusEndpoints);

                });

        // This method is used to configure the endpoints that the application will use
        private static void ConfigureBusEndpoints(IBusRegistrationConfigurator configurator)
        {
            // Add all consumers here for DI. This will allow the consumers to be resolved by the DI container
            configurator.AddConsumer<CustomerConsumer, CustomerConsumerDefinition>();
        }

        public static IServiceCollection AddMassTransitServices(this IServiceCollection services, Action<IBusRegistrationConfigurator> busConfigure)
        {
            services.AddMassTransit(x =>
            {
                // Default settings
                x.SetDefaultEndpointNameFormatter();

                busConfigure(x);

                // This configures rabbitmq, should be environment variables later on.
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
