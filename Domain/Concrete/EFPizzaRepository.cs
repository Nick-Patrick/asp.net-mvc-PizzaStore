using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaShop1.Domain.Entities;
using PizzaShop1.Domain.Abstract;

namespace PizzaShop1.Domain.Concrete
{
    public class EFPizzaRepository : IPizzaRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Pizza> Pizzas
        {
            get { return context.Pizzas; }
        }
    }
}
