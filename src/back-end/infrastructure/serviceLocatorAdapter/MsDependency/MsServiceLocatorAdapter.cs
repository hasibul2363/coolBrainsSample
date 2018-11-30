using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace SuitSupply.Infrastructure.SLAdapter.MsDependency
{
    public class MsServiceLocatorAdapter : CommonServiceLocator.IServiceLocator
    {
        public IServiceProvider ServiceProvider { get; set; }
        public MsServiceLocatorAdapter(IServiceCollection serviceCollection)
        {
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public MsServiceLocatorAdapter(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public object GetService(Type serviceType)
        {
            var instance = ServiceProvider.GetService(serviceType);
            if (instance == null)
                throw new Exception($"No registration has found for type {serviceType.ToString()}, Please add registration for it.");
            return instance;
        }

        public object GetInstance(Type serviceType)
        {
            return GetService(serviceType);
        }

        public object GetInstance(Type serviceType, string key)
        {
            return GetService(serviceType);
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return ServiceProvider.GetServices(serviceType);
        }

        public TService GetInstance<TService>()
        {
            var instance = ServiceProvider.GetService<TService>();
            if (instance == null)
                throw new Exception($"No registration has found for type {typeof(TService).ToString()}, Please add registration for it.");
            return instance;
        }

        public TService GetInstance<TService>(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return ServiceProvider.GetServices<TService>();
        }
    }
}
