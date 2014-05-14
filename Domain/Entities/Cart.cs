using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaShop1.Domain.Concrete;
using PizzaShop1.Domain.Abstract;

namespace PizzaShop1.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> cartLine = new List<CartLine>();
        public int DeliveryId { get; set; }

        public void AddItem(Pizza pizza, int quantity)
        {
            CartLine line = cartLine
               .Where(p => p.Pizza.PizzaId == pizza.PizzaId)
               .FirstOrDefault();

            if (line == null)
            {
                cartLine.Add(new CartLine { Pizza = pizza, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Pizza pizza)
        {
            cartLine.RemoveAll(line => line.Pizza.PizzaId == pizza.PizzaId);
        }

        public decimal GetTotalValue()
        {
            return cartLine.Sum(e => e.Pizza.Price * e.Quantity);
        }

        public void Clear()
        {
            cartLine.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return cartLine; }
        }

        

    }

    public class CartLine
    {
        public Pizza Pizza { get; set; }
        public int Quantity { get; set; }
    }
}
