using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace 导出zip压缩包
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            testZip();

        }

        public void testZip()
        {
            string thisFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            string thisFilePathZip = thisFilePath + "debug.zip";
            string name = thisFilePath.Substring(thisFilePath.LastIndexOf("\\") + 1) + ".zip";
            ZipClass.Zip(thisFilePath, thisFilePathZip, "");
            DownloadFile(thisFilePathZip, thisFilePath + "test.zip", progressBar1);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="URL">下载文件地址</param>
        /// <param name="Filename">下载后的存放地址</param>
        /// <param name="Prog">用于显示的进度条</param>
        public void DownloadFile(string URL, string filename, System.Windows.Forms.ProgressBar prog)
        {
            try
            {
                WebRequest Myrq = System.Net.HttpWebRequest.Create(URL);
                WebResponse myrp = Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;

                if (prog != null)
                {
                    prog.Maximum = (int)totalBytes;
                }

                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    System.Windows.Forms.Application.DoEvents();
                    so.Write(by, 0, osize);

                    if (prog != null)
                    {
                        prog.Value = (int)totalDownloadedByte;
                    }
                    osize = st.Read(by, 0, (int)by.Length);
                }
                so.Close();
                st.Close();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="URL">下载文件地址</param>
        /// <param name="Filename">下载后的存放地址</param>
        public void DownloadFile(string URL, string filename)
        {
            DownloadFile(URL, filename, null);
        }
    }
}
