using System.Linq;
using GrowDT.Models.Entities;
using GrowDT.Models.IRepositories;

namespace GrowDT.Repositories.Repositories
{
    public class UserRepository : GrowDTRepositoryBase<User, int>, IUserRepository
    {
        public RoleNames GetRoles(int userId)
        {
            var user = GetById(userId);
            if (user == null)
            {
                return RoleNames.None;
            }
            var roles = user.Roles.Sum(m => (int) m.RoleName);
            return (RoleNames) roles;
        }
    }
}
