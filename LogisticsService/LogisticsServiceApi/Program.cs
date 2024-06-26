using Domain.Services;
using LogisticsServiceApi.Consumers;
using LogisticsServiceApi.Handlers;
using LogisticsSQLInfrastructure;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace LogisticsServiceApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddEnvironmentVariables();
            })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransitServices(hostContext.Configuration, ConfigureBusEndpoints);
                    services.ConfigureHandlers(hostContext.Configuration);
                });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            MigrateDatabase(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }

        private static IServiceCollection ConfigureHandlers(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("LogisticsDbContext");
            connectionString ??= "Server=localhost,1435;Database=LogisticsDb;User=sa;Password=Your_password123;TrustServerCertificate=True";
            services.AddDbContext<LogisticsDbContext>(options =>
                        options.UseSqlServer(connectionString, c => c.MigrationsAssembly("LogisticsSQLInfrastructure")), ServiceLifetime.Scoped);

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