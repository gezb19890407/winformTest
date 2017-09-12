using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 二维码
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FWImageHelper.CreateQRCodeImageCommon("http://58.210.204.106:60001/8006/web/uploadfile/csyjxc/Android/csyjxc.apk", 250, null);
        }
    }
}
