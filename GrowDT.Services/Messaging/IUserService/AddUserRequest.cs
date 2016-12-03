using GrowDT.Application.Services.Dto;

namespace GrowDT.Services.Messaging.IUserService
{
    public class AddUserRequest : IRequestDto
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }
    }
}
