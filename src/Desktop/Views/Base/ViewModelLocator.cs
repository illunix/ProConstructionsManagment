using Autofac;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Modules;
using System;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;

namespace ProConstructionsManagment.Desktop.Views.Base
{
    public class ViewModelLocator : IViewModelLocator
    {
        private static readonly IContainer _container;

        static ViewModelLocator()
        {
            if (_container != null)
                throw new InvalidOperationException($"Cannot initialize {nameof(ViewModelLocator)} multiple times");

            var builder = new ContainerBuilder();

            builder.RegisterModule(new AssemblyScanningModule());

            builder.RegisterType<ViewModelLocator>().As<IViewModelLocator>();

            builder.RegisterType<ShellManager>().As<IShellManager>();

            _container = builder.Build();
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