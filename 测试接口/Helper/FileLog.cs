using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace 测试接口.Helper
{
    public class FileLog
    {
        private void WriteLog(string ServiceName, String msg)
        {
            DateTime dt = DateTime.Now;
            while (!Monitor.TryEnter(this))
            {
                TimeSpan ts = DateTime.Now - dt;
                if (ts.TotalSeconds > 10) return;
                Thread.Sleep(10);
            }
            try
            {
                string FilePath = string.Format("{0}\\Log\\Y{1}\\M{2}", AppDomain.CurrentDomain.BaseDirectory, dt.Year, dt.Month);
                if (!Directory.Exists(FilePath)) Directory.CreateDirectory(FilePath);
                string FileName = string.Format("{0}\\D{1}.{2}.log", FilePath, dt.Day, ServiceName);
                TextWriter tw = File.AppendText(FileName);
                tw.WriteLine(dt.ToString());
                tw.WriteLine(msg);
                tw.Close();
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

        private static FileLog mLog = null;
        public static void WirteFileLog(string msg)
        {
            if (mLog == null) mLog = new FileLog();
            lock (mLog)
            {
                mLog.WriteLog("12345服务日志", msg);
            }
        }

        //public static void WirteFileLog<T>(T entity)
        //{
        //    if (mLog == null) mLog = new FileLog();
        //    lock (mLog)
        //    {
        //        T entityClone = JsonHelper.Clone<T>(entity);
        //        Type t = typeof(T);
        //        PropertyInfo[] PropertyInfoS = t.GetProperties();
        //        foreach (PropertyInfo pi in PropertyInfoS)
        //        {
        //            if (pi.PropertyType == typeof(byte[]))
        //            {
        //                pi.SetValue(entityClone, null, null);
        //            }
        //        }
        //        string jsonString = JsonHelper.EntityToJsonString<T>(entityClone);
        //        WirteFileLog(typeof(T) + "：" + jsonString);
        //    }
        //}
    }
}
