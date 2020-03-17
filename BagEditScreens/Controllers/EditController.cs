using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bagging.BagEdit.Repositories;
using Bagging.BagEdit.Models;
using Bagging.BagEdit.Controllers;
using Bagging.BagEdit.Properties;

namespace Bagging.BagEdit {
    internal class EditController : Controller {

        internal static Dictionary<int, string> GetQualityValues() {
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

        internal static async Task<BagModel> UpdateBagAsync(BagModel originalBag, BagModel newBag) {
            if (DbStatus) {
                return await MySql_Edit_Repository.UpdateBagAsync(newBag);
            } 
            else if(newBag.UniqueIndentifier == 0){
                return (BagModel) FileRepository.UpdateModelInFile(originalBag, newBag, pendingQueriesFilePath, SETTINGS.Delimiter);
            }
            return null;
        }

        internal static async Task<BagModel[]> GetBagsAsync() {
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
                return (BagModel[]) FileRepository.ReadModelsFromFile(SETTINGS.PendingUpdateFilePath, SETTINGS.Delimiter, typeof(BagModel)).Keys.ToArray();
            }
            return null;
        }

        private static async Task<BagModel[]> GetDataFromDBAsync() {
            return await MySql_Edit_Repository.GetDataFromDBAsync(SETTINGS.SelectCommand);
        }


        internal static string Print(System.Drawing.Bitmap img, ushort copies) {
            try {
                PrintFunctions.Print(img, SETTINGS.PrinterIp, SETTINGS.PrinterPort, copies);
                return string.Empty;
            }catch (System.Net.Sockets.SocketException ex) {
                return ex.Message;
            }
        }

            internal static System.Drawing.Bitmap BuildLabel(BagModel bag) {
            return PrintFunctions.BuildLabel(bag, Resources.CrystalGreenLogo);
        }

        internal static async Task<BagModel> SaveNewBagAsync(BagModel newBag) {
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
