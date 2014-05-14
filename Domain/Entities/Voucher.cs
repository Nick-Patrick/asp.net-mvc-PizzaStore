using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop1.Domain.Entities
{
    public class Voucher
    {
        public int VoucherId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Criteria { get; set; }
        public string Discount { get; set; }
        public string DeliveryLimit { get; set; }
    }
}
