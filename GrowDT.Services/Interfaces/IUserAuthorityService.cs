using GrowDT.Application.Services;
using GrowDT.Services.Messaging.IUserAuthorityService;

namespace GrowDT.Services.Interfaces
{
    public interface IUserAuthorityService : IApplicationService
    {
        CheckLoginResponse CheckLogin(CheckLoginRequest request);
    }
}
