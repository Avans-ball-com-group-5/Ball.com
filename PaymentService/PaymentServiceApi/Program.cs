using MassTransit;
using Microsoft.EntityFrameworkCore;
using PaymentServiceApi.Consumers;
using PaymentSQLInfrastructure;
using PaymentServiceApi.Controllers;
using PaymentServiceApi.Handlers;
using Domain.Services;


namespace PaymentServiceApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.ConfigureServices((hostContext, services) =>
        {
            services.AddMassTransitServices(ConfigureBusEndpoints, hostContext.Configuration);
            services.ConfigureHandlers(hostContext.Configuration);
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        MigrateDatabase(app);

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
        var connectionString = configuration.GetConnectionString("PaymentDbContext");
        connectionString ??= "Server=localhost,1436;Database=PaymentDb;User=sa;Password=Your_password123;TrustServerCertificate=True";
        services.AddDbContext<PaymentDbContext>(options =>
                    options.UseSqlServer(connectionString, c => c.MigrationsAssembly("PaymentSQLInfrastructure")), ServiceLifetime.Scoped);

        services.AddScoped<PaymentHandler>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        //services.AddHostedService<BusSenderBackgroundService>();

        return services;
    }

    private static IServiceCollection AddMassTransitServices(this IServiceCollection services, Action<IBusRegistrationConfigurator> busConfigure, IConfiguration configuration)
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
                cfg.UseDelayedRedelivery(r => r.Interval(5, TimeSpan.FromSeconds(30)));
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
        configurator.AddConsumer<AfterPayConsumer, AfterPayConsumerDefinition>();
    }

    private static void MigrateDatabase(IHost host)
    {
        Console.WriteLine("Starting migration");
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var dbContext = services.GetRequiredService<PaymentDbContext>();
            dbContext.Database.Migrate();
            Console.WriteLine("Database migration successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
        }
    }
}