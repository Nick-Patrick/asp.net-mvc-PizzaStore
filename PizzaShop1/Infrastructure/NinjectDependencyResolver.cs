using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaShop1.Domain.Abstract;
using PizzaShop1.Domain.Concrete;
using Ninject;
using Ninject.Syntax;

namespace PizzaShop1.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver 
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public IBindingToSyntax<T> Bind<T>()
        {
            return kernel.Bind<T>();
        }

        public IKernel Kernel
        {
            get { return kernel; }
        }

        private void AddBindings(){

            Bind<IPizzaRepository>().To<EFPizzaRepository>();
            Bind<IOrderlineRepository>().To<EFOrderlineRepository>();
            Bind<IOrderRepository>().To<EFOrderRepository>();
            Bind<IDeliveryRepository>().To<EFDeliveryRepository>();
            Bind<IVoucherRepository>().To<EFVoucherRepository>();
            Bind<IToppingRepository>().To<EFToppingRepository>();
            Bind<IPizzaToppingOrderRepository>().To<EFPizzaToppingOrderRepository>();
        }
    }
}