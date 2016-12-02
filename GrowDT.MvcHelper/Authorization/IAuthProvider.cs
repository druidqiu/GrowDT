namespace GrowDT.MvcHelper.Authorization
{
    public interface IAuthProvider
    {
        void Authenticate(string username);
        void SignOut();
        bool IsAuthenticated();
        string LoginUrl { get; }
    }
}
