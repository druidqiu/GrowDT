using System.Linq;
using System.Reflection;

namespace GrowDT.AutoMappers
{
    public class AutoMapperBootstrap
    {
        private static bool _createdMappingsBefore;
        private static readonly object SyncObj = new object();

        public void CreateMappings(Assembly assembly)
        {
            lock (SyncObj)
            {
                if (_createdMappingsBefore)
                {
                    return;
                }

                FindAndAutoMapTypes(assembly);
                CreateOtherMappings();

                _createdMappingsBefore = true;
            }
        }

        private void FindAndAutoMapTypes(Assembly assembly)
        {
            var types = assembly.GetTypes()
                .Where(t => t.IsDefined(typeof (AutoMapAttribute)) ||
                            t.IsDefined(typeof (AutoMapFromAttribute)) ||
                            t.IsDefined(typeof (AutoMapToAttribute)));

            foreach (var type in types)
            {
                AutoMapperHelper.CreateMap(type);
            }
        }

        private void CreateOtherMappings()
        {

        }
    }
}
