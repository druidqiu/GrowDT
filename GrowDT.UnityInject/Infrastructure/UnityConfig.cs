using System;
using GrowDT.Dependency;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace GrowDT.UnityInject.Infrastructure
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    internal class UnityConfig
    {
        #region Unity Container
        private static readonly Lazy<IUnityContainer> unityContainer = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            container.AddNewExtension<Interception>();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return unityContainer.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        private static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container.RegisterType<IIocManager, UnityIocManager>(new ContainerControlledLifetimeManager(), new InjectionConstructor(container));
            IocManagerFactory.Initialize(container.Resolve<IIocManager>());

            ConfigureModuleStart.Inject();
        }
    }
}
