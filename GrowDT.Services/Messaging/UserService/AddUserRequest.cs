using GrowDT.Application.Services.Dto;

namespace GrowDT.Services.Messaging.UserService
{
    public class AddUserRequest : IRequestDto
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }
    }
}
