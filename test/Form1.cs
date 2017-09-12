using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            test();
        }

        public void test()
        {
            //string Authority = System.Web.HttpContext.Current.Request.Url.Authority;
            //String WebSiteRootUrl = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + (!string.IsNullOrEmpty(Authority) ? Authority : "localhost") + "/";
            //String VirtualDirectory = System.Web.HttpContext.Current.Request.ApplicationPath.TrimStart('/');
            //if (!String.IsNullOrEmpty(VirtualDirectory))
            //{
            //    WebSiteRootUrl += VirtualDirectory + "/";
            //}
            //return WebSiteRootUrl;

            //string aPath = "D:/test/program/winform/winformTest/test/Form1.cs";
            //string b = aPath.Substring(0, aPath.LastIndexOf("/"));
            //MessageBox.Show(b);
            string url = "D:/work/常熟阳光信访/050.编码/010.源代码/主程序/Project/Project.Web/Web/FlowHtml/SunshinePetition.htm";
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
