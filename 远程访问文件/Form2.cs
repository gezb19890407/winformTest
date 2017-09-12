using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using 远程访问文件.Helper;

namespace 远程访问文件
{
    public partial class Form2 : Form
    {
        private bool isAuto = AppSettingHelper.Search__ValueByKey("isAuto") == "1" ? true : false;
        public Form2()
        {
            InitializeComponent();          
            if (isAuto)
            {
                CopyToLocalFile();
                LogHelper.WriteLog("同步成功");
                System.Environment.Exit(0); 
            }
        }

        private string FilePath = @"\\10.32.6.197\record";
        private void button1_Click(object sender, EventArgs e)
        {
            CopyToLocalFile();
            MessageBox.Show("成功！");
        }

        public bool CopyToLocalFile()
        {
            bool IsSuccess = true;
            string noImportantStr = "";
            string importantStr = "";
            string gdbhStr = "";
            try
            {
                //取需要文件的信访数据
                DataTable dtExcuteData = Search__NeedExcuteData();
                List<string> gdbhList = new List<string>();
                if (dtExcuteData != null && dtExcuteData.Rows.Count > 0)
                {
                    for (int i = 0; i < dtExcuteData.Rows.Count; i++)
                    {
                        gdbhList.Add(dtExcuteData.Rows[i][0].ToString());
                        if (dtExcuteData.Rows[i][0] != DBNull.Value)
                        {
                            if (dtExcuteData.Rows[i][1] != DBNull.Value)
                            {
                                if ((bool)dtExcuteData.Rows[i][1])
                                {
                                    importantStr += "'" + dtExcuteData.Rows[i][0].ToString() + "',";
                                }
                                else
                                {
                                    noImportantStr += "'" + dtExcuteData.Rows[i][0].ToString() + "',";
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(importantStr))
                    {
                        importantStr = importantStr.Remove(importantStr.Length - 1, 1);
                    }
                    if (!string.IsNullOrEmpty(noImportantStr))
                    {
                        noImportantStr = noImportantStr.Remove(noImportantStr.Length - 1, 1);
                    }
                    #region
                    if (!string.IsNullOrEmpty(importantStr) || !string.IsNullOrEmpty(noImportantStr))
                    {
                        DataTable dt = Search__NeedCopyData(importantStr, noImportantStr);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (Connect(txtAddress.Text, txtUserName.Text, txtPassword.Text))
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    try
                                    {
                                        string fileLastName = dt.Rows[i][0].ToString().Replace("\\", "/");
                                        string fileName = Path.Combine(FilePath, fileLastName);
                                        string fileDirectory = Path.Combine(txtLocalPath.Text, fileLastName.Substring(0, fileLastName.LastIndexOf("/")));
                                        if (!Directory.Exists(fileDirectory))
                                        {
                                            Directory.CreateDirectory(fileDirectory);
                                        }
                                        File.Copy(fileName, Path.Combine(txtLocalPath.Text, fileLastName), true);
                                        LogHelper.WriteLog(dt.Rows[i][2].ToString() + fileName + "成功！");
                                    }
                                    catch (Exception ex)
                                    {
                                        if (gdbhList.Contains(dt.Rows[i][2].ToString()))
                                        {
                                            gdbhList.Remove(dt.Rows[i][2].ToString());
                                        }
                                        LogHelper.WriteLog(dt.Rows[i][0].ToString() + ex.Message);
                                    }
                                }
                            }
                        }
                    }

                    if (gdbhList != null && gdbhList.Count > 0)
                    {
                        gdbhStr = CommonMethods.JoinStringBySingleQuotes(gdbhList);
                        Update__PeitionIsSaveFile(gdbhStr);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
                if (isAuto)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return IsSuccess;
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
                command = @"net use \\" + remoteHost + " " + passWord + " " + " /user:" + userName + ">NUL";
                proc.StandardInput.WriteLine(command);
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
                    LogHelper.WriteLog(errormsg);
                    if (isAuto)
                    {
                        MessageBox.Show(errormsg);
                    }
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

        public DataTable Search__NeedCopyData(string importantStr, string noImportantStr)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(noImportantStr))
            {
                noImportantStr = " and tb1.no in (" + noImportantStr + ")";
            }
            if (!string.IsNullOrEmpty(importantStr))
            {
                importantStr = " and tb2.no in (" + importantStr + ")";
            }
            sb.AppendFormat(@"
select 
	record.recordFile,accfi.fiid,tb.no 
from NTHB_12369_Record record
    inner join NTHB_12369_AccFI accfi on accfi.AccsueID = Record.ID 
    inner join (
	    select tb1.instanceid,tb1.no from TZXF_XHJXFJBDJB tb1 where tb1.instanceid is not null {0}
	    union all
	    select tb2.instanceid,tb2.no from TZXF_XHJXFJBDJB_ZY tb2  where tb2.instanceid is not null {1}
	)tb on tb.instanceid = accfi.fiid", noImportantStr, importantStr);
            SqlCommand commond = new SqlCommand();
            commond.CommandText = sb.ToString();
            return SqlHelper.ExecuteDataTable(txtDataBase.Text, commond);
        }

        public DataTable Search__NeedExcuteData()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"
select gdbh,isnull(isimportant,0) from t_Dat_petitioninfo where PType  =2 and isnull(fbitstop,0)=0 and isnull(issaveFile,0)=0");
            SqlCommand commond = new SqlCommand();
            commond.CommandText = sb.ToString();
            return SqlHelper.ExecuteDataTable(txtLocalDataBase.Text, commond);
        }

        public bool Update__PeitionIsSaveFile(string gdbhStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"
update t_Dat_petitioninfo set issaveFile = 1 where PType  =2 and isnull(fbitstop,0)=0 and isnull(issaveFile,0)=0 and gdbh in ({0})", gdbhStr);
            SqlCommand commond = new SqlCommand();
            commond.CommandText = sb.ToString();
            return SqlHelper.ExecuteNonQuery(txtLocalDataBase.Text, commond) > 0;
        }
    }
}
