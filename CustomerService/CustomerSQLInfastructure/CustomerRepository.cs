using Domain;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSQLInfrastructure
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _context;
        public CustomerRepository(CustomerDbContext context)
        {
            _context = context;
        }

        public Customer GetCustomeryId(Guid orderId)
        {
            return _context.Customers.FirstOrDefault(x => x.Id == orderId) ?? null;
        }

        public void SaveCustomer(Customer customer)
        {
            var databaseCustomer = _context.Customers.FirstOrDefault(x => x.Phone == customer.Phone);

            if (databaseCustomer == null)
            {
                _context.Add(customer);
            }
            else
            {
                bool hasChanges = false;

                if (databaseCustomer.CompanyName != customer.CompanyName)
                {
                    databaseCustomer.CompanyName = customer.CompanyName;
                    hasChanges = true;
                }
                if (databaseCustomer.FirstName != customer.FirstName)
                {
                    databaseCustomer.FirstName = customer.FirstName;
                    hasChanges = true;
                }
                if (databaseCustomer.LastName != customer.LastName)
                {
                    databaseCustomer.LastName = customer.LastName;
                    hasChanges = true;
                }
                if (databaseCustomer.Address != customer.Address)
                {
                    databaseCustomer.Address = customer.Address;
                    hasChanges = true;
                }

                if (hasChanges)
                {
                    Console.WriteLine("A record has been updated...");
                    _context.Update(databaseCustomer);
                }
            }

            _context.SaveChanges();
        }
    }
}

