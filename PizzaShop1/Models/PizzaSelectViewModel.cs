using PizzaShop1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaShop1.Models
{
    public class PizzaSelectViewModel
    {

            public Pizza Pizza { get; set; }
            public ICollection<Topping> Toppings { get; set; }

        
    }
}