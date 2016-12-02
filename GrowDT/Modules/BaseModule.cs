using GrowDT.Dependency;

namespace GrowDT.Modules
{
    public abstract class BaseModule
    {
        protected IIocManager IocManager { get; private set; }

        protected BaseModule()
        {
            IocManager = IocManagerFactory.GetInstance();
        }

        public virtual void PreInitialize()
        {
            
        }

        public virtual void Initialize()
        {

        }

        public virtual void PostInitialize()
        {
            
        }
    }
}
