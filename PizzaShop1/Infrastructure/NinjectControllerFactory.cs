using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using System.Web.Routing;
using Moq;
using PizzaShop1.Domain.Abstract;
using PizzaShop1.Domain.Entities;
using PizzaShop1.Domain.Concrete;

/*namespace PizzaShop1.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IPizzaRepository>().To<EFPizzaRepository>();
            ninjectKernel.Bind<IOrderLineRepository>().To<EFOrderLineRepository>();
        }
    }
}*/