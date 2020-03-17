
using Bagging.Controller.Models;
using Bagging.DataMaxPrinterUtils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagging.BagEdit {
    public static class PrintFunctions {
        public static Bitmap BuildLabel(BagModel bag, Bitmap logo) {

            LabelBuilder builder = new LabelBuilder();
            string lotNumber = $"{Convert.ToInt32(bag.SiteId).ToString("000")}-{bag.UniqueIndentifier.ToString("00000")}"; // (3, "0") + SITE_ID, 3) + "-" + Right(String(5, "0") + bagNumber, 5);
            string LABEL_IMAGE_PATH = Directory.GetCurrentDirectory() + "images\\label.png";
            //Console.WriteLine(LABEL_IMAGE_PATH);

            builder.LabelImagePath = LABEL_IMAGE_PATH;
            builder.Format = ImageFormat.Png;

            builder.LabelSize(4, 6); // in inches
            builder.AppendImage(logo, 0, 0, .50);
            builder.AddBorder(2, Color.Red);

            builder.FontSize = 26;
            builder.FontName = "arial";
            builder.AddText($"{"5-28-0 with 10%Mg",45}", 130);
            builder.AddText($"     Product:           {bag.ProductType,16}", 280);
            builder.AddText($"     Net Weight:   {bag.ProductWeight.ToString("####0.##"),16} lbs", 340);

            builder.FontSize = 36;
            builder.AddText($"{lotNumber,24}", 440);

            builder.FontSize = 18;
            builder.AddText($"{"Crystal Green™ is sustainably produced by",56}", 560);
            builder.AddText($"{"Ostara Nutrient Recovery Technologies",56}", 585);

            builder.FontSize = 90;
            builder.FontName = "CODE-39-25";
            builder.AddText(StringCentering($"*{lotNumber}*", 15), 700); //bar code

            builder.FontSize = 14;
            builder.FontName = "arial";
            builder.AddText($"{$"*{lotNumber}*",65}", 840);
            builder.AddText($"{"Produced in USA",65}", 920);

            //builder.Save();

            int width = (int)Math.Ceiling(builder.Img.Width * 1.21);
            int height = (int)Math.Ceiling(builder.Img.Height * 1.21);
            Size picSize = new Size(width, height);
            new Bitmap(builder.Img, picSize).Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\pic2222.jpg");

            return new Bitmap(builder.Img, picSize);
        }
        
        public static string StringCentering(string s, int desiredLength) {
            if (s.Length >= desiredLength) return s;
            int firstpad = (s.Length + desiredLength) / 2;
            return s.PadLeft(firstpad).PadRight(desiredLength);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="printerIp"></param>
        /// <param name="printerPort"></param>
        /// <param name="copies"></param>
        /// <exception cref="System.Net.Sockets.SocketException"></exception>
        public static void Print(Bitmap image, string printerIp, int printerPort, ushort copies = 1) {
            PrinterUtilities printer = new PrinterUtilities(printerIp, printerPort);
            try {
                printer.PrintJob(image, copies);
            }catch(System.Net.Sockets.SocketException ex) {
                throw ex;
            }
        }

    }
}
