using System;
using Autofac;
using Autofac.Features.ResolveAnything;

namespace AInfrastructure
{
    public interface IClassResolver
    {
        T Get<T>() where T : class;
    }

    public class AutoFacClassResolver : IClassResolver
    {

        protected IContainer _container = null;

        public virtual T Get<T>() 
            where T : class
        {
            if(_container == null)
            {
                throw new Exception("Container is null");
            }

            return _container.Resolve<T>();
        }

        public AutoFacClassResolver() 
            => Init();

        void Init()
        {
            var builder = new ContainerBuilder();

            // This is an important step that enables autofac
            // to resolve any concrete type that may need 
            // instances of classes it knows how to resolve.
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            _container = Build(builder).Build();
        }

        protected virtual ContainerBuilder Build(ContainerBuilder builder)
            => builder;
    }

    public class BaseClassResolver : AutoFacClassResolver
    {
        protected override ContainerBuilder Build(ContainerBuilder builder)
        {
            builder = base.Build(builder);

            builder.RegisterType<BaseLogger>().As<ILogger>().SingleInstance();
            builder.RegisterType<StorageIo>().As<IStorageIo>().SingleInstance();

            return builder;
        }
    }
}
