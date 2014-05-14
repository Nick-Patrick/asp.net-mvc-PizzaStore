using PizzaShop1.Domain.Abstract;
using PizzaShop1.Domain.Concrete;
using PizzaShop1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop1.Domain.Concrete
{
    public class EFOrderRepository : IOrderRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Order> Orders
        {
            get { return context.Orders; }
        }

        public void SaveOrder(Order order)
        {
            if (order.OrderId == 0)
            {
                context.Orders.Add(order);
            }
            else
            {
                context.Entry(order).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
    }
}
