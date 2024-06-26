using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IPaymentRepository
    {
        public void AddPayment(Payment payment);
        public Payment GetPaymentById(Guid id);
        public void UpdatePayment(Payment payment);
    }
}
