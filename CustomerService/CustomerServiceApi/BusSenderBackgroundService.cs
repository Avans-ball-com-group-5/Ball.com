using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CustomerServiceApi
{
    public class BusSenderBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BusSenderBackgroundService> _logger;

        public BusSenderBackgroundService(IServiceProvider serviceProvider, ILogger<BusSenderBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var customerDataService = scope.ServiceProvider.GetRequiredService<CustomerServiceApi.Services.CustomerDataService>();
                        await customerDataService.ProcessCustomerDataAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error processing CSV data: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            } while (!stoppingToken.IsCancellationRequested);
        }
    }
}