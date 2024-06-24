using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; }
        public Guid PaymentId { get; set; }
        public List<ItemRef> Items { get; set; } = new();
        public LogisticsCompany? LogisticsCompany { get; set; }
        public Guid LogisticsCompanyId { get; set; }


        public override string ToString()
        {
            return $"Id: {Id}, CreatedAt: {CreatedAt}, PaymentId: {PaymentId}, Items: {string.Join(", ", Items.Select(i => $"{{ Id: {i.Id}, Amount: {i.Amount} }}"))}";
        }
    }
}
