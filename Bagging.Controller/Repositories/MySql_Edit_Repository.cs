using Bagging.Controller.Controllers;
using Bagging.Controller.Models;
using log4net;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Bagging.Controller.Repositories {
    //public enum QueryMode { INSERT = 0, UPDATE = 1 };
    public static class MySql_Edit_Repository {
        private static ILog log = LogManager.GetLogger(typeof(MySql_Edit_Repository));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MySqlException"></exception>
        internal static async Task<string> GenerateBagIdAsync() {
            string query = @"SELECT bagNo 
                                    FROM plcrecipes.baggedproduct
                                    ORDER BY bagNo DESC 
                                    LIMIT 1";
            try {
                using (MySqlConnection connection = new MySqlConnection(MainController.SETTINGS.ConnectionString)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection)) {
                        Console.WriteLine(command.ExecuteScalar());
                        return (Convert.ToInt32(await command.ExecuteScalarAsync()) + 1).ToString();
                    }
                }
            }
            catch (MySqlException ex) {
                Console.WriteLine($"GenerateBagIdAsync() => {ex.Message}");
                throw ex;
            }
            catch (Exception ex) {
                Console.WriteLine($"GenerateBagIdAsync() => {ex.Message}\n{ex.GetType()}");
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MySqlException"></exception>
        internal static Dictionary<int, string> GetQualityValues() {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            string query = "SELECT idQualityValues, QualityvaluesAsString FROM plcrecipes.qualityvalues;";
            Console.WriteLine(MainController.SETTINGS.ConnectionString);
            try {
                using (MySqlConnection conn = new MySqlConnection(MainController.SETTINGS.ConnectionString)) {
                    conn.Open();
                    using (MySqlCommand command = new MySqlCommand(query, conn)) {
                        var reader = command.ExecuteReader();
                        if (reader.HasRows) {
                            while (reader.Read()) {
                                dictionary.Add(reader.GetInt32(0), reader.GetString(1));
                            }
                        }
                        reader.Dispose();
                    }
                }
            }catch(MySqlException ex) {
                log.Error($"MySqlException", ex);
                if (!ex.Message.Contains("syntax")) {
                    return null;
                }
                throw ex;
            }
            catch(Exception ex) {
                log.Error(ex.GetType(), ex);
            }
            return dictionary;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bag"></param>
        /// <returns> returns the saved bag, null if unsuccessfull</returns>
        public static async Task<BagModel> SaveNewBagToDBAsync(BagModel bag) {
            Console.WriteLine(bag.ProductDate.ToString());
            try {
                bag.UniqueIndentifier = uint.Parse(await GenerateBagIdAsync());
                using (MySqlConnection connection = new MySqlConnection(MainController.SETTINGS.ConnectionString)) {
                    connection.Open();

                    string query = $@"INSERT INTO plcrecipes.baggedproduct (bagNo, ProductDate, ProductType, ProductWeight, Site_Id, notes, qualityflag, qualityflagName)
                                            VALUES(@bagNo, @ProductDate, @ProductType, @ProductWeight, @Site_id, @notes, @QualityFlag, @QualityFlagName)";

                    using (MySqlCommand command = new MySqlCommand(query, connection)) {
                        command.Parameters.AddWithValue("@bagNo", bag.UniqueIndentifier);
                        command.Parameters.AddWithValue("@ProductDate", bag.ProductDate.ToString());
                        command.Parameters.AddWithValue("@ProductType", bag.ProductType);
                        command.Parameters.AddWithValue("@ProductWeight", bag.ProductWeight);
                        command.Parameters.AddWithValue("@Site_id", MainController.SETTINGS.SiteId);
                        command.Parameters.AddWithValue("@notes", bag.Notes);
                        command.Parameters.AddWithValue("@QualityFlag", bag.QualityFlag);
                        command.Parameters.AddWithValue("@QualityFlagName", bag.QualityFlagName);

                        int result = -2;
                        result = await command.ExecuteNonQueryAsync();
                        if(result <= 0) {
                            return null;
                        }
                    }
                }
                return bag;
            } 
            catch (MySqlException ex) {
                Console.WriteLine($"MySqlException\nSaveNewBagToDBAsync() => {ex.Message}\n{ex.StackTrace}\n{ex.Number}");
            }
            catch (Exception ex) {
                Console.WriteLine($"SaveNewBagToDBAsync() => {ex.Message}\n{ex.GetType()}");
            }
            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bag"></param>
        /// <returns></returns>
        public static  async Task<BagModel> UpdateBagAsync(BagModel bag) {
            log.Debug(" Updating Bag to database Asynchronously...");
            string commandText = $@"UPDATE plcrecipes.baggedproduct
                                    SET ProductDate = @ProductDate, 
                                         ProductType = @ProductType, 
                                         ProductWeight = @ProductWeight, 
                                         notes = @Notes,
                                         QualityFlag = @QualityFlag,
                                         QualityFlagName = @QualityFlagName
                                    WHERE BagNo = @UniqueIndentifier";
            try {
                using (MySqlConnection conn = new MySqlConnection(MainController.SETTINGS.ConnectionString)) {
                    conn.Open();
                    using (var tran = conn.BeginTransaction()) {
                        using (MySqlCommand command = new MySqlCommand(commandText, conn, tran)) {
                            command.Parameters.AddWithValue("@ProductDate", bag.ProductDate);
                            command.Parameters.AddWithValue("@ProductType", bag.ProductType);
                            command.Parameters.AddWithValue("@ProductWeight", bag.ProductWeight);
                            command.Parameters.AddWithValue("@notes", bag.Notes);
                            command.Parameters.AddWithValue("@QualityFlag", bag.QualityFlag);
                            command.Parameters.AddWithValue("@QualityFlagName", bag.QualityFlagName);
                            command.Parameters.AddWithValue("@UniqueIndentifier", bag.UniqueIndentifier);
                            try {
                                int rst = -1;
                                rst = await command.ExecuteNonQueryAsync();
                                tran.Commit();
                                if (rst > 0) {
                                    log.Debug(" Bag Updated to database successfully!");
                                    return bag;
                                }
                            } 
                            catch (InvalidOperationException ex) {
                                tran.Rollback();
                                log.Error(" Exception thrown!", ex);
                            } 
                            catch (DBConcurrencyException ex) {
                                tran.Rollback();
                                log.Error(" Exception thrown!", ex);
                            } 
                            catch (MySqlException ex) {
                                tran.Rollback();
                                log.Error(" Exception thrown!", ex);
                            } 
                            catch (Exception ex) {
                                tran.Rollback();
                                log.Error(" Exception thrown!", ex);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex) {
                log.Error(" Exception thrown!", ex);
                if (!ex.Message.ToLower().Contains("syntax")) {
                    FileRepository.SaveModelToFile(bag, MainController.SETTINGS.PendingUpdateFilePath, MainController.SETTINGS.Delimiter, "INSERT");
                }
            }
            catch (Exception ex) {
                log.Error(" Exception thrown!", ex);
            }
            return null;
        }

        /// <summary>
        ///  returns an array of BagModel objects, null if no data is retrieved
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="DBConcurrencyException"></exception>
        /// <exception cref="MySqlException"></exception>
        /// <exception cref="Exception"></exception>
        public static async Task<BagModel[]> GetDataFromDBAsync(string query) {
            List<BagModel> values = new List<BagModel>();
            try {
                using (MySqlConnection conn = new MySqlConnection(MainController.SETTINGS.ConnectionString)) {
                    conn.Open();
                    
                    using (MySqlCommand command = new MySqlCommand(query, conn)) {
                        var reader = await command.ExecuteReaderAsync();
                        if (reader.HasRows) {
                            while (reader.Read()) {
                                BagModel row = new BagModel();
                                row.UniqueIndentifier = !reader.IsDBNull(0) ? uint.Parse(reader.GetString(0)): 0; //bagNo
                                row.ProductDate = DateTime.Parse(reader.GetString(1)); //ProductDate
                                row.ProductType = !reader.IsDBNull(2) ? reader.GetString(2) : string.Empty; //ProductType
                                row.ProductWeight = !reader.IsDBNull(3) ? decimal.Parse(reader.GetString(3)) : 0; //ProductWeight
                                row.QualityFlagName = !reader.IsDBNull(4) ? reader.GetString(4) : string.Empty; ; //qualityflagName
                                row.Notes = !reader.IsDBNull(5) ? reader.GetString(5) : string.Empty; //notes
                                values.Add(row);
                            }
                        }
                        reader.Dispose();
                    }
                    return values.ToArray();
                }
            } catch (MySqlException ex) {
                Console.WriteLine("GetDataFromDBAsync() => MySqlException {0}", ex.Message);
                throw ex;
            }
            catch (InvalidOperationException ex) {
                Console.WriteLine("GetDataFromDBAsync() => InvalidOperationException {0}", ex.Message);
            }
            catch (DBConcurrencyException ex) {
                Console.WriteLine("GetDataFromDBAsync() => DBConcurrencyException {0}", ex.Message);
            }

            catch (AggregateException ex) {
                Console.WriteLine("GetDataFromDBAsync() => AggregateException {0}", ex.Message);
            }
            return null;
        }
    }
}
