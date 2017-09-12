using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace 生成exe
{
    public class AppSettingHelper
    {
        public static string Search__ValueByKey(string Key)
        {
            return ConfigurationManager.AppSettings[Key] != null ? ConfigurationManager.AppSettings[Key] : "";
        }

        public static void Save__KeyValue(string Key, string Value)
        {
            //ConfigurationManager.AppSettings[Key] = Value;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[Key] != null)
            {
                config.AppSettings.Settings[Key].Value = Value;
            }
            else
            {
                config.AppSettings.Settings.Add(Key, Value);
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
