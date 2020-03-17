using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Windows.Forms;

namespace Bagging.BagEdit {
    static class Program {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            
            if (!Directory.Exists("C:\\runtimeLogFiles")) {
                Directory.CreateDirectory(".\\runtimeLogFiles");
            }
            XmlConfigurator.Configure(new FileInfo($@".\log4net.config"));
            log.Debug($"Entering application. {DateTime.Now}");
            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            FormLoadOption option = new FormLoadOption();

            option.ShowDialog();

            if (option.Choice == ScreenChoice.Edit) {

                Application.Run(new FormBagEdit());
                log.Debug($"Exiting application. {DateTime.Now}");
            }
            else if(option.Choice == ScreenChoice.Reseed) {
                Application.Run(new FormReseed());
                log.Debug($"Exiting application. {DateTime.Now}");
            }
            else {
                log.Debug($"Exiting application. {DateTime.Now}");
                Environment.Exit(0);
            }
        }
    }
}