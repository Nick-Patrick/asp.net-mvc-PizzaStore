using PizzaShop1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaShop1.Models
{
    public class CartCheckoutViewModel
    {
        public Cart Cart { get; set; }
        public UserProfile User { get; set; }
        public ICollection<Delivery> DeliveryTypes { get; set; }
        public Voucher Voucher { get; set; }
    }
}