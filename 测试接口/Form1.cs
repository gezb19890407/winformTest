using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 测试接口.Helper;
using System.Xml;

namespace 测试接口
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PassBack__12345Info();
        }

        /// <summary>
        /// 12345任务回传 把数据传给接口返回数据
        /// </summary>
        /// <param name="petitionEntity"></param>
        /// <returns></returns>
        public void PassBack__12345Info()
        {
            string ParasXml = "";
            ParasXml += "<![CDATA[<?xml version=\"1.0\" encoding=\"gb2312\"?>";
            ParasXml += "<paras>";
            ParasXml += "<toouguid>{8}</toouguid>";
            ParasXml += "<TaskGuid>{0}</TaskGuid>";
            ParasXml += "<workitemguid>{1}</workitemguid>";
            ParasXml += "<ActivityInstanceGuid>{2}</ActivityInstanceGuid>";
            ParasXml += "<FeedBackContent>{3}</FeedBackContent>";
            ParasXml += "<Is_OUAllowPublic>{4}</Is_OUAllowPublic>";
            ParasXml += "<Is_SZCG>{5}</Is_SZCG>";
            ParasXml += "<AttachList>";
            ParasXml += "<Attach>";
            ParasXml += "<AttFileName>{6}</AttFileName>";
            ParasXml += "<AttContent>{7}</AttContent>";
            ParasXml += "</Attach>";
            ParasXml += "</AttachList>";
            ParasXml += "</paras>]]>";
            string resultStr = null;

            ParasXml = string.Format(ParasXml, textBox4.Text, textBox5.Text, textBox6.Text, "测试信息", 1, 0, "", "", textBox3.Text);
            FileLog.WirteFileLog(ParasXml);
            try
            {
                #region soupui
                string soapAction = "http://tempuri.org/GetFeedBackContent";
                SoapClient soapClient = new SoapClient(textBox1.Text, soapAction);
                FileLog.WirteFileLog(textBox1.Text);
                soapClient.TargetName = "";
                soapClient.MethodName = "tem:GetFeedBackContent";
                soapClient.Arguments.Add(new SoapParameter("tem:ValidateData", "Epoint_WebSerivce_**##0601"));
                soapClient.Arguments.Add(new SoapParameter("tem:ParasXml", ParasXml));
                resultStr = soapClient.GetResult(true).ToString();
                FileLog.WirteFileLog(resultStr);
                #endregion
            }
            catch (Exception ex)
            {
                resultStr = ex.Message;                
            }
            textBox2.Text = txtBH + resultStr;
            FileLog.WirteFileLog(textBox2.Text);
        }
    }
}
