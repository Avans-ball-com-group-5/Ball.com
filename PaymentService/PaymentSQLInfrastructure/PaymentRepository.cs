using Domain;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSQLInfrastructure
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentDbContext _context;
        public PaymentRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public void AddPayment(Payment payment)
        {
            _context.Add(payment);
            _context.SaveChanges();
        }

        public Payment GetPaymentById(Guid id)
        {
            return _context.Payments.FirstOrDefault(x => x.Id == id) ?? throw new Exception();
        }

        public void UpdatePayment(Payment payment)
        {
            _context.Update(payment);
            _context.SaveChanges();
        }
    }
}
