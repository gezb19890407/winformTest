using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Project.Common.Http;
using System.Runtime.Serialization.Json;

namespace winformTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void post5(int type)
        {
            try
            {
                string url = "http://msg.umeng.com/api/send";
                string method = "POST";
                AppSendMsg msg = new AppSendMsg();
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = method;
                msg.timestamp = ConvertDateTimeInt(DateTime.Now).ToString();
                msg.payload = new payload();
                msg.payload.body = new payloadBody();
                string app_master_secret = null;
                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("请输入发送消息！");
                    return;
                }
                msg.description = textBox2.Text.Trim();
                msg.payload.body.ticker = textBox2.Text.Trim();
                msg.payload.body.title = textBox2.Text.Trim();
                msg.payload.body.text = textBox2.Text.Trim();
                if (type == 1)
                {
                    msg.appkey = "555ecc9d67e58ed3ce00460f";
                    msg.production_mode = "false";
                    app_master_secret = "6t4whtqve6xru9umypddgzfv200ndsrz";
                    msg.payload.aps = new aps();
                    msg.payload.aps.alert = "测试";
                    msg.payload.aps.badge = 1;
                    msg.alias = "db23a951-ef74-4653-a897-5948d8b1e806";
                }
                else if (type == 0)
                {
                    msg.appkey = "551de873fd98c59ebb0001e4";
                    msg.production_mode = "true";
                    app_master_secret = "s3fcggk1qbuyiamglvpmwbajkaeiizbv";
                    msg.payload.aps = null;
                    msg.alias = "db23a951-ef74-4653-a897-5948d8b1e806";     //殷  琨
                }
                msg.validation_token = GetMD5(msg.appkey.ToLower() + app_master_secret + msg.timestamp.ToLower()).ToLower();
                string postData = EntityToJsonString<AppSendMsg>(msg);
                string returnStr = HttpHelper.SendPost(url, postData);
                textBox1.Text = returnStr;
            }
            catch (WebException ex)
            {
                HttpWebResponse res = (HttpWebResponse)ex.Response;
                Stream myResponseStream = res.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                textBox1.Text = retString;
            }
        }

        // <summary> 
        /// DateTime时间格式转换为Unix时间戳格式 
        /// </summary>  
        /// <param name="time"> DateTime时间格式 </param> 
        /// <returns> Unix时间戳格式 </returns> 
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        /// <summary>
        /// 字符串MD5加密
        /// </summary>
        /// <param name="s"> 需要加密的字符串 </param>
        /// <returns> MD5字符串</returns>
        public static String DecryptString(String s)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.Unicode.GetBytes(s));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 计算MD5
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetMD5(String s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();

            StringBuilder strReturn = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                strReturn.Append(Convert.ToString(bytes[i], 16).PadLeft(2, '0'));
            }

            return strReturn.ToString().PadLeft(32, '0');
        }

        /// <summary>
        /// 实体类转化为Json字符串
        /// </summary>
        /// <typeparam name="T"> 实体类类型 </typeparam>
        /// <param name="Entity"> 实体类</param>
        /// <returns> Json字符串</returns>
        public static string EntityToJsonString<T>(T Entity)
        {
            if (Entity == null)
            {
                return null;
            }
            DataContractJsonSerializer dcjs = new DataContractJsonSerializer(Entity.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                dcjs.WriteObject(ms, Entity);
                StringBuilder sb = new StringBuilder();
                sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
                return sb.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //IOS
            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
                post5(1);
            }
            //安卓
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
                post5(0);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
            }
        }

    }
    public class AppSendMsg
    {
        string _appkey;
        /// <summary>
        /// appkey 必填 应用唯一标识 551de873fd98c59ebb0001e4
        /// </summary>
        public string appkey
        {
            get
            {
                if (_appkey == null)
                {
                    _appkey = "555ecc9d67e58ed3ce00460f";
                }
                return _appkey;
            }
            set
            {
                _appkey = value;
            }
        }

        private string _production_mode;
        /// <summary>
        /// "true/false" // 可选 正式/测试模式。测试模式下，只会将消息发给测试设备。
        /// 测试设备需要到web上添加。
        /// iOS: 测试模式对应APNs的开发环境(sandbox),
        /// 正式模式对应APNs的正式环境(prod),
        /// 正式、测试设备完全隔离。
        /// </summary>
        public string production_mode
        {
            get
            {
                if (_production_mode == null)
                {
                    _production_mode = "false";
                }
                return _production_mode;
            }
            set
            {
                _production_mode = value;
            }
        }

        /// <summary>
        /// 可选 发送消息描述，建议填写。
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 必填 时间戳，10位或者13位均可，时间戳有效期为10分钟
        /// </summary>
        public string timestamp { get; set; }

        string _type;
        /// <summary>
        /// 必填 消息发送类型,其值可以为:
        /// unicast-单播
        /// listcast-列播(要求不超过500个device_token)
        /// filecast-文件播
        /// (多个device_token可通过文件形式批量发送）
        /// broadcast-广播
        /// groupcast-组播
        /// (按照filter条件筛选特定用户群, 具体请参照filter参数)
        /// customizedcast(通过开发者自有的alias进行推送),
        /// 包括以下两种case:
        ///     - alias: 对单个或者多个alias进行推送
        ///     - file_id: 将alias存放到文件后，根据file_id来推送
        /// </summary>
        public string type
        {
            get
            {
                if (_type == null)
                {
                    _type = "customizedcast";
                }
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        //private string _device_tokens;
        ///// <summary>
        ///// 可选 设备唯一表示
        ///// 当type=unicast时,必填, 表示指定的单个设备
        ///// 当type=listcast时,必填,要求不超过500个,
        ///// 多个device_token以英文逗号间隔
        ///// </summary>
        //public string device_tokens
        //{
        //    get
        //    {
        //        return _device_tokens;
        //    }
        //    set
        //    {
        //        _device_tokens = value;
        //    }
        //}

        private string _alias_type;
        /// <summary>
        /// 可选 当type=customizedcast时，必填，alias的类型,
        /// alias_type可由开发者自定义,开发者在SDK中
        /// 调用setAlias(alias, alias_type)时所设置的alias_type
        /// </summary>
        public string alias_type
        {
            get
            {
                if (_alias_type == null)
                {
                    _alias_type = "sunanydzf";
                }
                return _alias_type;
            }
            set
            {
                _alias_type = value;
            }
        }

        /// <summary>
        /// 可选 当type=customizedcast时, 开发者填写自己的alias。 (这里天用户ID)
        /// </summary>
        public string alias { get; set; }

        /// <summary>
        /// 参数json格式字符串
        /// </summary>
        public payload payload { get; set; }

        public string validation_token { get; set; }
    }

    public class payload
    {
        public payloadBody body { get; set; }

        public aps aps
        {
            get;
            set;

        } // 苹果必填字段
        private string _display_type;
        /// <summary>
        ///  必填 消息类型，值可以为: notification-通知，message-消息
        /// </summary>
        public string display_type
        {
            get
            {
                if (_display_type == null)
                {
                    _display_type = "notification";
                }
                return _display_type;
            }
            set
            {
                _display_type = value;
            }
        }
    }

    public class aps
    {
        public string alert
        {
            get;
            set;
        }

        public int? badge
        {
            get;
            set;
        }
    }

    public class payloadBody
    {
        public string ticker { get; set; }

        public string title { get; set; }

        public string text { get; set; }

        string _after_open;
        /// <summary>
        /// "go_app": 打开应用
        /// "go_url": 跳转到URL
        /// "go_activity": 打开特定的activity
        /// "go_custom": 用户自定义内容。
        /// </summary>
        public string after_open
        {
            get
            {
                if (_after_open == null)
                {
                    _after_open = "go_app";
                }
                return _after_open;
            }
            set
            {
                _after_open = value;
            }
        }

        string _play_vibrate;
        /// <summary>
        /// "true/false", // 可选 收到通知是否震动,默认为"true".
        /// 注意，"true/false"为字符串
        /// </summary>
        public string play_vibrate
        {
            get
            {
                if (_play_vibrate == null)
                {
                    _play_vibrate = "true";
                }
                return _play_vibrate;
            }
            set
            {
                _play_vibrate = value;
            }
        }

        string _play_lights;
        /// <summary>
        /// 可选 收到通知是否闪灯,默认为"true"
        /// </summary>
        public string play_lights
        {
            get
            {
                if (_play_lights == null)
                {
                    _play_lights = "true";
                }
                return _play_lights;
            }
            set
            {
                _play_lights = value;
            }
        }

        string _play_sound;
        /// <summary>
        /// 可选 收到通知是否发出声音,默认为"true"
        /// </summary>
        public string play_sound
        {
            get
            {
                if (_play_sound == null)
                {
                    _play_sound = "true";
                }
                return _play_sound;
            }
            set
            {
                _play_sound = value;
            }
        }
    }
}
