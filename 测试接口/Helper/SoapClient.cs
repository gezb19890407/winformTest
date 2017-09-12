using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace 测试接口.Helper
{

    /// <summary>
    /// SOAP辅助类
    /// </summary>
    public static class SoapHelper
    {
        /// <summary>
        /// 消息体格式
        /// </summary>
        private const String FORMAT_ENVELOPE = (
@"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'  xmlns:{0}='{3}'>
   <soapenv:Header/>
   <soapenv:Body>
       <{1}>{2}</{1}>
   </soapenv:Body>
</soapenv:Envelope>");

        /// <summary>
        /// 参数格式
        /// </summary>
        private const String FORMAT_PARAMETER = "<{0}>{1}</{0}>";

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="soapAction">SOAP动作</param>
        /// <param name="soapParameters">参数集合</param>
        /// <returns>返回值</returns>
        public static String MakeEnvelope(String soapAction, String methodName, String TargetName, params SoapParameter[] soapParameters)
        {
            string targetNamespace = "http://tempuri.org/";
            if (!String.IsNullOrEmpty(soapAction))
            {
                int firstIndex = soapAction.IndexOf("//", System.StringComparison.Ordinal);
                int length = 0;
                if (firstIndex > 0)
                {
                    length = soapAction.IndexOf("/", firstIndex + 2, System.StringComparison.Ordinal);
                }
                targetNamespace = soapAction.Substring(0, length + 1);
            }

            if (!String.IsNullOrEmpty(TargetName))
            {
                targetNamespace = TargetName;
            }
            string tem = "xsd";
            if (methodName.Contains(":"))
            {
                tem = methodName.Split(new string[] { ":" }, StringSplitOptions.None)[0];
            }
            return String.Format(FORMAT_ENVELOPE, tem, methodName, BuildSoapParameters(soapParameters), targetNamespace);
        }

        /// <summary>
        /// 创建SOAP参数内容
        /// </summary>
        /// <param name="soapParameters">参数集合</param>
        /// <returns>SOAP参数内容</returns>
        public static String BuildSoapParameters(IEnumerable<SoapParameter> soapParameters)
        {
            var buffer = new StringBuilder();
            var content = "";
            foreach (var soapParameter in soapParameters)
            {
                if (soapParameter.innerParams.Count > 0)
                {
                    content = BuildSoapParameters(soapParameter.innerParams);
                }
                var strContent = GetObjectContent(soapParameter.Value);
                buffer.AppendFormat(FORMAT_PARAMETER, soapParameter.Name, string.IsNullOrEmpty(content) ? strContent : content);
            }

            return buffer.ToString();
        }


        /// <summary>
        /// 获取名称空间和函数名称
        /// </summary>
        /// <param name="soapAction">SOAP动作</param>
        /// <param name="nameSpace">名称空间</param>
        /// <param name="methodName">函数名称</param>
        public static void GetNameSpaceAndMethodName(String soapAction, out String nameSpace, out String methodName)
        {
            nameSpace = (methodName = String.Empty);

            var index = soapAction.LastIndexOf(Path.AltDirectorySeparatorChar);
            nameSpace = soapAction.Substring(0, index + 1);
            methodName = soapAction.Substring(index + 1, soapAction.Length - index - 1);
        }

        /// <summary>
        /// 获取对象内容XML
        /// </summary>
        /// <param name="graph">图</param>
        /// <returns>对象内容XML</returns>
        public static String GetObjectContent(Object graph)
        {
            using (var memoryStream = new MemoryStream())
            {
                var graphType = graph.GetType();
                var xmlSerializer = new XmlSerializer(graphType);

                // XML序列化
                xmlSerializer.Serialize(memoryStream, graph);

                // 获取对象XML
                var strContent = Encoding.UTF8.GetString(memoryStream.ToArray());
                var xmlDocument = new XmlDocument();

                xmlDocument.LoadXml(strContent);

                // 返回对象内容XML
                var contentNode = xmlDocument.SelectSingleNode(graphType.Name);

                if (contentNode != null)
                    return contentNode.InnerXml;

                return graph.ToString();
            }
        }
    }

    /// <summary>
    /// SOAP参数
    /// </summary>
    public sealed class SoapParameter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        public SoapParameter(String name, Object value)
        {
            this.Name = name;
            this.Value = value;
        }

        public SoapParameter addChildElement(String name, Object value)
        {
            var parm = new SoapParameter(name, value);
            this.innerParams.Add(parm);
            return parm;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// 值
        /// </summary>
        public Object Value { get; private set; }

        public List<SoapParameter> innerParams = new List<SoapParameter>();

    }

    /// <summary>
    /// SOAP客户端
    /// </summary>
    public sealed class SoapClient
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uriString">请求地址</param>
        /// <param name="soapAction">SOAP动作</param>
        public SoapClient(String uriString, String soapAction)
            : this(new Uri(uriString), soapAction) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uri">请求地址</param>
        /// <param name="soapAction">SOAP动作</param>
        public SoapClient(Uri uri, String soapAction)
        {
            this.Uri = uri;
            this.SoapAction = soapAction;
            this.Arguments = new List<SoapParameter>();
            this.Credentials = CredentialCache.DefaultNetworkCredentials;
        }

        /// <summary>
        /// 参数集合
        /// </summary>
        public IList<SoapParameter> Arguments { get; private set; }

        /// <summary>
        /// 身份凭证
        /// </summary>
        public ICredentials Credentials { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// SOAP动作
        /// </summary>
        public String SoapAction { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public String MethodName { get; set; }

        public String OperationCode { get; set; }

        public String TargetName { get; set; }

        public String ClientId { get; set; }

        public String SysCode { get; set; }
        /// <summary>
        /// 获取响应
        /// </summary>
        /// <returns>响应</returns>
        public WebResponse GetResponse()
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(this.Uri);
            webRequest.Headers.Add("SOAPAction", String.Format("\"{0}\"", this.SoapAction));
            webRequest.Headers.Add("OperationCode", this.OperationCode);
            webRequest.Headers.Add("ClientId", this.ClientId);
            webRequest.Headers.Add("SysCode", this.SysCode);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            webRequest.Credentials = this.Credentials;

            // 写入请求SOAP信息
            using (var requestStream = webRequest.GetRequestStream())
            {
                using (var textWriter = new StreamWriter(requestStream))
                {
                    var envelope = SoapHelper.MakeEnvelope(this.SoapAction, this.MethodName, this.TargetName, this.Arguments.ToArray());

                    if (!String.IsNullOrEmpty(envelope))
                        textWriter.Write(envelope);
                }
            }

            // 获取SOAP请求返回
            return webRequest.GetResponse();
        }

        /// <summary>
        /// 获取返回的XML
        /// </summary>
        /// <returns>返回值</returns>
        public Object GetResult()
        {
            string soapResult = "";
            using (WebResponse response = this.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
            }
            return soapResult;
        }

        /// <summary>
        /// 获取返回结果
        /// </summary>
        /// <param name="returnVal">是否需要只显示返回值</param>
        /// <returns>返回值</returns>
        public Object GetResult(bool returnVal)
        {
            // 获取响应
            var webResponse = this.GetResponse();
            var xmlReader = XmlTextReader.Create(webResponse.GetResponseStream());
            var xmlDocument = new XmlDocument();

            // 加载响应XML
            xmlDocument.Load(xmlReader);

            var nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
            nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");

            var bodyNode = xmlDocument.SelectSingleNode("soap:Envelope/soap:Body", nsmgr);

            if (bodyNode.FirstChild.HasChildNodes)
                return bodyNode.FirstChild.FirstChild.InnerXml;

            return null;
        }
    }
}
