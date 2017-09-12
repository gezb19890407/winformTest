using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace 是否节假日
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IsHolidayByDate(DateTime.Parse(dateTimePicker1.Text).ToString("yyyyMMdd"));
        }

        /// <summary>
        /// 判断是不是节假日,节假日返回true 
        /// </summary>
        /// <param name="date">日期格式：yyyyMMdd</param>
        /// <returns></returns>
        public bool IsHolidayByDate(string date)
        {
            bool isHoliday = false;
            System.Net.WebClient WebClientObj = new System.Net.WebClient();
            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
            PostVars.Add("d", date);//参数
            try
            {
                byte[] byRemoteInfo = WebClientObj.UploadValues(@"http://www.easybots.cn/api/holiday.php", "POST", PostVars);//请求地址,传参方式,参数集合
                string sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo);//获取返回值

                string result = JObject.Parse(sRemoteInfo)[date].ToString();
                textBox1.Text = result;
                if (result == "\"0\"" || result == "0")
                {
                    isHoliday = false;
                }
                else if (result == "\"1\"" || result == "\"2\"" || result == "1" || result == "2")
                {
                    isHoliday = true;
                }
            }
            catch (Exception ex)
            {
                isHoliday = false;
            }
            return isHoliday;
        }
    }
}
