using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ProductEvent
    {
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
