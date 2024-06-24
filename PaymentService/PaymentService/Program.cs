using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentService.Consumers;
using PaymentService.Handlers;

namespace PaymentService
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // This method is used to configure the host and services that the application will use, including consumers(endpoints)
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransitServices(hostContext.Configuration, ConfigureBusEndpoints);
                    services.ConfigureHandlers();
                });

        // This method is used to configure the handlers that the application will use through DI
        private static IServiceCollection ConfigureHandlers(this IServiceCollection services)
        {
            services.AddScoped<PaymentHandler>();
            // This adds a service that will run in the background and send messages to the bus every 30 seconds for testing purposes
            // services.AddHostedService<BusSenderBackgroundService>();

            return services;
        }

        private static IServiceCollection AddMassTransitServices(this IServiceCollection services, IConfiguration configuration, Action<IBusRegistrationConfigurator> busConfigure)
        {
            var rabbitMQHostName = configuration["RabbitMQ:HostName"];
            rabbitMQHostName ??= "localhost";
            services.AddMassTransit(x =>
            {
                // Default settings
                x.SetDefaultEndpointNameFormatter();

                busConfigure(x);

                // This configures rabbitmq, should be environment variables later on.
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.UseNewtonsoftJsonSerializer();
                    cfg.UseNewtonsoftJsonDeserializer();

                    cfg.Host(rabbitMQHostName, "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }

        // This method is used to configure the endpoints that the application will use
        private static void ConfigureBusEndpoints(IBusRegistrationConfigurator configurator)
        {
            // Add all consumers here for DI. This will allow the consumers to be resolved by the DI container
            configurator.AddConsumer<OrderPlacedEventConsumer, OrderPlacedEventConsumerDefinition>();
        }
    }
}