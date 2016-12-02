using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;
using GrowDT.EntityFramework.Infrastructures;

namespace GrowDT.EntityFramework
{
    [DbConfigurationType(typeof(Configure))]
    public abstract class BaseDbContext : DbContext
    {
        protected BaseDbContext(string connectionString)
            : base(connectionString)
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;//load sql.dll
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(GetType()));
        }
    }
}
