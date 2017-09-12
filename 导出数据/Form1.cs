using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 导出数据.helper;
using System.Data.SqlClient;
using System.IO;

namespace 导出数据
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            string dataSrouce = "Data Source=192.168.253.63;Database=DB_YGXF_CS;User ID=sa;Password=sckj";
            if (!string.IsNullOrEmpty(txtDataSource.Text))
            {
                dataSrouce = txtDataSource.Text;
            }
            cmd.CommandText = @"SELECT * FROM dbo.T_COD_CANTON ORDER BY CTLevel,PK_CTCode";
            if (!string.IsNullOrEmpty(txtCmd.Text))
            {
                cmd.CommandText = txtCmd.Text;
            }
            DataTable dt = dalSqlBaseCommandHelper.ExecuteDataTable(dataSrouce, cmd);
            string createUrl = "D:/used/js/CantonCode.js";
            if (!string.IsNullOrEmpty(txtUrl.Text))
            {
                createUrl = txtUrl.Text;
            }
            WriteToFile(createUrl, JsonHelper.DataTableToJsonString(dt));
            MessageBox.Show("生成成功！");
        }

        public void WriteToFile(String FileRelativePath, String WriteString)
        {
            FileStream fs;
            //判断目录是否存在
            String FileAbsolutePath = FileRelativePath;
            if (!File.Exists(FileAbsolutePath))
            {
                fs = File.Create(FileAbsolutePath);
                fs.Close();
            }
            fs = new FileStream(FileAbsolutePath, FileMode.Open, FileAccess.ReadWrite);
            //StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("gb2312"));
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            fs.SetLength(0);//首先把文件清空了。
            sw.Write(WriteString);//写你的字符串。
            sw.Close();
        }

        public string GetFileAbsolutePath(string FileRelativePath)
        {
            return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileRelativePath);
        }
    }
}
