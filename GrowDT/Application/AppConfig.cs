using System;
using System.IO;

namespace GrowDT.Application
{
    public static class AppConfig
    {
        private static readonly IAppConfigManager Configer;

        static AppConfig()
        {
            Configer = AppConfigManagerFactory.GetAppConfigManager();
        }

        public static string UserSessionKey
        {
            get { return "user_session_key"; }
        }

        public static string Log4NetName
        {
            get { return Configer.GetValueByKey(AppConst.Log4NetName) ?? "Logging"; }
        }

        public static string Log4NetConfigUrl
        {
            get
            {
                var folderUrl = Configer.GetValueByKey(AppConst.Log4NetConfigUrl) ?? @"bin\log4netConfig.xml";
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderUrl);
            }
        }

        public static bool IsTestEnviroment
        {
            get
            {
                bool flag;
                bool.TryParse(Configer.GetValueByKey(AppConst.IsTestEnviroment), out flag);
                return flag;
            }
        }

        public static string ADPath
        {
            get { return Configer.GetValueByKey(AppConst.ADPath); }
        }

        public static string SmtpUserPwd
        {
            get { return Configer.GetValueByKey(AppConst.SmtpUserPwd); }
        }

        public static string SmtpUserAddress
        {
            get { return Configer.GetValueByKey(AppConst.SmtpUserAddress); }
        }

        public static string SmtpHost
        {
            get { return Configer.GetValueByKey(AppConst.SmtpHost); }
        }

        public static int SmtpHostPort
        {
            get
            {
                int port;
                int.TryParse(Configer.GetValueByKey(AppConst.SmtpHostPort), out port);
                return port == 0 ? 25 : port;
            }
        }

        public static string SmtpUserDisplayName
        {
            get { return Configer.GetValueByKey(AppConst.SmtpUserDisplayName); }
        }

        public static string EmailInterceptReceiver
        {
            get { return Configer.GetValueByKey(AppConst.EmailInterceptReceiver); }
        }

        public static bool EmailWriteAsFile
        {
            get
            {
                bool flag;
                bool.TryParse(Configer.GetValueByKey(AppConst.EmailWriteAsFile), out flag);
                return flag;
            }
        }

        public static string EmailFileLocation
        {
            get { return Configer.GetValueByKey(AppConst.EmailFileLocation); }
        }

        public static bool MiniProfilerEnable
        {
            get
            {
                bool flag;
                bool.TryParse(Configer.GetValueByKey(AppConst.MiniProfilerEnable), out flag);
                return flag;
            }
        }

        public static string ExcelUploadFolder
        {
            get
            {
                string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Configer.GetValueByKey(AppConst.ExcelUploadFolder) ?? @"App_Data\ExcelUploaded");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                return folderPath;
            }
        }

        public static bool SmtpUseSsl
        {
            get
            {
                bool flag;
                bool.TryParse(Configer.GetValueByKey(AppConst.SmtpUseSsl), out flag);
                return flag;
            }
        }
    }
}
