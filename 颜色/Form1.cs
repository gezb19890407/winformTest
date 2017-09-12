using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 颜色
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Color color = new Color();
            string colorStr = null;
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                colorStr = textBox1.Text;
                if (colorStr.StartsWith("#"))
                {
                    color = ColorTranslator.FromHtml(colorStr);
                }
                else if (colorStr.StartsWith("rgb"))
                {
                    colorStr = colorStr.Replace("rgb", "").Replace("(", "").Replace(")", "").Replace("%", "");
                    string[] rgb = colorStr.Split(',');
                    color = Color.FromArgb(Int32.Parse(rgb[0]), Int32.Parse(rgb[1]), Int32.Parse(rgb[2]));
                }
                else {
                    color = Color.FromName(colorStr);
                }
                panel1.BackColor = color;
            }
        }
    }
}
