using System;
using System.Linq;
using System.Reflection;
using GrowDT.Dependency;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace GrowDT.UnityInject.Infrastructure
{
    public class UnityIocManager: IIocManager
    {
        private readonly IUnityContainer _unityContainer;
        private readonly InjectionMember[] _interceptionInjectionMembers;

        public UnityIocManager(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
            _interceptionInjectionMembers = new InjectionMember[]
            {
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<AuditLoggingBehavior>()
            };
        }

        public void RegisterType<T, TImp>(DependencyLifeStyle liftStyle) where TImp : T
        {
            if (liftStyle == DependencyLifeStyle.Singleton)
            {
                _unityContainer.RegisterType<T, TImp>(new ContainerControlledLifetimeManager());
            }
            if (liftStyle == DependencyLifeStyle.Transient)
            {
                _unityContainer.RegisterType<T, TImp>(new TransientLifetimeManager());
            }
        }

        public void RegisterType(Type tType, Type impType, DependencyLifeStyle liftStyle)
        {
            if (liftStyle == DependencyLifeStyle.Singleton)
            {
                _unityContainer.RegisterType(tType, impType, new ContainerControlledLifetimeManager());
            }
            if (liftStyle == DependencyLifeStyle.Transient)
            {
                _unityContainer.RegisterType(tType, impType, new TransientLifetimeManager());
            }
        }

        public void RegisterType(Assembly assembly)
        {
            _unityContainer.RegisterTypes(AllClasses.FromAssemblies(assembly),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.Transient);
        }

        public void RegisterType(Assembly assembly, Func<Type, bool> predicate)
        {
            _unityContainer.RegisterTypes(AllClasses.FromAssemblies(assembly).Where(predicate),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.Transient);
        }

        public void RegisterInterceptionType<T, TImp>(DependencyLifeStyle liftStyle) where TImp: T
        {
            if (liftStyle == DependencyLifeStyle.Singleton)
            {
                _unityContainer.RegisterType<T, TImp>(new ContainerControlledLifetimeManager(), _interceptionInjectionMembers);
            }
            if (liftStyle == DependencyLifeStyle.Transient)
            {
                _unityContainer.RegisterType<T, TImp>(new TransientLifetimeManager(), _interceptionInjectionMembers);
            }
        }

        public void RegisterInterceptionType(Type tType, Type impType, DependencyLifeStyle liftStyle)
        {
            if (liftStyle == DependencyLifeStyle.Singleton)
            {
                _unityContainer.RegisterType(tType, impType, new ContainerControlledLifetimeManager(), _interceptionInjectionMembers);
            }
            if (liftStyle == DependencyLifeStyle.Transient)
            {
                _unityContainer.RegisterType(tType, impType, new TransientLifetimeManager(), _interceptionInjectionMembers);
            }
        }

        public void RegisterInterceptionType(Assembly assembly)
        {
            _unityContainer.RegisterTypes(AllClasses.FromAssemblies(assembly),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.Transient, t => _interceptionInjectionMembers);
        }

        public void RegisterInterceptionType(Assembly assembly, Func<Type, bool> predicate)
        {
            _unityContainer.RegisterTypes(AllClasses.FromAssemblies(assembly).Where(predicate),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.Transient, t => _interceptionInjectionMembers);
        }

        public T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }
    }
}
