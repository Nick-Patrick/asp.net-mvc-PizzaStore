using PizzaShop1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop1.Domain.Abstract
{
    public interface IDeliveryRepository
    {
        IQueryable<Delivery> Deliveries { get; }
    }
}
