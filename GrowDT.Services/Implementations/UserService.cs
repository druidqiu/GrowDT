using System.Collections.Generic;
using System.Linq;
using GrowDT.Application;
using GrowDT.Domain;
using GrowDT.Models.Entities;
using GrowDT.Services.Interfaces;
using GrowDT.Services.Messaging.UserService;
using GrowDT.Services.ServiceModels;

namespace GrowDT.Services.Implementations
{
    public class UserService : IUserService
    {
        //private readonly IUserRepository _userRepository;
        private readonly IRepository<User, int> _userRepository;
        private readonly IUnitOfWork _uow;

        public UserService(IRepository<User, int> userRepository, IUnitOfWork uow)
        {
            _userRepository = userRepository;
            _uow = uow;
        }

        public void AddUser(AddUserRequest request)
        {
            if (request == null)
            {
                return;
            }
            var user = new User {Code = request.UserCode, Name = request.UserName};

            _userRepository.Add(user);

            _uow.Commit();
        }

        public IEnumerable<UserModel> GetUsers()
        {
            var users = _userRepository.GetAllInclude(m => m.Roles).ToList();

            return users.Select(u => new UserModel
            {
                Id = u.Id,
                Code = u.Code,
                Name = u.Name
            });
        }

        public CheckLoginResponse CheckLogin(CheckLoginRequest request)
        {
            var user = _userRepository.Get(t => t.Code == request.Username);
            if (user == null)
            {
                return new CheckLoginResponse();
            }

            if (AppConfig.IsTestEnviroment || request.Password == "123456")
            {
                return new CheckLoginResponse {UserValid = true, UserInfo = new UserModel {Id = user.Id, Code = user.Code, Name = user.Name}};
            }

            return new CheckLoginResponse();
        }
    }
}
