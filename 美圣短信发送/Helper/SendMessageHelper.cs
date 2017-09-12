using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using 美圣短信发送.Helper.Log;
using System.Web;

namespace 美圣短信发送.Helper
{
    public class SendMessageHelper
    {
        private static string SmsAddress = AppSettingHelper.Search__ValueByKey("SmsAddress");
        private static string SmsUserName = AppSettingHelper.Search__ValueByKey("SmsUserName");
        private static string Smsscode = AppSettingHelper.Search__ValueByKey("Smsscode");
        private static string Smscmsgid = AppSettingHelper.Search__ValueByKey("Smscmsgid");

        public static string SendMessage(string Phone, string Smstempid, Param param)
        {
            string backStr = "";
            string sms = "";
            if (!string.IsNullOrEmpty(SmsUserName))
            {
                SmsUserName = EncryptHelper.Decode(SmsUserName);
            }
            if (!string.IsNullOrEmpty(Smsscode))
            {
                Smsscode = EncryptHelper.Decode(Smsscode);
            }
            LogHelper.WriteMsgLog("发送手机" + Phone + "，发送内容" + sms);
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Param1))
                {
                    sms += "@1@=" + param.Param1;
                }
                if (!string.IsNullOrEmpty(param.Param2))
                {
                    sms += ",@2@=" + param.Param2;
                }
                if (!string.IsNullOrEmpty(param.Param3))
                {
                    sms += ",@3@=" + param.Param3;
                }
                if (!string.IsNullOrEmpty(param.Param4))
                {
                    sms += ",@4@=" + param.Param4;
                }
                if (!string.IsNullOrEmpty(param.Param5))
                {
                    sms += ",@5@=" + param.Param5;
                }
            }
            sms = HttpUtility.UrlEncode(sms, Encoding.GetEncoding("gbk"));//若参数中有中文的话，请先用此方法转成GBK编码
            String url = SmsAddress + "?username=" + SmsUserName + "&scode=" + Smsscode + "&content=" + sms + "&tempid=" + Smstempid + "&mobile=" + Phone;
            //return null;
            HttpClient client = new HttpClient(url);
            backStr = client.GetString();
            backStr = Search__SMSRetrun(backStr);
            LogHelper.WriteMsgLog("返回值" + backStr);
            return backStr;
        }

        private static string Search__SMSRetrun(string ErrorNo)
        {
            string value = "发送成功";
            switch (ErrorNo)
            {
                case "100":
                    value = "发送失败，";
                    break;
                case "101":
                    value = "用户账号不存在或密码错误，";
                    break;
                case "102":
                    value = "账号已禁用，";
                    break;
                case "103":
                    value = "参数不正确，";
                    break;
                case "105":
                    value = "短信内容超过500字、或为空、或内容编码格式不正确，";
                    break;
                case "106":
                    value = "手机号码超过100个或合法的手机号码为空，每次最多提交100个号，";
                    break;
                case "108":
                    value = "余额不足，";
                    break;
                case "109":
                    value = "指定访问ip地址错误，";
                    break;
                case "110#":
                    value = "短信内容存在系统保留关键词，如有多个词，使用逗号分隔：110#(李老师,成人)，";
                    break;
                case "114":
                    value = "模板短信序号不存在，";
                    break;
                case "115":
                    value = "短信签名标签序号不存在，";
                    break;
                default:
                    break;
            }
            return value;
        }
    }
}
