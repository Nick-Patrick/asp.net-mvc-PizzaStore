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
    public class PizzaControllerTest
    {
        [TestMethod]
        public void ListActionLoads()
        {
            //Arrange
            Mock<IPizzaRepository> mock = new Mock<IPizzaRepository>();
            PizzaController controller = new PizzaController(mock.Object, null);

            //Action
            ViewResult result = controller.List() as ViewResult;

            //Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod]
        public void CanCreateMenu()
        {
            //Arrange
            Mock<IPizzaRepository> mock = new Mock<IPizzaRepository>();
            mock.Setup(m => m.Pizzas).Returns(new Pizza[]{
                new Pizza { PizzaId = 1, Name = "Pizza1", Status = "Available"},
                new Pizza { PizzaId = 2, Name = "Pizza2", Status = "Available"}
            }.AsQueryable());

            PizzaController controller = new PizzaController(mock.Object, null);

            //Action
            Pizza[] results = ((PizzaListViewModel)controller.List().Model).Pizzas.ToArray();

            //Assert
            Assert.AreEqual(results.Length, 2);
        }

        [TestMethod]
        public void CanFilterPizzaByStatus()
        {
            //Arrange
            Mock<IPizzaRepository> mock = new Mock<IPizzaRepository>();
            mock.Setup(m => m.Pizzas).Returns(new Pizza[]{
                new Pizza { PizzaId = 1, Name = "Pizza1", Status = "Available"},
                new Pizza { PizzaId = 2, Name = "Pizza2", Status = "NotAvailable"}
            }.AsQueryable());

            PizzaController controller = new PizzaController(mock.Object, null);

            //Action
            Pizza[] result = ((PizzaListViewModel)controller.List().Model).Pizzas.ToArray();

            //Assert
            Assert.AreEqual(result.Length, 1);
            Assert.IsTrue(result[0].Name == "Pizza1" && result[0].Status == "Available");
        }
    }
}
