using Bagging.BagEdit.Models;
using Bagging.BagEdit.Repositories;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Bagging.BagEdit.Controllers {
    internal class Controller {

        internal static readonly AppSettings SETTINGS = new AppSettings("AppSettings.json");
        internal static bool DbStatus { get; set; }

        internal static readonly string pendingQueriesFilePath = string.Format(@"{0}\{1}", Directory.GetCurrentDirectory(), SETTINGS.PendingUpdateFilePath);
        internal static readonly string qualityValuesFilePath = string.Format(@"{0}\{1}", Directory.GetCurrentDirectory(), SETTINGS.QualityValuesFilePath);

        public static readonly ushort SITE_ID = SETTINGS.SiteId;
        public static readonly uint[] ProdyctTypeListAsInt = SETTINGS.ProdyctTypeListAsInt;
        public static readonly decimal MIN_WEIGHT = SETTINGS.MinWeight;
        public static readonly decimal MAX_WEIGHT = SETTINGS.MaxWeight;
        public static readonly uint DAYS_IN_THE_PAST = SETTINGS.DaysInThePast;
        public static readonly string[] PRODUCT_TYPE_LIST = SETTINGS.ProdyctTypeList;

        private static async Task<uint> RunQueriesAsync() {
            Dictionary<Model, string> bagList = FileRepository.ReadModelsFromFile(SETTINGS.PendingUpdateFilePath, SETTINGS.Delimiter, typeof(Model));
            List<Model> savedModels = new List<Model>();
            uint result = 0;

            if (bagList == null || bagList.Count == 0) {
                return result;
            }

            Dictionary<Model, string> tempBagList = new Dictionary<Model, string>(bagList);
            Model response = null;

            foreach (var bagKeyValuePair in tempBagList) {
                //Console.WriteLine(bagKeyValuePair.Key.GetType());

                if (bagKeyValuePair.Value == "INSERT") {
                    if(typeof(BagModel) == bagKeyValuePair.Key.GetType()) {
                        BagModel model = bagKeyValuePair.Key as BagModel;
                        response = await MySql_Edit_Repository.SaveNewBagToDBAsync(model);
                    } 
                    else if (typeof(ReseedModel) == bagKeyValuePair.GetType()) {
                        ReseedModel model = bagKeyValuePair.Key as ReseedModel;
                        response = await MySql_Reseed_Repository.SaveNewReseedToDBAsync(model);
                    }
                } 
                else if (bagKeyValuePair.Value == "UPDATE") {
                    if (typeof(BagModel) == bagKeyValuePair.Key.GetType()) {
                        BagModel model = bagKeyValuePair.Key as BagModel;
                        response = await MySql_Edit_Repository.UpdateBagAsync(model);
                    } 
                    else if (typeof(ReseedModel) == bagKeyValuePair.GetType()) {
                        ReseedModel model = bagKeyValuePair.Key as ReseedModel;
                        response = await MySql_Reseed_Repository.UpdateReseedAsync(model);
                    }
                }
                if (response != null) {
                    savedModels.Add(response);
                    bagList.Remove(bagKeyValuePair.Key);
                    ++result;
                }
            }
            if (bagList.Count > 0) {
                FileRepository.SaveRemainingModelsToFile(bagList, SETTINGS.PendingUpdateFilePath, SETTINGS.Delimiter);
            }
            else {
                FileRepository.LogSavedModelsToFile(savedModels, SETTINGS.SavedBagsLogFile, SETTINGS.Delimiter);
                FileRepository.DeleteFile(SETTINGS.PendingUpdateFilePath);
            }
            return result;
        }

        /// <summary>
        /// Run pending queries asynchronously from an specific file
        /// </summary>
        /// <returns>Task<string></returns>
        internal static async Task<string> RunPendingqueriesAsync() {
            string message = string.Empty;
            if (File.Exists(pendingQueriesFilePath)) {
                await Task.Run(async () => { //Runs all the pending queries that are saved in file in a separate thread
                    uint result = await RunQueriesAsync();
                    if (0 < result) {
                        message = $"{result} pending bags saved to database. Bags Saved to DB";
                        File.Delete(SETTINGS.PendingUpdateFilePath);
                    } else {
                        message = $"{result} pending bags not saved to database. MySql error.";
                    }
                });
            }
            return message;
        }

        /// <summary>
        ///  Checks if database is online
        /// </summary>
        /// <returns>Task<bool></returns>
        public static async Task<bool> CheckDBConnection() {
            string query = @"SELECT bagNo 
                                FROM plcrecipes.baggedproduct
                                ORDER BY bagNo DESC 
                                LIMIT 1";
            try {
                using (MySqlConnection connection = new MySqlConnection(SETTINGS.ConnectionString)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection)) {
                        object response = await command.ExecuteScalarAsync();
                        if(response != null) {
                            DbStatus = true;
                            return true;
                        }
                    }
                }
            } catch (MySqlException ex) {
                Console.WriteLine($"CheckConnection() => {ex.Message}");
                if(!ex.Message.Contains("syntax"))
                    return false;
            } 
            catch (Exception ex) {
                Console.WriteLine($"CheckConnection() => {ex.Message}\n{ex.GetType()}");
            }
            return false;
            
        }
    }
}
