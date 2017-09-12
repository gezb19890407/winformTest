using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.IO;

namespace 生成exe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            initForm();
        }

        public void initForm()
        {
            txtAddress.Text = AppSettingHelper.Search__ValueByKey("Address");
            txtDataBaseName.Text = AppSettingHelper.Search__ValueByKey("DataBaseName");
            txtUserID.Text = AppSettingHelper.Search__ValueByKey("UserID");
            txtPassword.Text = AppSettingHelper.Search__ValueByKey("Password");
            txtTableName.Text = AppSettingHelper.Search__ValueByKey("TableName");
            txtSavePath.Text = AppSettingHelper.Search__ValueByKey("SavePath");
            txtNamespace.Text = AppSettingHelper.Search__ValueByKey("Namespace");
            txtSavePath1.Text = AppSettingHelper.Search__ValueByKey("SavePath1");
            txtNamespace1.Text = AppSettingHelper.Search__ValueByKey("Namespace1");
            radio2.Checked = AppSettingHelper.Search__ValueByKey("IsNew") == "1" ? true : false;
        }
        string SqlConnectionString = "";

        public void button1_Click(object sender, EventArgs e)
        {
            if (SqlConnectionString == "")
            {
                SqlConnectionString = GetConnectionString(txtAddress.Text.Trim(), txtDataBaseName.Text.Trim(), txtUserID.Text.Trim(), txtPassword.Text.Trim());
            }
            string str = txtTableName.Text.Trim();
            string tableName = str;
            try
            {
                string writeString = "";
                string writeStringModel = "";
                if (radio1.Checked == true)
                {
                    writeString = DataTableHelper.DataTableToEntityStringCs(SqlConnectionString, tableName, txtNamespace.Text.Trim());
                    writeStringModel = writeString.Replace("PK_", "").Replace("FK_", "").Replace(tableName, "MappingEntity__" + tableName);
                }
                else
                {
                    writeString = DataTableHelper.DataTableToEntityStringCsNew(SqlConnectionString, tableName, txtNamespace.Text.Trim(), txtMapping.Text);
                    writeStringModel = writeString.Replace("PK_", "").Replace("FK_", "").Replace(tableName, "M" + tableName).Replace(txtNamespace.Text, txtNamespace1.Text);
                }
                if (!Directory.Exists(txtSavePath.Text.Trim()))
                {
                    Directory.CreateDirectory(txtSavePath.Text.Trim());
                }
                txtMapping.Text = DataTableHelper.DataTableToMappingEntityString(SqlConnectionString, tableName, radio1.Checked == true);
                DataTableHelper.WriteToFile(Path.Combine(txtSavePath.Text.Trim(), tableName + ".cs"), writeString);
                if (!Directory.Exists(txtSavePath1.Text.Trim()))
                {
                    Directory.CreateDirectory(txtSavePath1.Text.Trim());
                }
                DataTableHelper.WriteToFile(Path.Combine(txtSavePath1.Text.Trim(),
                       (radio1.Checked == true ? "MappingEntity__" : "M") + tableName + ".cs"), writeStringModel);
                MessageBox.Show("生成成功");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public string GetConnectionString(string DataSource, string Database, string txtUserID, string txtPassword)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("Data Source={0};Database={1};User ID={2};Password={3}", new object[] { DataSource, Database, txtUserID, txtPassword });
            return builder.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AppSettingHelper.Save__KeyValue("Address", txtAddress.Text);
            AppSettingHelper.Save__KeyValue("DataBaseName", txtDataBaseName.Text);
            AppSettingHelper.Save__KeyValue("UserID", txtUserID.Text);
            AppSettingHelper.Save__KeyValue("Password", txtPassword.Text);
            AppSettingHelper.Save__KeyValue("TableName", txtTableName.Text);
            AppSettingHelper.Save__KeyValue("SavePath", txtSavePath.Text);
            AppSettingHelper.Save__KeyValue("Namespace", txtNamespace.Text);
            AppSettingHelper.Save__KeyValue("SavePath1", txtSavePath1.Text);
            AppSettingHelper.Save__KeyValue("Namespace1", txtNamespace1.Text);
            AppSettingHelper.Save__KeyValue("IsNew", radio2.Checked == true ? "1" : "0");
            MessageBox.Show("保存成功！");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (SqlConnectionString == "")
            {
                SqlConnectionString = GetConnectionString(txtAddress.Text.Trim(), txtDataBaseName.Text.Trim(), txtUserID.Text.Trim(), txtPassword.Text.Trim());
            }
            string str = txtTableName.Text.Trim();
            string tableName = str;
            try
            {
                string writeString = DataTableHelper.DataTableToMappingEntityString(SqlConnectionString, tableName, radio1.Checked == true);
                txtMapping.Text = writeString;
            }
            catch (Exception ex)
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtMapping.Text = "";
        }
    }
}
