using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LogisticsService.Handlers;
using LogisticsService.Consumers;
using LogisticsSQLInfrastructure;
using Microsoft.EntityFrameworkCore;
using Domain.Services;
using Microsoft.Extensions.Configuration;

namespace LogisticsService
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
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
            var connectionString = configuration.GetConnectionString("LogisticsDbContext");
            connectionString ??= "Server=localhost,1435;Database=LogisticsDb;User=sa;Password=Your_password123;TrustServerCertificate=True";
            services.AddDbContext<LogisticsDbContext>(options =>
                        options.UseSqlServer(connectionString, c => c.MigrationsAssembly("LogisticsSQLInfrastructure")),ServiceLifetime.Scoped);

            services.AddScoped<ILogisticsRepository, LogisticsRepository>();
            services.AddScoped<ITrackingRepository, TrackingRepository>();
            services.AddScoped<LogisticsHandler>();
            //services.AddHostedService<BusSenderBackgroundService>();

            return services;
        }

        private static IServiceCollection AddMassTransitServices(this IServiceCollection services, IConfiguration configuration, Action<IBusRegistrationConfigurator> busConfigure)
        {
            var rabbitMQHostName = configuration["RabbitMQ:HostName"];
            rabbitMQHostName ??= "localhost";
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
                    // Use BusSenderBackgroundService
                });
            });
            return services;
        }

        // This method is used to configure the endpoints that the application will use
        private static void ConfigureBusEndpoints(IBusRegistrationConfigurator configurator)
        {
            // Add all consumers here for DI. This will allow the consumers to be resolved by the DI container
            configurator.AddConsumer<OrderReadyForShippingConsumer, OrderReadyForShippingDefinition>();
            configurator.AddConsumer<LogisticSelectionConsumer, LogisticSelectionDefinition>();
            configurator.AddConsumer<OrderTrackingConsumer, OrderTrackingDefinition>();
        }

        private static void MigrateDatabase(IHost host)
        {
            Console.WriteLine("Starting migration");
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var dbContext = services.GetRequiredService<LogisticsDbContext>();
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