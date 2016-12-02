using System.Collections.Generic;
using System.Data.Entity;
using GrowDT.Models.Entities;

namespace GrowDT.Repositories.Infrastructure
{
    public class GrowDTDbInitializer : DropCreateDatabaseIfModelChanges<GrowDTDbContext>
    {
        protected override void Seed(GrowDTDbContext context)
        {
            var user = new User { Code = "admin", Name = "Administrator"};
            context.Users.Add(user);

            var roles = new List<Role>
            {
                new Role{ RoleName = RoleNames.Admin, Name = "Admin"}
            };
            context.Roles.AddRange(roles);
            user.Roles.AddRange(roles);

            context.SaveChanges();
        }
    }
}
