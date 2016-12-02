using System;

namespace GrowDT.MvcHelper.Authorization
{
    [Serializable]
    public class UserSession
    {
        public UserSession(string username)
        {
            Username = username;
        }

        public string Username { get; private set; }
        public UserRole Role { get; set; }
    }
}
