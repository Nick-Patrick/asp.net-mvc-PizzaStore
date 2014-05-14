using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzaShop1.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop1.Models
{
    public class PizzaListViewModel
    {
        [Key]
        public IEnumerable<Pizza> Pizzas { get; set; }
        public string CurrentStatus { get; set; }
    }
}