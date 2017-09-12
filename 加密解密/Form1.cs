using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FtpLib;
using Project.Common.Safe;

namespace 加密解密
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = Help_Encrypt.Encode(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = Help_Encrypt.Decode(textBox1.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = RSAHelper.EncryptionString(textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = RSAHelper.DecryptString(textBox1.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox2.Text = MD5Helper.DecryptString(textBox1.Text);
        }
    }
}
