using System;
using System.Reflection;
using AutoMapper;

namespace GrowDT.AutoMappers
{
    internal static class AutoMapperHelper
    {
        public static void CreateMap(Type type)
        {
            CreateMap<AutoMapFromAttribute>(type);
            CreateMap<AutoMapToAttribute>(type);
            CreateMap<AutoMapAttribute>(type);
        }

        private static void CreateMap<TAttribute>(Type type)
            where TAttribute : AutoMapAttribute
        {
            if (!type.IsDefined(typeof(TAttribute)))
            {
                return;
            }

            foreach (var autoMapToAttribute in type.GetCustomAttributes<TAttribute>())
            {
                if (autoMapToAttribute.TargetTypes==null || autoMapToAttribute.TargetTypes.Length == 0)
                {
                    continue;
                }

                foreach (var targetType in autoMapToAttribute.TargetTypes)
                {
                    if (autoMapToAttribute.Direction.HasFlag(AutoMapDirection.To))
                    {
                        Mapper.Initialize(cfg => cfg.CreateMap(type, targetType));
                    }

                    if (autoMapToAttribute.Direction.HasFlag(AutoMapDirection.From))
                    {

                        Mapper.Initialize(cfg => cfg.CreateMap(targetType, type));
                    }
                }
            }
        }
    }
}
