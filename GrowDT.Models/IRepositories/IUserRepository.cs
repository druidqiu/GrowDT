using GrowDT.Domain;
using GrowDT.Models.Entities;

namespace GrowDT.Models.IRepositories
{
    public interface IUserRepository : IRepository<User, int>
    {
        RoleNames GetRoles(int userId);
    }
}
