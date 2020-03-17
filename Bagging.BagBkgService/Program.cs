using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Bagging.Controller.Controllers;
using Bagging.Controller.Models;

namespace Bagging.BagBkgService {
    class Program {

        public static ContextMenu menu;
        public static MenuItem mnuExit;
        public static NotifyIcon notificationIcon;

            static void Main(string[] args) {
            Thread notifyThread = new Thread(
                delegate () {
                    menu = new ContextMenu();
                    mnuExit = new MenuItem("Exit");
                    menu.MenuItems.Add(0, mnuExit);
                    Icon icon = new Icon(@"..\..\Ostara_logo_no_bkg.ico");
                    notificationIcon = new NotifyIcon() {
                        Icon = icon,
                        ContextMenu = menu,
                        Text = "Ostara bagging service"
                    };
                    mnuExit.Click += new EventHandler(MenuExit_Click);

                    notificationIcon.Visible = true;
                    Application.Run();
                });

            notifyThread.Start();

            try {
                if (args.Length > 0) {
                    Console.WriteLine("Weight: " + args[0]);
                    Console.WriteLine("Bag quality: " + args[1]);
                    Convert.ToByte(args[1]);

                    BagModel bag = new BagModel();
                    bag.ProductWeight = Convert.ToDecimal(args[0]);
                    bag.ProductDate = DateTime.Now;
                    bag.SiteId = 10;
                    if (Convert.ToInt32(args[1]) == 1) {
                        bag.Notes = "YES";
                    }
                    else if (Convert.ToInt32(args[1]) == 0) {
                        bag.Notes = "NO";
                    }
                    else {
                        bag.Notes = args[1];
                    }

                    bag.QualityFlag = 0;
                    bag.QualityFlagName = "None";
                    bool dbonline = MainController.CheckDBConnection().Result;

                    var entityResult = EditController.SaveNewBagAsync(bag).Result;
                    if (entityResult != null) {
                        notificationIcon.Dispose();
                        Application.Exit();
                        Environment.Exit(0);
                        Console.ReadKey();
                    }
                    else {
                        notificationIcon.Dispose();
                        Application.Exit();
                        Environment.Exit(1);
                        Console.ReadKey();
                    }
                }
                else {
                    Console.ReadKey();
                }
            }
            catch (Exception ex) {
                if (ex.InnerException != null) {
                    Console.WriteLine($"{ex.InnerException.Message}\n{ex.InnerException.StackTrace}\n{ex.GetType()}");
                }
                Console.WriteLine(ex.Message + " " + ex.StackTrace + " " + ex.GetType());
                Environment.Exit(1);
            }
        }

        static void MenuExit_Click(object sender, EventArgs e) {
            notificationIcon.Dispose();
            Application.Exit();
        }
    }
}
