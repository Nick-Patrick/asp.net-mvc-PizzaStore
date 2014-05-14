using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop1.Domain.Entities
{
    public class ToppingPizza
    {
        public int ToppingPizzaId { get; set; }
        public int Topping_ToppingId { get; set; }
        public int Pizza_PizzaId { get; set; }
    }
}
