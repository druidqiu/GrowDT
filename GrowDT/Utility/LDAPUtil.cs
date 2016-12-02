using System.DirectoryServices;
using GrowDT.Application;

namespace GrowDT.Utility
{
    public class LDAPUtil
    {
        public static bool DomainLogin(string ap, string userId, string pwd)
        {
            bool result = false;
            string ldapPath = "LDAP://" + AppConfig.ADPath;
            var entry = new DirectoryEntry(ldapPath, ap + "\\" + userId, pwd);
            try
            {
                var search = new DirectorySearcher(entry)
                {
                    Filter = "(SAMAccountName=" + userId + ")"
                };
                search.PropertiesToLoad.Add("cn");
                SearchResult sr = search.FindOne();
                if (sr != null)
                {
                    result = true;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
