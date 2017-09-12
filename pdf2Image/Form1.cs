using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using O2S.Components.PDFRender4NET;

namespace pdf2Image
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string pdfInputPath = textBox1.Text;
                //DateTime dt = File.GetLastWriteTime(pdfInputPath);
                string imageOutputName = pdfInputPath.Replace(".pdf", ".png");
                string fileOutputName = pdfInputPath.Replace(".pdf", ".htm");
                string imageOutputPath = pdfInputPath.Substring(0, pdfInputPath.LastIndexOf("\\"));
                FileInfo file = new FileInfo(pdfInputPath);
                PDFFile pdfFile = PDFFile.Open(pdfInputPath);
                int startPageNum = 1;
                int endPageNum = pdfFile.PageCount;
                StringBuilder sb = new StringBuilder();
                sb = new StringBuilder(@"  
                    <html xmlns='http://www.w3.org/1999/xhtml'>
                    <head>
                        <title>证据资料</title>
                    </head>
                    <body>
                        <div style='width:900px; margin:0 auto;'>");
                for (int i = startPageNum; i <= endPageNum; i++)
                {
                    try
                    {
                        sb.AppendFormat(@"<div style=' width:100%;'><img alt='' style='width:880px; margin: 0 auto;' src=""{0}"" />
    </div>
    <div>
    &nbsp;
    </div>", file.Name + "_" + i.ToString() + "." + ImageFormat.Png.ToString());
                        Bitmap pageImage = pdfFile.GetPageImage(i - 1, 56 * 3);
                        pageImage.Save(imageOutputPath + "\\" + file.Name + "_" + i.ToString() + "." + ImageFormat.Png.ToString(), ImageFormat.Png);
                        pageImage.Dispose();

                        //WriteLog.WirteFileLog("成功:" + imageOutputPath + "\\" + file.Name + "_" + i.ToString() + "." + ImageFormat.Png.ToString(), null);
                    }
                    catch (Exception ex)
                    {
                        //WriteLog.WirteFileLog("pdf错误：" + fileName + "i" + ex.Message);
                    }
                }
                sb.Append(@" 
                        </div>
                    </body>
                </html>");
                WriteToFile(fileOutputName, sb.ToString());
                pdfFile.Dispose();
            }
            catch (Exception ex)
            {
                //WriteLog.WirteFileLog("文件错误：" + fileName + ex.Message);
            }
        }

        public static void WriteToFile(String FileRelativePath, String WriteString)
        {
            FileStream fs;
            //判断目录是否存在
            String FileAbsolutePath = GetFileAbsolutePath(FileRelativePath);
            if (!File.Exists(FileAbsolutePath))
            {
                fs = File.Create(FileAbsolutePath);
                fs.Close();
            }
            fs = new FileStream(FileAbsolutePath, FileMode.Open, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("utf-8"));
            fs.SetLength(0);//首先把文件清空了。
            sw.Write(WriteString);//写你的字符串。
            sw.Close();
        }

        public static string GetFileAbsolutePath(string FileRelativePath)
        {
            return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileRelativePath);
        }
    }
}
