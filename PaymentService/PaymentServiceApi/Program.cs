using MassTransit;
using PaymentServiceApi.Consumers;
using PaymentServiceApi.Handlers;

namespace PaymentServiceApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Host
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddEnvironmentVariables();
            });

        var rabbitMQHostName = builder.Configuration["RabbitMQ:HostName"];
        rabbitMQHostName ??= "localhost";

        builder.Services.AddMassTransit(x =>
        {
            // Default settings
            x.SetDefaultEndpointNameFormatter();

            ConfigureBusEndpoints(x);

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

        builder.Services.ConfigureHandlers();

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
        services.AddScoped<PaymentHandler>();
        // This adds a service that will run in the background and send messages to the bus every 30 seconds for testing purposes
        // services.AddHostedService<BusSenderBackgroundService>();

        return services;
    }

    // This method is used to configure the endpoints that the application will use
    private static void ConfigureBusEndpoints(IBusRegistrationConfigurator configurator)
    {
        // Add all consumers here for DI. This will allow the consumers to be resolved by the DI container
        configurator.AddConsumer<OrderPlacedEventConsumer, OrderPlacedEventConsumerDefinition>();
    }
}