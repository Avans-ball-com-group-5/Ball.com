using MassTransit;
using OrderServiceApi.Consumers;
using OrderServiceApi.Handlers;
using OrderSQLInfrastructure;
using Microsoft.EntityFrameworkCore;
using Domain.Services;

namespace OrderServiceApi
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

            app.UseEndpoints(e =>
            {
                e.MapControllers();
            });

            app.Run();
        }

        private static IServiceCollection ConfigureHandlers(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("OrderDbContext");
            connectionString ??= "Server=localhost,1431;Database=OrderDB;User=sa;Password=Your_password123;TrustServerCertificate=True";
            Console.WriteLine(connectionString);
            services.AddDbContext<OrderEventDbContext>(options =>
                options.UseSqlServer(connectionString, c => c.MigrationsAssembly("OrderSQLInfrastructure")), ServiceLifetime.Scoped);
            Console.WriteLine("SQL Server injection worked");

            services.AddScoped<IOrderCommandHandler, OrderCommandHandler>();
            services.AddScoped<IOrderQueryHandler, OrderQueryHandler>();
            services.AddScoped<OrderEventHandler>();

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
                });
            });

            return services;
        }

        // This method is used to configure the endpoints that the application will use
        private static void ConfigureBusEndpoints(IBusRegistrationConfigurator configurator)
        {
            // Add all consumers here for DI. This will allow the consumers to be resolved by the DI container
            configurator.AddConsumer<PlaceOrderConsumer, PlaceOrderConsumerDefinition>();
            configurator.AddConsumer<PaymentCreatedConsumer, PaymentCreatedConsumerDefinition>();
            configurator.AddConsumer<OrderDenormalizerConsumer, OrderDenormalizerConsumerDefinition>();
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