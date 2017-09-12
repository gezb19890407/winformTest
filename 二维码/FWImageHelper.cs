using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using ThoughtWorks.QRCode.Codec;

namespace 二维码
{

    public class FWImageHelper
    {
        /// <summary>
        /// 条行码保存的路径
        /// </summary>
        public static readonly String BarcodeFolderRelativePath = "Web/TemporaryFolder/Barcode/";
        //public static readonly string 
        public static string CreateQRCodeImageCommon(string content, int width, string url)
        {
            FWQRContent qrContent = new FWQRContent();
            qrContent.disString = "";
            qrContent.qrCodeContent = content;
            if (!string.IsNullOrEmpty(url))
            {
                qrContent.logoUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, url);
            }
            else
            {
                qrContent.logoUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image/logo_42x42.png");
            }
            qrContent.sideLen = width;
            qrContent.logoString = "";
            qrContent.leftColor = Color.FromArgb(63, 164, 172);
            qrContent.rightColor = Color.FromArgb(26, 75, 165);
            createQRCodeImage(qrContent);
            return qrContent.outFileName;
        }

        public static void createQRCodeImage(FWQRContent qrContent)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 20;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
            Bitmap image = qrCodeEncoder.Encode(qrContent.qrCodeContent);

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BarcodeFolderRelativePath);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            Bitmap noColorBitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb); //宽度长度 

            Graphics graphics = Graphics.FromImage(noColorBitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawImage(image, 0, 0);
            image.Dispose();

            //线性渐变颜色
            Bitmap linearGradientBitmap = linearGradient(noColorBitmap.Width, noColorBitmap.Height, qrContent.leftColor, qrContent.rightColor);

            Color leftTopColor = qrContent.leftTopColor;
            int num = 140;
            try
            {
                num -= (Encoding.UTF8.GetBytes(qrContent.qrCodeContent).Length - 20) / 2;
            }
            catch (Exception)
            {
            }
            //刷上线性渐变颜色
            Color tempColor;
            Color pixel;
            for (int i = 0; i < noColorBitmap.Width; i++)
            {
                for (int j = 0; j < noColorBitmap.Height; j++)
                {
                    pixel = noColorBitmap.GetPixel(i, j);
                    if ((i < num) && (j < num))
                    {
                        tempColor = ((pixel.A == 255) && (pixel.B == 0)) ? leftTopColor : pixel;
                    }
                    else
                    {
                        tempColor = ((pixel.A == 255) && (pixel.B == 0)) ? linearGradientBitmap.GetPixel(i, j) : pixel;
                    }
                    noColorBitmap.SetPixel(i, j, tempColor);
                }
            }
            linearGradientBitmap.Dispose();
            float emSize = 32f;
            emSize -= (qrContent.disString.Length - 4) * 1.8f;
            Font font = new Font(qrContent.disStringFontFamily, emSize, FontStyle.Bold);
            //获取top的矩形的宽度和高度
            SizeF ef = new SizeF();
            ef = graphics.MeasureString(qrContent.disString, font);
            //画布中间的像素计算
            float minNum = (noColorBitmap.Width - ef.Width) / 2f;
            Brush leftBottomBrush = new SolidBrush(qrContent.leftBottomColor);
            Brush whiteBrush = new SolidBrush(Color.White);
            int y = 50;
            graphics.FillRectangle(whiteBrush, new Rectangle((int)minNum, y, (int)ef.Width, (int)ef.Height));
            graphics.DrawString(qrContent.disString, font, leftBottomBrush, (float)((int)minNum), (float)y);
            Brush minEllipseBrush = new SolidBrush(qrContent.minEllipseColor);
            int width = 140;
            //graphics.FillEllipse(whiteBrush, (noColorBitmap.Width - width) / 2, (noColorBitmap.Height - width) / 2, width, width);
            //int numBigEllipse = 128;
            //graphics.FillEllipse(minEllipseBrush, (noColorBitmap.Width - numBigEllipse) / 2, (noColorBitmap.Height - numBigEllipse) / 2, numBigEllipse, numBigEllipse);
            //int numSmallEllipse = 110;
            //graphics.FillEllipse(whiteBrush, (noColorBitmap.Width - numSmallEllipse) / 2, (noColorBitmap.Height - numSmallEllipse) / 2, numSmallEllipse, numSmallEllipse);
            float minEmSize = 32f;
            minEmSize -= (qrContent.logoString.Length - 3) * 3.5f;
            Font logoStringFont = new Font(qrContent.logoStringFontFamily, minEmSize, FontStyle.Bold);
            ef = graphics.MeasureString(qrContent.logoString, logoStringFont);
            float x = ((noColorBitmap.Width - ef.Width) / 2f) + 2f;
            float logoStringNum = ((noColorBitmap.Height - ef.Height) / 2f) + 8f;
            graphics.DrawString(qrContent.logoString, logoStringFont, leftBottomBrush, x, logoStringNum);
            string url = null;
            if (!string.IsNullOrEmpty(qrContent.logoUrl))
            {
                url = qrContent.logoUrl;
            }
            else
            {
                url = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources\\logo.png");
            }
            int height1 = noColorBitmap.Width / qrContent.sideLen * 42;
            graphics.DrawImage(Image.FromFile(url), new Point((noColorBitmap.Width - height1) / 2, (noColorBitmap.Height - height1) / 2));
            //graphics.DrawImage(Image.FromFile(url), new Point((noColorBitmap.Width - width) / 2, (noColorBitmap.Height - width) / 2));
            graphics.Dispose();
            //这张是最终二维码的图
            string fileOldPath = Path.Combine(filePath, Guid.NewGuid() + ".jpg");
            qrContent.outFileName = Guid.NewGuid() + ".jpg";
            string fileNewPath = Path.Combine(filePath, qrContent.outFileName);
            noColorBitmap.Save(fileOldPath);
            //缩放图片(缩放太小有可能扫描不出来)
            cutForSquare(fileOldPath, fileNewPath, qrContent.sideLen, qrContent.sideLen);
            qrContent.outFilePath = fileNewPath;
            qrContent.FileActualUrl = fileOldPath;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileOldUrl"></param>
        /// <param name="fileSaveUrl"></param>
        /// <param name="side"></param>
        /// <param name="quality"></param>
        private static void cutForSquare(string fileOldUrl, string fileSaveUrl, int side, int quality)
        {
            //创建目录
            string dir = Path.GetDirectoryName(fileSaveUrl);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            //原始图片（获取原始图片创建对象，并使用流中嵌入的颜色管理信息）
            System.Drawing.Image initImage = System.Drawing.Image.FromFile(fileOldUrl, true);

            //原图宽高均小于模版，不作处理，直接保存
            if (initImage.Width <= side && initImage.Height <= side)
            {
                initImage.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                //原始图片的宽、高
                int initWidth = initImage.Width;
                int initHeight = initImage.Height;

                //非正方型先裁剪为正方型
                if (initWidth != initHeight)
                {
                    //截图对象
                    System.Drawing.Image pickedImage = null;
                    System.Drawing.Graphics pickedG = null;

                    //宽大于高的横图
                    if (initWidth > initHeight)
                    {
                        //对象实例化
                        pickedImage = new System.Drawing.Bitmap(initHeight, initHeight);
                        pickedG = System.Drawing.Graphics.FromImage(pickedImage);
                        //设置质量
                        pickedG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        pickedG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        //定位
                        Rectangle fromR = new Rectangle((initWidth - initHeight) / 2, 0, initHeight, initHeight);
                        Rectangle toR = new Rectangle(0, 0, initHeight, initHeight);
                        //画图
                        pickedG.DrawImage(initImage, toR, fromR, System.Drawing.GraphicsUnit.Pixel);
                        //重置宽
                        initWidth = initHeight;
                    }
                    //高大于宽的竖图
                    else
                    {
                        //对象实例化
                        pickedImage = new System.Drawing.Bitmap(initWidth, initWidth);
                        pickedG = System.Drawing.Graphics.FromImage(pickedImage);
                        //设置质量
                        pickedG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        pickedG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        //定位
                        Rectangle fromR = new Rectangle(0, (initHeight - initWidth) / 2, initWidth, initWidth);
                        Rectangle toR = new Rectangle(0, 0, initWidth, initWidth);
                        //画图
                        pickedG.DrawImage(initImage, toR, fromR, System.Drawing.GraphicsUnit.Pixel);
                        //重置高
                        initHeight = initWidth;
                    }

                    //将截图对象赋给原图
                    initImage = (System.Drawing.Image)pickedImage.Clone();
                    //释放截图资源
                    pickedG.Dispose();
                    pickedImage.Dispose();
                }

                //缩略图对象
                System.Drawing.Image resultImage = new System.Drawing.Bitmap(side, side);
                System.Drawing.Graphics resultG = System.Drawing.Graphics.FromImage(resultImage);
                //设置质量
                resultG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                resultG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //用指定背景色清空画布
                resultG.Clear(Color.White);
                //绘制缩略图
                resultG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, side, side), new System.Drawing.Rectangle(0, 0, initWidth, initHeight), System.Drawing.GraphicsUnit.Pixel);

                //关键质量控制
                //获取系统编码类型数组,包含了jpeg,bmp,png,gif,tiff
                ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo ici = null;
                foreach (ImageCodecInfo i in icis)
                {
                    if (i.MimeType == "image/jpeg" || i.MimeType == "image/bmp" || i.MimeType == "image/png" || i.MimeType == "image/gif")
                    {
                        ici = i;
                    }
                }
                EncoderParameters ep = new EncoderParameters(1);
                ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality);
                //保存缩略图
                resultImage.Save(fileSaveUrl, ici, ep);
                //释放关键质量控制所用资源
                ep.Dispose();
                //释放缩略图资源
                resultG.Dispose();
                resultImage.Dispose();
                //释放原始图片资源
                initImage.Dispose();
            }
        }
        /// <summary>
        /// 线性渐变封装
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        private static Bitmap linearGradient(int w, int h, Color from, Color to)
        {
            Bitmap image = new Bitmap(w, h, PixelFormat.Format32bppArgb);
            Rectangle rect = new Rectangle(0, 0, w, h);
            LinearGradientBrush brush = null;
            brush = new LinearGradientBrush(rect, from, to, LinearGradientMode.Horizontal);
            Graphics graphics = Graphics.FromImage(image);
            graphics.FillRectangle(brush, rect);
            graphics.Dispose();
            return image;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="path"></param>
        public static void String2File(string str, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Append);
            StreamWriter sr = new StreamWriter(fs);
            sr.WriteLine(str);
            sr.Close();
            fs.Close();
        }


        /// <summary>

        /// 將 Byte 陣列轉換為 Image。

        /// </summary>

        /// <param name="byteArray">Byte 陣列。</param>       
        public static Image byteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0) { return null; }
            byte[] data = null;
            Image oImage = null;
            Bitmap oBitmap = null;
            //建立副本
            data = (byte[])byteArray.Clone();
            try
            {
                MemoryStream oMemoryStream = new MemoryStream(byteArray);
                oMemoryStream.Position = 0;
                oImage = System.Drawing.Image.FromStream(oMemoryStream);
                oBitmap = new Bitmap(oImage);
            }
            catch
            {
                throw;
            }
            return oBitmap;
        }


        /// <summary>

        /// 將 Image 轉換為 Byte 陣列。

        /// </summary>

        /// <param name="Image">Image 。</param>

        /// <param name="imageFormat">指定影像格式。</param>       
        public static byte[] imageToByteArray(Image Image, System.Drawing.Imaging.ImageFormat imageFormat)
        {

            if (Image == null) { return null; }

            byte[] data = null;

            using (MemoryStream oMemoryStream = new MemoryStream())
            {
                using (Bitmap oBitmap = new Bitmap(Image))
                {
                    oBitmap.Save(oMemoryStream, imageFormat);
                    oMemoryStream.Position = 0;
                    data = new byte[oMemoryStream.Length];
                    oMemoryStream.Read(data, 0, Convert.ToInt32(oMemoryStream.Length));
                    oMemoryStream.Flush();

                }
            }
            return data;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] FileToByte(string path)
        {
            FileStream stream = null;
            try
            {
                stream = File.OpenRead(path);
                MemoryStream ms = new MemoryStream();
                int pos;
                while ((pos = stream.ReadByte()) != -1)
                {
                    ms.WriteByte((byte)pos);
                }
                return ms.ToArray();
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }
    }
}
