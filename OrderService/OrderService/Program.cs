using Microsoft.Extensions.Hosting;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Consumers;
using OrderService.Handlers;
using OrderSQLInfrastructure;
using Microsoft.EntityFrameworkCore;
using OrderService.Services;
using OrderDomain.Services;

namespace OrderService
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
                    services.AddMassTransitServices(ConfigureBusEndpoints);
                    services.ConfigureHandlers();
                });

        // This method is used to configure the handlers that the application will use through DI
        private static IServiceCollection ConfigureHandlers(this IServiceCollection services)
        {
            services.AddScoped<OrderHandler>();
            services.AddScoped<IOrderRepository, OrderEventRepository>();
            // This adds a service that will run in the background and send messages to the bus every 30 seconds for testing purposes
            //services.AddHostedService<BusSenderBackgroundService>();
            services.AddDbContext<OrderEventDbContext>(options =>
                options.UseSqlServer("Server=localhost,1435;Database=YourDatabaseName;User Id=sa;Password=Your_password123;TrustServerCertificate=True", c => c.MigrationsAssembly("OrderSQLInfrastructure")), ServiceLifetime.Scoped);
            services.AddHostedService<BusSenderBackgroundService>();
            return services;
        }

        private static IServiceCollection AddMassTransitServices(this IServiceCollection services, Action<IBusRegistrationConfigurator> busConfigure)
        {
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

        // This method is used to configure the endpoints that the application will use
        private static void ConfigureBusEndpoints(IBusRegistrationConfigurator configurator)
        {
            // Add all consumers here for DI. This will allow the consumers to be resolved by the DI container
            configurator.AddConsumer<PlaceOrderConsumer, PlaceOrderConsumerDefinition>();
            configurator.AddConsumer<PaymentCompletedConsumer, PaymentCompletedConsumerDefinition>();
        }
    }
}
