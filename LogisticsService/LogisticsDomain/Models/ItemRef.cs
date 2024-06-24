using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsDomain.Models
{
    public class ItemRef
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Amount { get; set; }
    }
}
