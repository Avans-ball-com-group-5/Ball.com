using Domain;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProductSQLInfrastructure;

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
            services.AddMassTransitServices(hostContext.Configuration);
            services.ConfigureHandlers(hostContext.Configuration);
        });

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
        var connectionString = configuration.GetConnectionString("ProductDbContext");
        connectionString ??= "Server=localhost,1438;Database=ProductDb;User=sa;Password=Your_password123;TrustServerCertificate=True";
        Console.WriteLine(connectionString);
        services.AddDbContext<ProductDbContext>(options =>
            options.UseSqlServer(connectionString, c => c.MigrationsAssembly("ProductSQLInfrastructure")), ServiceLifetime.Scoped);
        Console.WriteLine("SQL Server injection worked");

        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }

    private static IServiceCollection AddMassTransitServices(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMQHostName = configuration["RabbitMQ:HostName"];
        rabbitMQHostName ??= "localhost";
        Console.WriteLine("rabbit hostname: " + rabbitMQHostName);
        services.AddMassTransit(x =>
        {
            // Default settings
            x.SetDefaultEndpointNameFormatter();

            // This configures rabbitmq, should be environment variables later on.
            x.UsingRabbitMq((context, cfg) =>
            {
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

    // Method to perform database migration
    private static void MigrateDatabase(IHost host)
    {
        Console.WriteLine("Starting migration");
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var dbContext = services.GetRequiredService<ProductDbContext>();
            dbContext.Database.Migrate();
            Console.WriteLine("Database migration successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
        }
    }
}