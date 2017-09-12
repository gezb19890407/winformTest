using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Web;
using System.Data;

namespace 远程访问文件.Helper
{
    /// <summary>
    /// 通用方法
    /// </summary>
    public class CommonMethods
    {

        /// <summary>
        /// 生成两个数之间的随机数
        /// 参数1      min                 开始数 
        /// 参数2      max                 结束数 
        /// </summary>
        public static int RandomNumber(int min, int max)
        {
            Random ran = new Random(Guid.NewGuid().GetHashCode());
            int RandKey = ran.Next(min, max);
            return RandKey;
        }

        /// <summary>
        /// 字符串集合转化为连接字符串
        /// </summary>
        /// <param name="liststr">字符串集合</param>
        /// <returns>连接字符串</returns>
        public static string JoinString(List<String> liststr)
        {
            StringBuilder strs = new StringBuilder();
            if (liststr != null && liststr.Count > 0)
            {
                foreach (String str in liststr)
                {
                    if (!String.IsNullOrEmpty(str))
                    {
                        strs.Append(str + ",");
                    }
                }
                strs.Remove(strs.Length - 1, 1);
            }
            return strs.ToString();
        }

        public static string JoinStringByDoubleQuotes(List<String> liststr)
        {
            StringBuilder strs = new StringBuilder();
            if (liststr != null && liststr.Count > 0)
            {
                foreach (String str in liststr)
                {
                    if (!String.IsNullOrEmpty(str))
                    {
                        strs.Append("''" + str + "''" + ",");
                    }
                }
                strs.Remove(strs.Length - 1, 1);
            }
            return strs.ToString();
        }
        public static string JoinStringBySingleQuotes(List<String> liststr)
        {
            StringBuilder strs = new StringBuilder();
            if (liststr != null && liststr.Count > 0)
            {
                foreach (String str in liststr)
                {
                    if (!String.IsNullOrEmpty(str))
                    {
                        strs.Append("'" + str + "'" + ",");
                    }
                }
                strs.Remove(strs.Length - 1, 1);
            }
            return strs.ToString();
        }
        public static string JoinStringBySingleQuotes(List<Int32> liststr)
        {
            StringBuilder strs = new StringBuilder();
            if (liststr != null && liststr.Count > 0)
            {
                foreach (Int32 str in liststr)
                {
                    //if (!String.IsNullOrEmpty(str))
                    //{
                    strs.Append("'" + str + "'" + ",");
                    //}
                }
                strs.Remove(strs.Length - 1, 1);
            }
            return strs.ToString();
        }

        /// <summary>
        /// 通过逗号相连
        /// </summary>
        /// <param name="liststr"></param>
        /// <returns></returns>
        public static string JoinStringByNcomma(List<Int32> liststr)
        {
            StringBuilder strs = new StringBuilder();
            if (liststr != null && liststr.Count > 0)
            {
                foreach (Int32 str in liststr)
                {
                    //if (!String.IsNullOrEmpty(str))
                    //{
                    strs.Append(str + ",");
                    //}
                }
                strs.Remove(strs.Length - 1, 1);
            }
            return strs.ToString();
        }

        public static string JoinStringBySingleQuotes(List<Int64> liststr)
        {
            StringBuilder strs = new StringBuilder();
            if (liststr != null && liststr.Count > 0)
            {
                foreach (Int64 str in liststr)
                {
                    //if (!String.IsNullOrEmpty(str))
                    //{
                    strs.Append("'" + str + "'" + ",");
                    //}
                }
                strs.Remove(strs.Length - 1, 1);
            }
            return strs.ToString();
        }
        public static string JoinStringBySingleQuotes(List<Decimal> liststr)
        {
            StringBuilder strs = new StringBuilder();
            if (liststr != null && liststr.Count > 0)
            {
                foreach (Decimal str in liststr)
                {
                    //if (!String.IsNullOrEmpty(str))
                    //{
                    strs.Append("'" + str + "'" + ",");
                    //}
                }
                strs.Remove(strs.Length - 1, 1);
            }
            return strs.ToString();
        }
        public static string JoinStringBySingleQuotes(List<Guid> liststr)
        {
            StringBuilder strs = new StringBuilder();
            if (liststr != null && liststr.Count > 0)
            {
                foreach (Guid str in liststr)
                {
                    //if (!String.IsNullOrEmpty(str))
                    //{
                    strs.Append("'" + str + "'" + ",");
                    //}
                }
                strs.Remove(strs.Length - 1, 1);
            }
            return strs.ToString();
        }

        public static string JoinSQLBySingleQuotes(String FieldName, List<String> liststr)
        {
            StringBuilder strs = new StringBuilder();
            Boolean bFirst = true;
            ;
            if (liststr != null && liststr.Count > 0)
            {
                foreach (String str in liststr)
                {
                    if (!String.IsNullOrEmpty(str))
                    {
                        if (bFirst)
                        {
                            bFirst = false;
                        }
                        else
                            strs.Append(" OR ");
                        strs.AppendFormat("{0} Like '{1}%'", FieldName, str);
                    }
                }
            }
            return strs.ToString();
        }

        /// <summary>
        /// 根据路径获取企业图片
        /// </summary>
        /// <param name="Path">图片路径</param>
        /// <returns>图片字节流</returns>
        public static Byte[] GetFileFromPath(string Path)
        {
            try
            {
                FileStream FileStream = new FileStream(Path, FileMode.Open, FileAccess.Read);
                byte[] file = new byte[FileStream.Length];
                FileStream.Read(file, 0, (int)FileStream.Length);
                FileStream.Close();
                return file;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ImageData"></param>
        /// <param name="Extension">扩展名</param>
        /// <returns></returns>
        public static string ConvertImageDataToUrlData(Byte[] ImageData, String Extension)
        {
            return "data:image/" + Extension + ";base64," + Convert.ToBase64String(ImageData);
        }

        public static void CreateImage(Byte[] ImageData, String Path)
        {
            if (File.Exists(Path))
            {
                return;
            }
            using (MemoryStream Stream = new MemoryStream(ImageData))
            {
                Bitmap Image = new Bitmap(Stream);
                Image.Save(Path, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        public static String ColorToRGB(Color color)
        {
            return String.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
        }

        public static string GetWebSiteRootUrl()
        {
            string Authority = System.Web.HttpContext.Current.Request.Url.Authority;
            String WebSiteRootUrl = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + (!string.IsNullOrEmpty(Authority) ? Authority : "localhost") + "/";
            String VirtualDirectory = System.Web.HttpContext.Current.Request.ApplicationPath.TrimStart('/');
            if (!String.IsNullOrEmpty(VirtualDirectory))
            {
                WebSiteRootUrl += VirtualDirectory + "/";
            }
            return WebSiteRootUrl;
        }

        public static Boolean IsExistsVirtual()
        {
            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ApplicationPath.TrimStart('/')))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
