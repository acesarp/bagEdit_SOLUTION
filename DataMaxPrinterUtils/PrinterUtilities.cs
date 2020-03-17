using System;
using Honeywell.Connection;
using Honeywell.Printer;
using Honeywell.Printer.Configuration.DPL;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;

namespace Bagging.DataMaxPrinterUtils {
    public class PrinterUtilities {

        public DocumentDPL DocDPL { get; set; }
        public ParametersDPL ParamDPL { get; set; }

        private string ip;
        private int port;
        public PrinterUtilities(string ip, int port) {
            this.ip = ip;
            this.port = port;
            DocDPL = new DocumentDPL();
            ParamDPL = new ParametersDPL();
            CheckIpFormat(ip); //throws an exception if ip is in an invalid format

        }

        public void PrintJob(byte[] page, ushort copies = 1) {
            var conn = Connection_TCP.CreateClient(ip, port);
            new PrintSettings_DPL(conn);
            try {
                conn?.Open();
                ushort i = 0;
                while (copies > i) {
                    Console.WriteLine(string.Format("[{0:MM/dd/yyy hh:mm:ss.fff}]", System.DateTime.Now) + " Checking if printer is ready..\r\n");
                    conn.Write(page);
                    ++i;
                }
            }
            catch(System.Net.Sockets.SocketException ex) {
                Console.WriteLine("PrintJob Exception: " + ex.Message + " Socket error: " + ex.InnerException?.Message + " code: " + ex.SocketErrorCode);
            } 
            finally {
                Console.WriteLine($"Connection closed. {conn.BytesAvailable}");
                conn.Dispose();
            }
        }

        public void PrintJob(DocumentDPL DocDPL, ushort copies = 1) {
            var conn = Connection_TCP.CreateClient(ip, port);
            PrinterStatus_DPL printerStatus = new PrinterStatus_DPL(conn);

            try {
                conn.Open();
                ushort i = 0;
                while (copies < i) {
                    conn.Write(DocDPL.GetDocumentData());
                    ++i;
                }
                conn.ClearReadBuffer();
                conn.ClearSizeCounters();
                conn.ClearWriteBuffer();
            } 
            catch (System.Net.Sockets.SocketException ex) {
                Console.WriteLine("PrintJob Exception: " + ex.Message + " Socket error: " + ex.InnerException?.Message + " code: " + ex.SocketErrorCode);
                throw ex;
            } 
            finally{
                conn.Close();
                Console.WriteLine($"Connection closed. {conn.BytesAvailable}");
                conn.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="img"></param>
        /// <param name="copies"></param>
        /// <exception cref="System.Net.Sockets.SocketException"></exception>
        public void PrintJob(Bitmap img, ushort copies = 1) {
            var conn = Connection_TCP.CreateClient(ip, port);
            ParametersDPL paramDPL = new ParametersDPL();
            DocDPL.WriteImage(img, 0, 0, paramDPL);
            byte[] doc = DocDPL.GetDocumentData();
            try {
                conn.Open();
                ushort i = 0;
                while (copies > i) {
                    conn.Write(doc);
                    ++i;
                }
            }
            catch(System.Net.Sockets.SocketException ex) {
                //Console.WriteLine("PrintJob Exception: " + ex.Message + " Socket error: " + ex.InnerException?.Message + " code: " + ex.SocketErrorCode);
                throw ex;
            } finally {
                conn?.Close();
               //Console.WriteLine($"Connection closed. {conn.BytesAvailable}");
                conn?.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="img"></param>
        /// <param name="copies"></param>
        /// <exception cref="System.Net.Sockets.SocketException"></exception>
        public void PrintJobBitmap(Bitmap img, ushort copies = 1) {
            var conn = Connection_TCP.CreateClient(ip, port);
            ParametersDPL paramDPL = new ParametersDPL();
            DocDPL.WriteImage(img, 0, 0, paramDPL);
            byte[] doc = DocDPL.GetDocumentData();
            try {
                conn.Open();
                ushort i = 0;
                while (copies > i) {
                    conn.Write(doc);
                    ++i;
                }
            } catch (System.Net.Sockets.SocketException ex) {
                Console.WriteLine("PrintJob Exception: " + ex.Message + " Socket error: " + ex.InnerException?.Message + " code: " + ex.SocketErrorCode);
                throw ex;
            } finally {
                conn?.Close();
                Console.WriteLine($"Connection closed. {conn.BytesAvailable}");
                conn?.Dispose();
            }
        }

        private bool GetStatus() {
            var conn = Connection_TCP.CreateClient(ip, port);
            PrinterStatus_DPL printerStatus = new PrinterStatus_DPL(conn);
                //Query for printer status
                printerStatus.QueryPrinter(500);

                //Unable to retreive data from query
                if (printerStatus.Valid == false)
                    return false;

                PrinterStatus_DPL.PrinterStatus currentStatus = printerStatus.CurrentStatus;

                if (currentStatus != PrinterStatus_DPL.PrinterStatus.PrinterReady) {
                    return false;
                }
            
            return true;
        }

        private void CheckIpFormat(string ip_) {
            if (!Regex.IsMatch(ip_, "^([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])$"))
                throw new Exception("Invalid IP Address format entered.");
        }

        public string Query() {
            var conn = Connection_TCP.CreateClient(ip, port);
            //====DPL Printers(eg. RL3, RL4, etc.)========//
            //Query Printer info
            PrinterInformation_DPL printerInfo = new PrinterInformation_DPL(conn);
            printerInfo.QueryPrinter(3000);
            if (printerInfo.Valid == false) {
                return "No response from printer\r\n";
            }
            else {
                return  String.Format("Firmware Version: {0}\n", printerInfo.VersionInformation);
            }
        }
    }
}

