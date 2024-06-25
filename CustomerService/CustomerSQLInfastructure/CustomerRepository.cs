using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSQLInfastructure
{
    public class CustomerRepository : ICustomerRepository
    {
        public Customer GetCustomeryId(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public void SaveCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
