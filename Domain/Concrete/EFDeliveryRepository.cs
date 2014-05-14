using PizzaShop1.Domain.Abstract;
using PizzaShop1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop1.Domain.Concrete
{
    public class EFDeliveryRepository : IDeliveryRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Delivery> Deliveries
        {
            get { return context.Deliveries; }
        }
    }
}
