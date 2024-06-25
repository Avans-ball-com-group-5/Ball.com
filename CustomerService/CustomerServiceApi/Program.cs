using CustomerServiceApi.Messaging;
using CustomerServiceApi.Services;
using MassTransit;

namespace CustomerServiceApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.ConfigureServices((hostContext, services) =>
        {
            services.AddMassTransitServices(ConfigureBusEndpoints);
            services.ConfigureHandlers();
        });

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

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

    private static IServiceCollection ConfigureHandlers(this IServiceCollection services)
    {
        services.AddScoped<CustomerHandler>();

        // This adds a service that will run in the background and send messages to the bus every 30 seconds for testing purposes
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
                cfg.UseDelayedRedelivery(r => r.Interval(5, TimeSpan.FromSeconds(30)));
                cfg.UseNewtonsoftJsonSerializer();
                cfg.UseNewtonsoftJsonDeserializer();
                cfg.Host("amqp://guest:guest@rabbitmq");
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
}

