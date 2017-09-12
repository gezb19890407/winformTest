using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace 美圣短信发送.Helper.Log
{
    public class LogHelper
    {
        private static Queue<LogInfo> LogInfoQueue = new Queue<LogInfo>();
        private static bool IsWriteLogToFileNow = false;
        /// <summary>
        /// 日志的路径
        /// </summary>
        public static readonly string LogFolderRelativePath = "Log/";
        public static readonly string LogFileRelativePath = LogFolderRelativePath + "Log.txt";

        /// <summary>
        /// 锁住对象
        /// </summary>
        public static object obj = new object();
        /// <summary>
        /// 消息发送错误日志
        /// </summary>
        public static readonly string MsgLgoPath = "MsgLog/";

        public static void WriteLog(string LogString)
        {
            LogInfoQueue.Enqueue(new LogInfo() { InfoList = new List<string>() { LogString } });
            WriteLogToFile();
        }

        public static void WriteLog(List<string> LogStringList)
        {
            LogInfoQueue.Enqueue(new LogInfo() { InfoList = LogStringList });
            WriteLogToFile();
        }

        private static void WriteLogToFile()
        {
            try
            {
                if (!IsWriteLogToFileNow)
                {
                    IsWriteLogToFileNow = true;
                    FileStream fs;
                    string LogFileAbsolutePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LogFileRelativePath);
                    if (!File.Exists(LogFileAbsolutePath))
                    {
                        string LogFolderAbsolutePath = System.IO.Path.GetDirectoryName(LogFileAbsolutePath);
                        if (!Directory.Exists(LogFolderAbsolutePath))
                        {
                            Directory.CreateDirectory(LogFolderAbsolutePath);
                        }
                        fs = File.Create(LogFileAbsolutePath);
                    }
                    else
                    {
                        fs = new FileStream(LogFileAbsolutePath, FileMode.Append, FileAccess.Write);
                    }
                    LogInfo _LogInfo;
                    StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("gb2312"));
                    while (LogInfoQueue.Count > 0)
                    {
                        _LogInfo = LogInfoQueue.Dequeue();
                        if (_LogInfo != null)
                        {
                            sw.WriteLine(_LogInfo.DateTime.ToString());
                            foreach (String LogString in _LogInfo.InfoList)
                            {
                                sw.WriteLine(LogString);
                            }
                            sw.WriteLine();
                        }
                    }
                    sw.Close();
                    fs.Close();
                    IsWriteLogToFileNow = false;
                }
            }
            catch (Exception) { }
        }


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

        public static void WriteMsgLog(string msg)
        {
            WriteLog(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, MsgLgoPath), "MessageError", msg);
        }
    }
}
