using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using log4net;

namespace Bagging.DataMaxPrinterUtils {
    public class LabelBuilder {

        private static readonly ILog log = LogManager.GetLogger(typeof(LabelBuilder));

        public Bitmap Img { get; private set; }
        public string LogoFilePath { get; set; }
        public string LabelImagePath { get; set; }
        public string FontName { get; set; }
        public int FontSize { get; set; }

        public ImageFormat Format { get; set; } = ImageFormat.Png;

        [DllImport("gdi32.dll", EntryPoint = "CreateDC", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr CreateDC(string lpszDriver, string lpszDeviceName, string lpszOutput, IntPtr devMode);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern Int32 GetDeviceCaps(IntPtr hdc, Int32 capindex);
        private const int LOGPIXELSX = 88;

        private static int _dpi = -1;
        public static int DPI {
            get {
                if (_dpi != -1)
                    return _dpi;

                _dpi = 96;
                try {
                    IntPtr hdc = CreateDC("DISPLAY", null, null, IntPtr.Zero);
                    if (hdc != IntPtr.Zero) {
                        _dpi = GetDeviceCaps(hdc, LOGPIXELSX);
                        if (_dpi == 0)
                            _dpi = 96;
                        DeleteDC(hdc);
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message + " type: " + ex.GetType());
                }
                return _dpi;
            }
        }

        /// <summary>
        /// Creates a blank image with the specified size
        /// </summary>
        /// <param name="width">In inches</param>
        /// <param name="height">In inches</param>
        public void LabelSize(int width, int height) {
            Console.WriteLine("Dpi: " + DPI + "height:" + height + "Width: " + width);

            //Height
            int MonitorHeightPx = Screen.PrimaryScreen.Bounds.Height;
            //Width
            int MonitorWidthinPx = Screen.PrimaryScreen.Bounds.Width;
            Console.WriteLine("Dpi: " + DPI + "MonitorHeightPx:" + MonitorHeightPx + "MonitorWidthinPx: " + MonitorWidthinPx);

            //double windowsScallingFactor = .66;
            //int displayPPI = 163;

            Img = new Bitmap(Convert.ToInt32(width * 163), Convert.ToInt32(height * 163) );
            Console.WriteLine("Dpi: " + DPI + "height:" + Img.Height + "Width: " + Img.Width);
            try {
                using (Graphics graphics = Graphics.FromImage(Img)) {
                    graphics.Clear(Color.White);
                }
            }
            catch (Exception ex) {
                log.Error("Exception thrown! ", ex);
                throw ex;

            }
        }

        /// <summary>
        /// Adds image while keeping the proportions
        /// Ex 1 = 100%; 2= 200%; .5 = 50%
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="imgLocationX"></param>
        /// <param name="imgLocationY"></param>
        /// <param name="scaleFactor"></param>
        public void AppendImage(string imagePath, int imgLocationX, int imgLocationY, double scaleFactor = 1) {
            Image logo = Image.FromFile(imagePath);
            AppendImage(logo, imgLocationX, imgLocationY, scaleFactor);
        }

        /// <summary>
        /// Adds image while keeping the proportions
        /// Ex 1 = 100%; 2= 200%; .5 = 50%
        /// </summary>
        /// <param name="logo"></param>
        /// <param name="imgLocationX"></param>
        /// <param name="imgLocationY"></param>
        /// <param name="scaleFactor"></param>
        public void AppendImage(Image logo, int imgLocationX, int imgLocationY, double scaleFactor = 1) {
        
        PointF imageLocation = new PointF(imgLocationX, imgLocationY);
            try {

                Bitmap objBitmap = new Bitmap(logo, new Size(Convert.ToInt32(logo.Width * scaleFactor), Convert.ToInt32(logo.Height * scaleFactor)));
                using (Graphics graphics = Graphics.FromImage(Img)) {
                    graphics.DrawImage(objBitmap, imageLocation);
                }
            }
            catch (FileNotFoundException ex) {
                log.Error("Exception thrown! ", ex);
            }
            catch (Exception ex) {
                log.Error("Exception thrown! ", ex);
            }
        }

            public void AddText(string text, float verticalLine) {
            float horizontalLine = 0;
                StringFormat drawFormat = new StringFormat { FormatFlags = StringFormatFlags.DisplayFormatControl };

                try {
                    using (Graphics graphics = Graphics.FromImage(Img)) {

                        using (Font font = new Font(FontName, FontSize)) {
                            graphics.DrawString(text, font, Brushes.Black, horizontalLine, verticalLine, drawFormat);
                        }
                    }
                }
                catch (ArgumentNullException ex) {
                log.Error("Exception thrown! ", ex);
            }
            }

        /// <summary>
        ///  Adds border to label 
        /// </summary>
        /// <param name="thickness"></param>
            public void AddBorder(float thickness, Color color) {
                //1008, 1488

                int top = 0;
                int bottom = 0;
                int right = 0;
                int left = 0;
                Point a = new Point(left, top);
                Point b = new Point(Img.Width - right, top);
                Point c = new Point(left, Img.Height - bottom);
                Point d = new Point(Img.Width - right, Img.Height - bottom);

                Point[] points = { a, b, d, c, a };
                try {
                    using (Graphics graphics = Graphics.FromImage(Img)) {

                        Pen pen = new Pen(color, thickness);
                        graphics.DrawLines(pen, points);
                    }
                }
                catch (Exception ex) {
                log.Error("Exception thrown! ", ex);
                }
            }

            /// <summary>
            /// Saves the image into a file
            /// </summary>
            public void Save() {
                try {
                    Img.Save(LabelImagePath, Format);//save the image file
                    //Img.Dispose();
                }
                catch(ArgumentException ex){
                log.Error("Exception thrown! ", ex);
            } catch (ExternalException ex) {
                log.Error("Exception thrown! ", ex);
            }
                catch(Exception ex) {
                log.Error("Exception thrown! ", ex);
            }
            }

        /// <summary>
        ///         Creates image from text
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        private Image ImageFromText(string text, Font font) {
            //create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            try {
                Graphics drawing = Graphics.FromImage(img);

                //measure the string to see how big the image needs to be
                SizeF textSize = drawing.MeasureString(text, font);

                //free up the dummy image and old graphics object
                img.Dispose();
                drawing.Dispose();

                //create a new image of the right size
                img = new Bitmap((int)textSize.Width, (int)textSize.Height);

                drawing = Graphics.FromImage(img);

                drawing.Clear(Color.Transparent);

                Brush textBrush = new SolidBrush(Color.Black);

                drawing.DrawString(text, font, textBrush, 0, 0);
                drawing.Save();

                textBrush.Dispose();
                drawing.Dispose();
                return img;
            }
            catch(Exception ex) {
                log.Error("Exception thrown! ", ex);
                return null;
            }
        }
    }
}
