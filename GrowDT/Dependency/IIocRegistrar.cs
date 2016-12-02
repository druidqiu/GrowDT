using System;
using System.Reflection;

namespace GrowDT.Dependency
{
    public interface IIocRegistrar
    {
        void RegisterType<T, TImp>(DependencyLifeStyle liftStyle) where TImp : T;
        void RegisterType(Type tType, Type impType, DependencyLifeStyle liftStyle);
        void RegisterType(Assembly assembly);
        void RegisterType(Assembly assembly, Func<Type, bool> predicate);

        void RegisterInterceptionType<T, TImp>(DependencyLifeStyle liftStyle) where TImp : T;
        void RegisterInterceptionType(Type tType, Type impType, DependencyLifeStyle liftStyle);
        void RegisterInterceptionType(Assembly assembly);
        void RegisterInterceptionType(Assembly assembly, Func<Type, bool> predicate);
    }
}
