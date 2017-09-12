using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace 远程访问文件
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Connect(txtAddress.Text, txtUserName.Text, txtPassword.Text))
            {
                MessageBox.Show("连接成功");
                File.Copy(txtFullPath.Text, txtLocalPath.Text, true);
            }
            //File.Copy(txtFullPath.Text, txtLocalPath.Text, true);
        }

        public bool Connect(string remoteHost, string userName, string passWord)
        {
            bool Flag = true;
            Process proc = new Process();
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = true;
            try
            {
                proc.Start();
                string command = "exit";
                proc.StandardInput.WriteLine(command);
                command = @"net  use  \\" + remoteHost + "  " + passWord + "  " + "  /user:" + userName + ">NUL";
                MessageBox.Show(proc.StandardError.ReadToEnd());
                command = "exit";
                proc.StandardInput.WriteLine(command);
                while (proc.HasExited == false)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                if (errormsg != "")
                {
                    Flag = false;
                    MessageBox.Show(errormsg);
                }
                proc.StandardError.Close();
            }
            catch (Exception ex)
            {
                Flag = false;
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return Flag;
        }
    }
}
