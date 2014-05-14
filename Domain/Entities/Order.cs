using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop1.Domain.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime TimeSubmitted { get; set; }
        public int UserId { get; set; }
        public int DeliveryId { get; set; }
        public decimal PriceBeforeVouchers { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }

        public Delivery Delivery { get; set; }
    }
}
