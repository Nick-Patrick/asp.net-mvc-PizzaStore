using PizzaShop1.Domain.Abstract;
using PizzaShop1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop1.Domain.Concrete
{
    public class EFPizzaToppingOrderRepository : IPizzaToppingOrderRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<PizzaToppingOrder> PizzaToppingOrders
        {
            get { return context.PizzaToppingOrders; }
        }

        public void SavePizzaToppingOrder(PizzaToppingOrder pizzaToppingOrder)
        {
            if (pizzaToppingOrder.PizzaToppingOrderId == 0)
            {
                context.PizzaToppingOrders.Add(pizzaToppingOrder);
            }
            else
            {
                context.Entry(pizzaToppingOrder).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
    }
}
