using System;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaShop1.Domain.Entities;
using PizzaShop1.Domain.Concrete;
using PizzaShop1.Domain.Abstract;
using Moq;
using System.Linq;
using PizzaShop1.Controllers;
using PizzaShop1.Models;
using System.Collections.Generic;
using System.Web.Mvc;


namespace PizzaShop1.Test.Model
{
    [TestClass]
    public class CartTest
    {
        [TestMethod]
        public void CanAddANewLine()
        {
            //Arrange
            Pizza p1 = new Pizza { PizzaId = 1, Name = "Pizza1" };
            Pizza p2 = new Pizza { PizzaId = 2, Name = "Pizza2" };
            Cart cart = new Cart();

            //Action
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            CartLine[] results = cart.Lines.ToArray();

            //Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[1].Pizza, p2);
        }

        [TestMethod]
        public void CanAddQuantityToExistingLine()
        {
            //Arrange
            Pizza p1 = new Pizza { PizzaId = 1, Name = "Pizza1" };
            Pizza p2 = new Pizza { PizzaId = 2, Name = "Pizza2" };
            Cart cart = new Cart();

            //Action
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 5);
            CartLine[] results = cart.Lines.OrderBy(p => p.Pizza.PizzaId).ToArray();

            //Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity, 6);
            Assert.AreEqual(results[1].Quantity, 1);
        }

        [TestMethod]
        public void CanRemoveLine()
        {
            //Arrange
            Pizza p1 = new Pizza { PizzaId = 1, Name = "Pizza1" };
            Pizza p2 = new Pizza { PizzaId = 2, Name = "Pizza2" };
            Pizza p3 = new Pizza { PizzaId = 3, Name = "Pizza3" };
            Cart cart = new Cart();

            //Action
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 2);
            cart.AddItem(p1, 5);
            cart.AddItem(p3, 3);
            cart.RemoveLine(p1);

            //Assert
            Assert.AreEqual(cart.Lines.Count(), 2);
            Assert.AreEqual(cart.Lines.Where(p => p.Pizza == p1).Count(), 0);
        }

        [TestMethod]
        public void CalcCartTotal()
        {
            //Arrange
            Pizza p1 = new Pizza { PizzaId = 1, Name = "Pizza1", Price = 5M };
            Pizza p2 = new Pizza { PizzaId = 2, Name = "Pizza2", Price = 3.50M };
            Pizza p3 = new Pizza { PizzaId = 3, Name = "Pizza3", Price = 51M };
            Cart cart = new Cart();

            //Action
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 2);
            cart.AddItem(p3, 1);
            decimal result = cart.GetTotalValue();

            //Assert
            Assert.AreEqual(result, 63M);
        }

        [TestMethod]
        public void CanClearCart()
        {
            //Arrange
            Pizza p1 = new Pizza { PizzaId = 1, Name = "Pizza1", Price = 5M };
            Pizza p2 = new Pizza { PizzaId = 2, Name = "Pizza2", Price = 3.50M };
            Pizza p3 = new Pizza { PizzaId = 3, Name = "Pizza3", Price = 51M };
            Cart cart = new Cart();

            //Action
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 2);
            cart.AddItem(p3, 1);
            cart.Clear();

            //Assert
            Assert.AreEqual(cart.Lines.Count(), 0);
        }
    }
}
