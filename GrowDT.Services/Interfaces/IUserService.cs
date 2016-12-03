using System.Collections.Generic;
using GrowDT.Application.Services;
using GrowDT.Services.Messaging.IUserService;
using GrowDT.Services.ServiceModels;

namespace GrowDT.Services.Interfaces
{
    public interface IUserService : IApplicationService
    {
        void AddUser(AddUserRequest request);
        IEnumerable<UserModel> GetUsers();
    }
}
