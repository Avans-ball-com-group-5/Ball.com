using Microsoft.Extensions.Hosting;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Consumers;
using OrderService.Handlers;
using OrderSQLInfrastructure;
using Microsoft.EntityFrameworkCore;
using OrderService.Services;
using OrderDomain.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace OrderService
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            // Run database migration before starting the application
            MigrateDatabase(host);
            host.Run();
        }

        // This method is used to configure the host and services that the application will use, including consumers(endpoints)
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransitServices(hostContext.Configuration, ConfigureBusEndpoints);
                    services.ConfigureHandlers(hostContext.Configuration);
                });

        // This method is used to configure the handlers that the application will use through DI
        private static IServiceCollection ConfigureHandlers(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("OrderDbContext");
            Console.WriteLine(connectionString);
            services.AddDbContext<OrderEventDbContext>(options =>
                options.UseSqlServer(connectionString, c => c.MigrationsAssembly("OrderSQLInfrastructure")), ServiceLifetime.Scoped);
            Console.WriteLine("SQL Server injection worked");

            services.AddScoped<OrderHandler>();
            services.AddScoped<IOrderRepository, OrderEventRepository>();
            return services;
        }

        private static IServiceCollection AddMassTransitServices(this IServiceCollection services, IConfiguration configuration, Action<IBusRegistrationConfigurator> busConfigure)
        {
            var rabbitMQHostName = configuration["RabbitMQ:HostName"]; // Read from configuration
            Console.WriteLine("rabbit hostname: " + rabbitMQHostName);
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
            configurator.AddConsumer<PlaceOrderConsumer, PlaceOrderConsumerDefinition>();
            configurator.AddConsumer<PaymentCompletedConsumer, PaymentCompletedConsumerDefinition>();
        }

        // Method to perform database migration
        private static void MigrateDatabase(IHost host)
        {
            Console.WriteLine("Starting migration");
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var dbContext = services.GetRequiredService<OrderEventDbContext>();
                dbContext.Database.Migrate();
                Console.WriteLine("Database migration successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
            }
        }
    }
}
