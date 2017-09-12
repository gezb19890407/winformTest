using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 图片上加字
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }


        /// <summary>
        /// 给导出的图片加上文字描述
        /// </summary>
        /// <param name="filePath"></param>
        protected void SetPicDescription(string filePath, string description)
        {
            if (System.IO.File.Exists(filePath))//看该路径下图片是否存在
            {
                string strDescription = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");//文字描述
                System.IO.MemoryStream ms = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(filePath));
                Image image = Image.FromStream(ms);
                Graphics g = Graphics.FromImage(image);
                g.DrawString(strDescription, new Font("宋体", 25, FontStyle.Bold), Brushes.Goldenrod, new PointF(image.Width - 380, image.Height - 40));
                System.IO.File.Delete(filePath);
                image.Save(filePath);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetPicDescription("C:/Users/Public/Pictures/Sample Pictures/test1.jpg", null);
        }
    }
}
