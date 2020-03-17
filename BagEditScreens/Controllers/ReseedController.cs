using Bagging.BagEdit.Models;
using Bagging.BagEdit.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagging.BagEdit.Controllers {
    internal class ReseedController : Controller {

        internal static async Task<ReseedModel[]> GetReseedsAsync() {
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
        internal static async Task<ReseedModel> SaveNewReseedAsync(ReseedModel newReseed) {
            ReseedModel result;
            if (DbStatus) {
                try {
                    result = await SaveReseedToDB(newReseed);
                } catch (MySql.Data.MySqlClient.MySqlException ex) {
                    throw ex;
                }
            } else {
                result = SaveReseedToFile(newReseed);
            }
            return result;
        }

        internal static async Task<ReseedModel> UpdateReseedAsync(ReseedModel originalReseed, ReseedModel newReseed) {
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
        private static async Task<ReseedModel> SaveReseedToDB(ReseedModel newReseed) {
            try {
                return await MySql_Reseed_Repository.SaveNewReseedToDBAsync(newReseed);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex) {
                throw ex;
            }
        }
    }
}