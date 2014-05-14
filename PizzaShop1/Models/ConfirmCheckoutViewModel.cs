using PizzaShop1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaShop1.Models
{
    public class ConfirmCheckoutViewModel
    {
        public UserProfile User { get; set; }
        public ICollection<Pizza> Pizzas { get; set; }
        public Order Order { get; set; }
        public ICollection<Topping> Toppings { get; set; }
    }
}