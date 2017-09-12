using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 美圣短信发送.Helper;

namespace 美圣短信发送
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetCheckbox();
        }
        private string content;
        private void SetCheckbox()
        {
            string smstempid = null;
            string smstempidValue = null;
            List<string> list = new List<string>();
            list.Add("");
            for (int i = 0; i < 10; i++)
            {
                smstempid = "Smstempid" + i;
                smstempidValue = AppSettingHelper.Search__ValueByKey(smstempid);
                if (!string.IsNullOrEmpty(smstempidValue))
                {
                    list.Add(smstempidValue);
                }
            }
            cmbType.DataSource = list;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string errorStr = "";
            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                errorStr += "请填写手机号码！";
            }
            if (string.IsNullOrEmpty(content))
            {
                errorStr += "请选择模版！";
            }
            if (!string.IsNullOrEmpty(errorStr))
            {
                MessageBox.Show(errorStr);
                return;
            }

            txtContentValue.Text = content;
            Param param = new Param();
            param.Param1 = txtParam1.Text;
            param.Param2 = txtParam2.Text;
            param.Param3 = txtParam3.Text;
            param.Param4 = txtParam4.Text;
            param.Param5 = txtParam5.Text;
            txtBackValue.Text = SendMessageHelper.SendMessage(txtPhone.Text, cmbType.SelectedValue.ToString(), param);
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbType.SelectedValue == null)
            {
                MessageBox.Show("请选择模版！");
                return;
            }
            content = AppSettingHelper.Search__ValueByKey(cmbType.SelectedValue.ToString());
            txtTypeValue.Text = content;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtContentValue.Text = content.Replace("@1@", txtParam1.Text).Replace("@2@", txtParam2.Text)
                .Replace("@3@", txtParam3.Text).Replace("@4@", txtParam4.Text).Replace("@5@", txtParam5.Text);
        }
    }
}
