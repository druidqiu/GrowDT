using System.Configuration;
using GrowDT.Application;

namespace GrowDT.MvcHelper.Customization
{
    public class HttpConfigManager: IAppConfigManager
    {
        public string GetValueByKey(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
