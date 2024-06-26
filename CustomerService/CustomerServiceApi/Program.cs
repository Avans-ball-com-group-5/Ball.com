using CustomerServiceApi.Messaging;
using CustomerServiceApi.Services;
using CustomerSQLInfrastructure;
using Domain.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        var host = CreateHostBuilder(args).Build();
        MigrateDatabase(host);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

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

    public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        /*.ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.AddEnvironmentVariables();
        })*/
        .ConfigureServices((hostContext, services) =>
        {
            services.AddMassTransitServices(ConfigureBusEndpoints, hostContext.Configuration);
            services.ConfigureHandlers(hostContext.Configuration);
        });

    private static IServiceCollection ConfigureHandlers(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CustomerDbContext");
        connectionString ??= "Server=localhost,1436;Database=CustomerDb;User=sa;Password=Your_password123;TrustServerCertificate=True";
        services.AddDbContext<CustomerDbContext>(options =>
                    options.UseSqlServer(connectionString, c => c.MigrationsAssembly("CustomerSQLInfrastructure")), ServiceLifetime.Scoped);

        services.AddScoped<CustomerHandler>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<CustomerDataService>();
        services.AddHostedService<BusSenderBackgroundService>();

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
        configurator.AddConsumer<RegisterCustomerServiceTicketConsumer, RegisterCustomerServiceTicketConsumerDefinition>();
    }

    private static void MigrateDatabase(IHost host)
    {
        Console.WriteLine("Starting migration");
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var dbContext = services.GetRequiredService<CustomerDbContext>();
            dbContext.Database.Migrate();
            Console.WriteLine("Database migration successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
        }
    }
}

