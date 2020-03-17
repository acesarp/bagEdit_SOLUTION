using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.IO;
using Bagging.Controller.Models;
using Bagging.Controller.Repositories;
using log4net;

namespace Bagging.Controller.Controllers {
    public class EditController : MainController {
        private static ILog log = LogManager.GetLogger(typeof(EditController));
        public static Dictionary<int, string> GetQualityValues() {
            log.Debug("Getting Quality Values...");
            Dictionary<int, string> qualityValues;
            if (DbStatus) {
                qualityValues = MySql_Edit_Repository.GetQualityValues();

                if (qualityValues != null) {
                    FileRepository.SaveQualityValuesToFile(qualityValuesFilePath, qualityValues.Keys.Zip(qualityValues.Values, (k, v) => k + SETTINGS.Delimiter + v).ToArray());
                }
            }
            else {
                qualityValues = FileRepository.ReadQualityValuesFromFile(qualityValuesFilePath, SETTINGS.Delimiter);
            }
            return qualityValues;

        }

        public static async Task<BagModel> UpdateBagAsync(BagModel originalBag, BagModel newBag) {
            if (DbStatus) {
                return await MySql_Edit_Repository.UpdateBagAsync(newBag);
            } 
            else if(newBag.UniqueIndentifier == 0){
                return (BagModel) FileRepository.UpdateModelInFile(originalBag, newBag, pendingQueriesFilePath, SETTINGS.Delimiter);
            }
            return null;
        }

        public static async Task<BagModel[]> GetBagsAsync() {
            BagModel[] result;
            if (DbStatus) {
                result = await GetDataFromDBAsync();
            } else {
                result = GetDataFromFile();
            }
            return result;
        }

        private static BagModel[] GetDataFromFile() {
            if (File.Exists(SETTINGS.PendingUpdateFilePath)) {
                Model[] originalArray = FileRepository.ReadModelsFromFile(SETTINGS.PendingUpdateFilePath, SETTINGS.Delimiter, typeof(BagModel)).Keys.ToArray();
                var newArray = Array.ConvertAll(originalArray, item => (BagModel)item);
                return newArray;
            }
            return null;
        }

        private static async Task<BagModel[]> GetDataFromDBAsync() {
            return await MySql_Edit_Repository.GetDataFromDBAsync(SETTINGS.SelectCommand);
        }


        public static string Print(Bitmap img, ushort copies) {
            try {
                PrintFunctions.Print(img, SETTINGS.PrinterIp, SETTINGS.PrinterPort, copies);
                return string.Empty;
            }catch (System.Net.Sockets.SocketException ex) {
                return ex.Message;
            }
        }

        public static Bitmap BuildLabel(BagModel bag, Bitmap logo) {
            return PrintFunctions.BuildLabel(bag, logo);
        }

        public static async Task<BagModel> SaveNewBagAsync(BagModel newBag) {
            BagModel result;
            if (DbStatus) {
                result = await SaveBagToDB(newBag);
            } else {
                result = SaveBagToFile(newBag);
            }
            return result;
        }

        private static BagModel SaveBagToFile(BagModel newBag) {
            return (BagModel) FileRepository.SaveModelToFile(newBag, SETTINGS.PendingUpdateFilePath, SETTINGS.Delimiter, "INSERT");
        }

        private static async Task<BagModel> SaveBagToDB(BagModel newBag) {
            return await MySql_Edit_Repository.SaveNewBagToDBAsync(newBag);
        }

    }
}
