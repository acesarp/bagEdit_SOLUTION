using Bagging.Controller.Models;
using Bagging.Controller.Repositories;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagging.Controller.Controllers {
    public class ReseedController : MainController {
        private static ILog log = LogManager.GetLogger(typeof(ReseedController));
        public static async Task<ReseedModel[]> GetReseedsAsync() {
            log.Debug("Getting Reseeds Asynchronously...");
            ReseedModel[] result;
            if (DbStatus) {
                result = await GetReseedFromDBAsync();
            } else {
                result = GetReseedFromFile();
            }
            return result;
        }

        private static async Task<ReseedModel[]> GetReseedFromDBAsync() {
            return await MySql_Reseed_Repository.GetReseedFromDBAsync(SETTINGS.SelectReseedCommand);
        }

        private static ReseedModel[] GetReseedFromFile() {
            log.Debug(" Getting Reseed from file...");
            if (File.Exists(SETTINGS.PendingUpdateFilePath)) {
                var result = FileRepository.ReadModelsFromFile(SETTINGS.PendingUpdateFilePath, SETTINGS.Delimiter, typeof(ReseedModel)).Keys.Cast<ReseedModel>().ToArray();

                return result;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newReseed"></param>
        /// <exception cref="MySql.Data.MySqlClient.MySqlException"></exception>
        /// <returns></returns>
        public static async Task<ReseedModel> SaveNewReseedAsync(ReseedModel newReseed) {
            log.Debug(" Saving new Reseed Asynchronously...");
            ReseedModel result;
            if (DbStatus) {
                try {
                    result = await SaveReseedToDBAsync(newReseed);
                } 
                catch (MySql.Data.MySqlClient.MySqlException ex) {
                    throw ex;
                }
            } 
            else {
                result = SaveReseedToFile(newReseed);
            }
            return result;
        }

        public static async Task<ReseedModel> UpdateReseedAsync(ReseedModel originalReseed, ReseedModel newReseed) {
            log.Debug("Updating Reseed Asynchronously...");
            if (DbStatus) {
                return await MySql_Reseed_Repository.UpdateReseedAsync(newReseed);
            } else if (newReseed.UniqueIndentifier == 0) {
                return (ReseedModel)FileRepository.UpdateModelInFile(originalReseed, newReseed, pendingQueriesFilePath, SETTINGS.Delimiter);
            }
            return null;
        }

        private static ReseedModel SaveReseedToFile(ReseedModel newReseed) {
            return (ReseedModel)FileRepository.SaveModelToFile(newReseed, SETTINGS.PendingUpdateFilePath, SETTINGS.Delimiter, "INSERT");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newReseed"></param>
        /// <exception cref="MySql.Data.MySqlClient.MySqlException"></exception>
        /// <returns>ReseedModel object</returns>
        private static async Task<ReseedModel> SaveReseedToDBAsync(ReseedModel newReseed) {
            log.Debug("Saving Reseed to Db Asynchronously...");
            try {
                return await MySql_Reseed_Repository.SaveNewReseedToDBAsync(newReseed);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex) {
                throw ex;
            }
        }
    }
}