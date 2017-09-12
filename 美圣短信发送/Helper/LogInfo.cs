using System;
using System.Collections.Generic;

namespace 美圣短信发送.Helper.Log
{
    public class LogInfo
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime DateTime
        {
            set
            {
                _DateTime = value;
            }
            get
            {
                return _DateTime;
            }
        }
        private DateTime _DateTime = DateTime.Now;

        /// <summary>
        /// 信息列表
        /// </summary>
        public List<String> InfoList
        {
            get
            {
                return _InfoList;
            }
            set
            {
                _InfoList = value;
            }
        }
        private List<String> _InfoList = new List<String>();
    }
}
