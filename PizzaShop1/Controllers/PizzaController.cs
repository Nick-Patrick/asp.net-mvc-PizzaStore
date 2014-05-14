using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaShop1.Domain.Abstract;
using PizzaShop1.Models;
using PizzaShop1.Domain.Entities;
using System.Data.Entity;

namespace PizzaShop1.Controllers
{
    public class PizzaController : Controller
    {
        private IPizzaRepository repository;
        private IToppingRepository toppingRepository;
        private List<Topping> toppingList = new List<Topping>();

        public PizzaController(IPizzaRepository pizzaRepository, IToppingRepository toppingRepo)
        {
            repository = pizzaRepository;
            toppingRepository = toppingRepo;
        }

        public ViewResult List()
        {
            PizzaListViewModel viewModel = new PizzaListViewModel
            {
                Pizzas = repository.Pizzas
                    .Where(p => p.Status == "Available")
            };


            return View(viewModel);
        }

        public ViewResult ViewPizza(Pizza pizza)
        {
            Pizza pizzaDetails;
            var pizzas = repository.Pizzas.Include("PizzaToppings");
            pizzaDetails = pizzas.FirstOrDefault(p => p.PizzaId == pizza.PizzaId);
            var allToppings = toppingRepository.Toppings.Where(t => t.Size == pizza.Size).ToList();

            PizzaSelectViewModel viewModel = new PizzaSelectViewModel
            {
                Pizza = pizzaDetails,
                Toppings = allToppings
            };

            toppingList = new List<Topping>();
            Session["ToppingList"] = new List<Topping>();
            return View(viewModel);
        }

        public PartialViewResult AddCustomTopping(int toppingId)
        {
            Topping topping = toppingRepository.Toppings.FirstOrDefault(t => t.ToppingId == toppingId);
            List<Topping> toppingsTemp = new List<Topping>();
           // toppingsTemp = (List<Topping>)Session["ToppingList"];
            toppingsTemp.Add(topping);
            
            foreach (Topping eachTopping in Session["ToppingList"] as List<Topping>)
            {
                toppingList.Add(eachTopping);
            }
            toppingList.Add(topping);
            Session["ToppingList"] = toppingList;

            decimal toppingsPrice = 0;
            foreach(var toppingPrice in toppingList){
                toppingsPrice += toppingPrice.Price;
            }
            CustomToppingViewModel viewModel = new CustomToppingViewModel
            {
                Toppings = toppingList,
                ToppingsPrice = toppingsPrice
            };
            
            return PartialView(viewModel);
        }
       
    }
}
