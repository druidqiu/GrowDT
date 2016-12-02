using GrowDT.Application.Services.Dto;
using GrowDT.Services.ServiceModels;

namespace GrowDT.Services.Messaging.UserService
{
    public class CheckLoginResponse : IResponseDto
    {
        public bool UserValid { get; set; }
        public UserModel UserInfo { get; set; }
    }
}
