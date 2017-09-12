using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace 远程访问文件.Helper
{
    public class AppSettingHelper
    {
        public static string Search__ValueByKey(string Key)
        {
            return ConfigurationManager.AppSettings[Key] != null ? ConfigurationManager.AppSettings[Key] : "";
        }
    }
}
