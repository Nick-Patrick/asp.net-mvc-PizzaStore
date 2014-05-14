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


namespace PizzaShop1.Test.Controller
{
    [TestClass]
    public class CartControllerTest
    {

        [TestMethod]
        public void IndexActionLoads()
        {
            //Arrange
            Mock<IPizzaRepository> mock = new Mock<IPizzaRepository>();
            CartController controller = new CartController(mock.Object, null, null, null, null, null);
            Cart cart = new Cart();

            //Action
            ViewResult result = controller.Index(cart, null) as ViewResult;

            //Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod]
        public void CanAddToCart()
        {
            //Arrange
            Mock<IPizzaRepository> mock = new Mock<IPizzaRepository>();
            mock.Setup(p => p.Pizzas).Returns(new Pizza[]{
                new Pizza {PizzaId = 1, Name = "Pizza1"},
            }.AsQueryable());

            Cart cart = new Cart();
            CartController controller = new CartController(mock.Object, null, null, null, null, null);

            //Action
            controller.AddToCart(cart, 1, null);

            //Assert
            Assert.AreEqual(cart.Lines.Count(), 1);
        }

        [TestMethod]
        public void AddToCartGoesToShoppingList()
        {
            //Arrange
            Mock<IPizzaRepository> mock = new Mock<IPizzaRepository>();
            mock.Setup(p => p.Pizzas).Returns(new Pizza[]{
                new Pizza {PizzaId = 1, Name = "Pizza1"},
            }.AsQueryable());

            Cart cart = new Cart();
            CartController controller = new CartController(mock.Object, null, null, null, null, null);

            //Action
            RedirectToRouteResult result = controller.AddToCart(cart, 1, "testUrl");

            //Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "testUrl");
        }

        [TestMethod]
        public void CanViewCart()
        {
            //Arrange
            Cart cart = new Cart();
            Delivery delivery = new Delivery();
            CartController controller = new CartController(null, null, null, null, null, null);

            //Action
            CartIndexViewModel result = (CartIndexViewModel)controller.Index(cart, "testUrl").ViewData.Model;

            //Assert
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "testUrl");
        }

        [TestMethod]
        public void CanEnterValidVoucher()
        {
            //Arrange
            Cart cart = new Cart();
            CartController controller = new CartController(null, null, null, null, null, null);

            //Action
            String voucherResponse = controller.VoucherCheck("PD201401062");

            //Assert
            Assert.AreEqual("Voucher PD201401062 is a valid code. GREAT DEAL!", voucherResponse);
        }

        [TestMethod]
        public void CanEnterInvalidVoucher()
        {
            //Arrange
            Cart cart = new Cart();
            CartController controller = new CartController(null, null, null, null, null, null);

            //Action
            String voucherResponse = controller.VoucherCheck("PD1401062");

            //Assert
            Assert.AreEqual("Voucher 'PD1401062' is not a valid code.", voucherResponse);
        }

    }
}
