using PizzaShop1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaShop1.Models
{
    public class CustomToppingViewModel
    {
        public ICollection<Topping> Toppings { get; set; }
        public decimal ToppingsPrice { get; set; }
    }
}