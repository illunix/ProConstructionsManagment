using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Modules;
using System;
using System.Net.Http;

namespace ProConstructionsManagment.Desktop.Views.Base
{
    public class ViewModelLocator : IViewModelLocator
    {
        private static readonly IContainer _container;

        static ViewModelLocator()
        {
            if (_container != null)
                throw new InvalidOperationException($"Cannot initialize {nameof(ViewModelLocator)} multiple times");

            var services = new ServiceCollection();

            services.AddHttpClient();

            var builder = new ContainerBuilder();

            builder.Populate(services);

            builder.RegisterModule(new AssemblyScanningModule());

            builder.RegisterType<ViewModelLocator>().As<IViewModelLocator>();

            builder.RegisterType<ShellManager>().As<IShellManager>();

            _container = builder.Build();

            _container.Resolve<IHttpClientFactory>();
        }

        T IViewModelLocator.Get<T>()
        {
            return Resolve<T>();
        }

        public static T Get<T>()
        {
            return Resolve<T>();
        }

        private static T Resolve<T>()
        {
            if (_container == null)
                throw new NullReferenceException($"{nameof(ViewModelLocator)} not initialized");

            return _container.Resolve<T>();
        }
    }
}