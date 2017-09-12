using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace 美圣短信发送.Helper
{
    public class AppSettingHelper
    {
        public static string Search__ValueByKey(string Key)
        {
            return ConfigurationManager.AppSettings[Key] != null ? ConfigurationManager.AppSettings[Key] : "";
        }
    }
    
    public class Param
    {
        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public string Param3 { get; set; }
        public string Param4 { get; set; }
        public string Param5 { get; set; }
    }
}
