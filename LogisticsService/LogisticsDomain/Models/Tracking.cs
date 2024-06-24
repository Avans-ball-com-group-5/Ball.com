using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsDomain.Models
{
    public class Tracking
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Status Status { get; set; }
        public Order? Order { get; set; }
        public Guid OrderId { get; set; }
    }

    public enum Status
    {
        Unknown,
        Left_Ball_Com_Warehouse,
        Arrived_At_Delivery_Warehouse,
        Sorted,
        Delivering,
        Delivered
    }
}
