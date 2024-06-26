using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Domain;
using Domain.Services;
using Microsoft.Extensions.Logging;

namespace CustomerServiceApi.Services
{
    public class CustomerDataService
    {
        private readonly ILogger<CustomerDataService> _logger;
        private readonly string _csvUrl = "https://marcavans.blob.core.windows.net/solarch/fake_customer_data_export.csv?sv=2023-01-03&st=2024-06-14T10%3A31%3A07Z&se=2032-06-15T10%3A31%3A00Z&sr=b&sp=r&sig=q4Ie3kKpguMakW6sbcKl0KAWutzpMi747O4yIr8lQLI%3D";
        private readonly ICustomerRepository _customerRepository;

        public CustomerDataService(ILogger<CustomerDataService> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public async Task<List<Customer>> ReadCustomerFromCsvAsync()
        {
            List<Customer> customerDataList = new List<Customer>();

            try
            {
                using (var httpClient = new HttpClient())
                using (var response = await httpClient.GetAsync(_csvUrl))
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                {
                    await reader.ReadLineAsync();
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        var values = line.Split(',');

                        // Assuming CSV format is fixed: Company Name,First Name,Last Name,Phone Number,Address
                        if (values.Length >= 5)
                        {
                            var companyName = values[0];
                            var firstName = values[1];
                            var lastName = values[2];
                            var phoneNumber = values[3];
                            var address = values[4];

                            var customerData = new Customer
                            {
                                CompanyName = companyName,
                                FirstName = firstName,
                                LastName = lastName,
                                Phone = phoneNumber,
                                Address = address,
                            };

                            customerDataList.Add(customerData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error reading CSV data: {ex.Message}");
            }

            return customerDataList;
        }

        public async Task ProcessCustomerDataAsync()
        {
            var customerData = await ReadCustomerFromCsvAsync();

            foreach (var customer in customerData)
            {
                _customerRepository.SaveCustomer(customer);
            }
        }
    }
}
