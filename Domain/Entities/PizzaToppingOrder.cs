using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop1.Domain.Entities
{
    public class PizzaToppingOrder
    {
        public int PizzaToppingOrderId { get; set; }
        public int OrderlineId { get; set; }
        public int ToppingId { get; set; }
    }
}
