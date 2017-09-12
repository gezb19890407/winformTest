using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace 远程访问文件.Helper
{
    public class LogHelper
    {
        private static object obj = new object();
        private static string MsgLgoPath = "FileEvidenceLog";

        public static void WriteLog(string FilePath, string Name, String Message)
        {
            DateTime dt = DateTime.Now;
            //2014年2月17日 增加多线程日志写入同步机制
            while (!Monitor.TryEnter(obj))
            {
                TimeSpan ts = DateTime.Now - dt;
                if (ts.TotalSeconds > 10) return;
                Thread.Sleep(10);
            }
            try
            {
                FilePath = string.Format("{0}\\Y{1}\\M{2}", FilePath, dt.Year, dt.Month);
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }
                string FileName = string.Format("{0}\\D{1}.{2}.log", FilePath, dt.Day, Name);
                TextWriter tw = File.AppendText(FileName);
                tw.WriteLine(dt.ToString());
                tw.WriteLine(Message);
                tw.Close();
            }
            finally
            {
                Monitor.Exit(obj);
            }
        }

        public static void WriteLog(string msg)
        {
            WriteLog(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, MsgLgoPath), "FileLog", msg);
        }
    }
}
