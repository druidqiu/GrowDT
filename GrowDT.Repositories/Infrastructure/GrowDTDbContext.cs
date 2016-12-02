using System.Data.Entity;
using GrowDT.EntityFramework;
using GrowDT.Models.Entities;

namespace GrowDT.Repositories.Infrastructure
{
    public class GrowDTDbContext : BaseDbContext
    {
        public GrowDTDbContext(): base("DbConnectionString")
        {
        }

        #region Entities
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        #endregion Entities
    }
}
