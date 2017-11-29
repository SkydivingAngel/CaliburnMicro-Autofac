using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Autofac.Features.AttributeFilters;
using Caliburn.Micro;
using IContainer = Autofac.IContainer;
using Autofac.Core;
using Autofac.Features.ResolveAnything;

namespace WpfApp1
{
    using System.Collections.ObjectModel;
    using System.Reflection;

    public class AutofacBootstrapper : BootstrapperBase
    {
        private static IContainer container;

        public static IContainer Container => container;

        public AutofacBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            string fileName01 = File.ReadAllLines("QualeUso.txt").First();
            string fileName02 = File.ReadAllLines("QualeUso.txt").Last();

            builder.RegisterType<Repo>()
                .Named<IRepo>("Uno")
                .WithParameter("nome", "Foo");

            builder.RegisterType<Repo>()
                .Named<IRepo>("Due")
                .WithParameter("nome", "Barr");

            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                .Where(t => t.Name != null)
                .WithParameter(
                    (pi, c) =>
                    {
                        return pi.ParameterType == typeof(IRepo) && pi.Name == "Uno";
                    },
                    (pi, c) => c.ResolveNamed<IRepo>("Uno"))
                .Where(t => t.Name != null)
                .WithParameter(
                    (pi, c) => pi.ParameterType == typeof(IRepo) && pi.Name == "Due",
                    (pi, c) => c.ResolveNamed<IRepo>("Due"));

            builder.RegisterType<SecondViewModel>().Named<IScreen>("Second");


            builder.Register<IWindowManager>(c => new WindowManager()).InstancePerDependency();
            builder.Register<IEventAggregator>(c => new EventAggregator()).InstancePerDependency();

            ConfigureContainer(builder);
            container = builder.Build();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
            base.OnStartup(sender, e);
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                if (Container.IsRegistered(serviceType))
                    return Container.Resolve(serviceType);
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", key ?? serviceType.Name));
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return Container.Resolve(typeof(IEnumerable<>).MakeGenericType(serviceType)) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance)
        {
            Container.InjectProperties(instance);
        }

        protected virtual void ConfigureContainer(ContainerBuilder builder)
        {
        }
    }

    public enum Database { Uno, Due, Tre }
}
