using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;

namespace 二维码
{
    /// <summary>
    /// 二维码生成构造对象
    /// </summary>
    [DataContract]
    public class FWQRContent
    {

        private Color _leftColor = Color.FromArgb(220, 35, 156, 160);

        /// <summary>
        ///  左边的颜色  从左自由的线性渐变  默认Color.FromArgb(220, 35, 156, 160)
        /// </summary>
        [DataMember]
        public Color leftColor
        {
            get { return _leftColor; }
            set { _leftColor = value; }
        }


        private Color _rightColor = Color.FromArgb(220, 8, 60, 160);

        /// <summary>
        ///  右边的颜色  从左自由的线性渐变  默认Color.FromArgb(220, 8, 60, 160)
        /// </summary>
        [DataMember]
        public Color rightColor
        {
            get { return _rightColor; }
            set { _rightColor = value; }
        }

        private string _logoUrl;

        /// <summary>
        /// 中间LOGO图标的url
        /// </summary>
        [DataMember]
        public string logoUrl
        {
            get { return _logoUrl; }
            set { _logoUrl = value; }
        }

        private string _FileActualUrl;

        /// <summary>
        /// 最终生成的图片位置（如果需要永久保存二维码图片请自行copy）
        /// </summary>
        [DataMember]
        public string FileActualUrl
        {
            get { return _FileActualUrl; }
            set { _FileActualUrl = value; }
        }

        private string _outFilePath;

        /// <summary>
        /// 图片名称 （如果需要永久保存二维码图片请自行copy）
        /// </summary>
        [DataMember]
        public string outFilePath
        {
            get { return _outFilePath; }
            set { _outFilePath = value; }
        }

        private string _outFileName;

        /// <summary>
        /// 最终生成的文件名称
        /// </summary>
        [DataMember]
        public string outFileName
        {
            get { return _outFileName; }
            set { _outFileName = value; }
        }


        /// <summary>
        /// 默认微软雅黑 设置logo的字体
        /// </summary>
        [DataMember]
        public string logoStringFontFamily
        {
            get { return _logoStringFontFamily; }
            set { _logoStringFontFamily = value; }
        }

        private string _disStringFontFamily = "微软雅黑";

        /// <summary>
        /// 默认宋体 设置顶部显示字体
        /// </summary>
        [DataMember]
        public string disStringFontFamily
        {
            get { return _disStringFontFamily; }
            set { _disStringFontFamily = value; }
        }

        private string _logoStringFontFamily = "宋体";

        private Color _minEllipseColor = Color.FromArgb(255, 78, 178, 194);

        /// <summary>
        /// 中间的圆圈颜色 默认 Color.FromArgb(255, 78, 178, 194)
        /// </summary>
        [DataMember]
        public Color minEllipseColor
        {
            get { return _minEllipseColor; }
            set { _minEllipseColor = value; }
        }

        private Color _leftBottomColor = Color.FromArgb(255, 78, 178, 194);

        /// <summary>
        /// 左下的颜色 默认Color.FromArgb(200, 224, 114, 1)
        /// </summary>
        [DataMember]
        public Color leftBottomColor
        {
            get { return _leftBottomColor; }
            set { _leftBottomColor = value; }
        }

        private Color _leftTopColor = Color.FromArgb(200, 224, 114, 1);

        /// <summary>
        /// 左上的颜色 Color.FromArgb(200, 224, 114, 1)
        /// </summary>
        [DataMember]
        public Color leftTopColor
        {
            get { return _leftTopColor; }
            set { _leftTopColor = value; }
        }

        private string _qrCodeContent;

        /// <summary>
        /// 设置二维码扫描的内容
        /// </summary>
        [DataMember]
        public string qrCodeContent
        {
            get { return _qrCodeContent; }
            set { _qrCodeContent = value; }
        }

        private string _disString;

        /// <summary>
        /// 设置二维码显示的标题
        /// </summary>
        [DataMember]
        public string disString
        {
            get { return _disString; }
            set { _disString = value; }
        }

        private string _logoString = "神彩";

        /// <summary>
        /// 设置logo的内容 默认 神彩
        /// </summary>
        [DataMember]
        public string logoString
        {
            get { return _logoString; }
            set { _logoString = value; }
        }

        private int _sideLen;

        /// <summary>
        /// 设置生成的二维码边长  建议边长不要小于120
        /// </summary>
        [DataMember]
        public int sideLen
        {
            get { return _sideLen; }
            set { _sideLen = value; }
        }
    }
}
