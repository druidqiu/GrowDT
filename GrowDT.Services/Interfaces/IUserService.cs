using System.Collections.Generic;
using GrowDT.Application.Services;
using GrowDT.Services.Messaging.UserService;
using GrowDT.Services.ServiceModels;

namespace GrowDT.Services.Interfaces
{
    public interface IUserService : IApplicationService
    {
        void AddUser(AddUserRequest request);
        IEnumerable<UserModel> GetUsers();

        CheckLoginResponse CheckLogin(CheckLoginRequest request);
    }
}
