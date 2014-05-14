using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzaShop1.Domain.Entities;

namespace PizzaShop1.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }

    }
}