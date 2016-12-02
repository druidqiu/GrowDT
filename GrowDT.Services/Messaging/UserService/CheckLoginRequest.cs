using GrowDT.Application.Services.Dto;

namespace GrowDT.Services.Messaging.UserService
{
    public class CheckLoginRequest : IRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
