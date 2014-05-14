using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaShop1.Domain.Abstract;
using PizzaShop1.Domain.Entities;
using PizzaShop1.Models;
using WebMatrix.WebData;
using System.Text.RegularExpressions;

namespace PizzaShop1.Controllers
{
    public class CartController : Controller
    {
        private IPizzaRepository repository;
        private IOrderlineRepository orderlineRepository;
        private IOrderRepository orderRepository;
        private IDeliveryRepository deliveryRepository;
        private IVoucherRepository voucherRepository;
        private IPizzaToppingOrderRepository pizzaToppingOrderRepository;

        public CartController(IPizzaRepository repo, IOrderlineRepository orderlineRepo, IOrderRepository orderRepo, IDeliveryRepository deliveryRepo, IVoucherRepository voucherRepo, IPizzaToppingOrderRepository pizzaToppingOrderRepo)
        {
            repository = repo;
            orderlineRepository = orderlineRepo;
            orderRepository = orderRepo;
            deliveryRepository = deliveryRepo;
            voucherRepository = voucherRepo;
            pizzaToppingOrderRepository = pizzaToppingOrderRepo;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl,
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int PizzaId, string returnUrl)
        {
            Pizza pizza = repository.Pizzas.FirstOrDefault(p => p.PizzaId == PizzaId);

            if (pizza != null)
            {
                if (pizza.Name == "Create Your Own")
                {
                    foreach (var topping in Session["ToppingList"] as List<Topping>)
                    {
                        pizza.Price += topping.Price;
                    }
                    cart.AddItem(pizza, 1);
                }
                else
                {
                    cart.AddItem(pizza, 1);
                }
                
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int PizzaId, string returnUrl)
        {
            Pizza pizza = repository.Pizzas.FirstOrDefault(p => p.PizzaId == PizzaId);

            if (pizza != null)
            {
                cart.RemoveLine(pizza);
            }
            return RedirectToAction("Index", new { returnUrl });
        }


        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        [Authorize]
        public ViewResult Checkout(Cart cart)
        {
            var context = new UsersContext();
            var username = User.Identity.Name;
            var user = context.UserProfiles.SingleOrDefault(u => u.UserName == username);

            return View(new CartCheckoutViewModel
            {
                User = user,
                Cart = cart,
                DeliveryTypes = deliveryRepository.Deliveries.ToList(),
                Voucher = null
            });
            
            //return View(user);
        }

       

        [Authorize]
        public ViewResult ConfirmCheckout(Cart cart, int deliveryId)
        {
            Delivery delivery = deliveryRepository.Deliveries.FirstOrDefault(d => d.DeliveryId == deliveryId);
            Order order = new Order();
            order.PriceBeforeVouchers = cart.GetTotalValue();
            order.UserId = WebSecurity.CurrentUserId;
            order.TimeSubmitted = DateTime.Now;
            
            order.DeliveryId = deliveryId;
            order.Subtotal = order.PriceBeforeVouchers + delivery.Cost;
            order.Total = order.Subtotal;
            order.Status = "Processed";
            orderRepository.SaveOrder(order);
            int orderId = orderRepository.Orders.FirstOrDefault(o => o.TimeSubmitted == order.TimeSubmitted).OrderId;
            List<Pizza> pizzas = new List<Pizza>();

            foreach(var cartLine in cart.Lines){
                for(int i=0;i<cartLine.Quantity;i++)
                {
                    Orderline orderline = new Orderline();
                    orderline.PizzaId = cartLine.Pizza.PizzaId;
                    orderline.OrderlinePrice = cartLine.Pizza.Price * cartLine.Quantity;
                    orderline.UserId = WebSecurity.CurrentUserId;
                    orderline.OrderId = order.OrderId;
                    orderlineRepository.SaveOrderline(orderline);
                    pizzas.Add(repository.Pizzas.FirstOrDefault(p => p.PizzaId == orderline.PizzaId));
                    Pizza pizza = repository.Pizzas.FirstOrDefault(p => p.PizzaId == orderline.PizzaId);
                    if (pizza.Name == "Create Your Own")
                    {
                        foreach (var topping in Session["ToppingList"] as List<Topping>)
                        {
                            PizzaToppingOrder pizzaToppingOrder = new PizzaToppingOrder();
                            pizzaToppingOrder.OrderlineId = orderline.OrderlineId;
                            pizzaToppingOrder.ToppingId = topping.ToppingId;
                            pizzaToppingOrderRepository.SavePizzaToppingOrder(pizzaToppingOrder);
                        }
                    }
                }
            }

            cart.Clear();
            ViewBag.PriceBeforeVouchers = order.PriceBeforeVouchers;
            
            ViewBag.DeliveryCost = delivery.Cost;
            ViewBag.PriceIncDelivery = order.PriceBeforeVouchers + delivery.Cost;
            ViewBag.DeliveryType = delivery.DeliveryType;
            
            var context = new UsersContext();
            var username = User.Identity.Name;
            var user = context.UserProfiles.SingleOrDefault(u => u.UserName == username);

            List<Topping> toppings = Session["ToppingList"] as List<Topping>;

            return View(new ConfirmCheckoutViewModel
            {
                User = user,
                Order = order,
                Pizzas =  pizzas, //orderlineRepository.Orderlines.Where(ol => ol.OrderId == order.OrderId).ToList()
                Toppings = toppings
            });
            //return View(user);
        }

        

        [Authorize]
        public ActionResult SaveCurrentOrder(Cart cart)
        {
            var orderlineQuery = orderlineRepository.Orderlines.Where(u => u.UserId == WebSecurity.CurrentUserId).ToList();
            foreach (var orderline in orderlineQuery)
            {
                if (orderline != null)
                {
                    orderlineRepository.ClearOrderline(orderline);
                }
            }

            for(int i = 0; i<cart.Lines.Count(); i++){
                
                var line = cart.Lines.ElementAt(i);
                for (int j = 0; j < line.Quantity; j++)
                {
                    Orderline orderline = new Orderline();
                    orderline.PizzaId = line.Pizza.PizzaId;
                    orderline.OrderlinePrice = line.Pizza.Price;
                    orderline.UserId = WebSecurity.CurrentUserId;
                    orderlineRepository.SaveOrderline(orderline);
                }
                
            }
            TempData["message"] = string.Format("Order saved");

            return RedirectToAction("Index");
        }

        public ActionResult SubmitVoucher(string voucherCode = "0")
        {

                    
             if (Request.IsAjaxRequest())
             {
                 //Voucher voucher;
                 //if (voucherCode != "")
                 //{
                     Voucher voucher = voucherRepository.Vouchers.FirstOrDefault(v => v.Code == voucherCode);
                 //}
                 //else
                // {
                 //    voucher = new Voucher();
                 //}
                  return PartialView("VoucherArea", voucher);
             }
             else
             {
                  return View();
             }
        }

        [Authorize]
        public ActionResult RetrieveSavedOrder(Cart cart)
        {
            var orderlineQuery = orderlineRepository.Orderlines.Where(u => u.UserId == WebSecurity.CurrentUserId).ToList();
            cart.Clear();
            foreach (var orderline in orderlineQuery)
            {
                if (orderline != null)
                {
                    if (orderline.OrderId == 0)
                    {
                        Pizza pizza = new Pizza();
                        pizza.PizzaId = orderline.PizzaId;
                        pizza.Name = repository.Pizzas.FirstOrDefault(p => p.PizzaId == pizza.PizzaId).Name;
                        pizza.Size = repository.Pizzas.FirstOrDefault(p => p.PizzaId == pizza.PizzaId).Size;
                        pizza.Price = orderline.OrderlinePrice;
                        cart.AddItem(pizza, 1);
                        TempData["message"] = string.Format("Order Retrieved");
                    }
                }
            }

            if(orderlineQuery.Count() == 0){
                TempData["message"] = string.Format("No Items Saved");               
            }
            return RedirectToAction("Index");
        }

        public String VoucherCheck(string VoucherCode)
        {
            string voucherCode = Regex.Replace(VoucherCode, @"<[^>]*>", String.Empty);

            String voucherResponse = "Voucher '" + voucherCode + "' is not a valid code.";
            if (voucherCode == "PD201401061" || voucherCode == "PD201401062" || voucherCode == "PD201401063" || voucherCode == "PD201401064" || voucherCode == "PD201401065" || voucherCode == "PD201401066")
            {
                 voucherResponse = "Voucher " + voucherCode + " is a valid code. GREAT DEAL!";
            }
            return voucherResponse;
        }

    }

    

    
}
