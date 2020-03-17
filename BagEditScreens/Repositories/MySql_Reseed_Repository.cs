using Bagging.BagEdit.Controllers;
using Bagging.BagEdit.Models;
using MySql.Data.MySqlClient;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Bagging.BagEdit.Repositories {
    //public enum QueryMode { INSERT = 0, UPDATE = 1 };

    public static class MySql_Reseed_Repository {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MySqlException"></exception>
        internal static async Task<string> GenerateReseedIdAsync() {
            string query = @"SELECT EntryNo 
                                    FROM plcrecipes.Reseededproduct
                                    ORDER BY EntryNo DESC 
                                    LIMIT 1";
            try {
                using (MySqlConnection connection = new MySqlConnection(EditController.SETTINGS.ConnectionString)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection)) {
                        Console.WriteLine(command.ExecuteScalar());
                        return (Convert.ToInt32(await command.ExecuteScalarAsync()) + 1).ToString();
                    }
                }
            }
            catch (MySqlException ex) {
                Console.WriteLine($"GenerateReseedIdAsync() => {ex.Message}");
                throw ex;
            }
            catch (Exception ex) {
                Console.WriteLine($"GenerateReseedIdAsync() => {ex.Message}\n{ex.GetType()}");
            }
            return null;
        }

        /// <summary>
        /// returns a string that is safe for use in sql queries 
        /// </summary>
        /// <param name="text">an unsafe string to be sanitized</param>
        /// <returns>returns an SQL safe string</returns>
        private static string SqlSafe(string text) {
            using (var writer = new StringWriter()) {
                using (var provider = CodeDomProvider.CreateProvider("CSharp")) {
                    provider.GenerateCodeFromExpression(new CodePrimitiveExpression(text), writer, null);
                    return writer.ToString();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reseed"></param>
        /// <exception cref="MySqlException"></exception>
        /// <returns> returns the saved reseed, null if unsuccessfull</returns>
        public static async Task<ReseedModel> SaveNewReseedToDBAsync(ReseedModel reseed) {
            string mySqlDateFormat = reseed.ReSeedDate.ToString("yyyy-MM-dd HH:mm:ss"); // Formats to MySql datetime format

            try {
                using (MySqlConnection connection = new MySqlConnection(Controller.SETTINGS.ConnectionString)) {
                    connection.Open();
                    string uniqueIndentifier = string.Empty;
                    uniqueIndentifier = await GenerateReseedIdAsync();
                    reseed.UniqueIndentifier = uint.Parse(uniqueIndentifier);
                    string query = $@"INSERT INTO plcrecipes.reseededproduct (EntryNo, ReSeedDate, BagNo, OriginalSite, Site_id, notes)
                                            VALUES(@EntryNo, @ReSeedDate, @BagNo, @OriginalSite, @Site_id, @notes )";

                    using (MySqlCommand command = new MySqlCommand(query, connection)) {

                        command.Parameters.AddWithValue("@EntryNo", uniqueIndentifier);
                        command.Parameters.AddWithValue("@ReSeedDate", mySqlDateFormat);
                        command.Parameters.AddWithValue("@Bagno", reseed.BagNo);
                        command.Parameters.AddWithValue("@OriginalSite", reseed.OriginalSite);
                        command.Parameters.AddWithValue("@Site_id", Controller.SETTINGS.SiteId);
                        command.Parameters.AddWithValue("@notes", reseed.Notes);



                        int result = -2;
                        result = await command.ExecuteNonQueryAsync();
                        if(result <= 0) {
                            return null;
                        }
                    }
                }
                return reseed;
            } 
            catch (MySqlException ex) {
                Console.WriteLine($"MySqlException\nSaveNewReseedToDBAsync() => {ex.Message}\n{ex.StackTrace}\n{ex.Number}");
                throw ex;
            }
            catch (Exception ex) {
                Console.WriteLine($"SaveNewReseedToDBAsync() => {ex.Message}\n{ex.GetType()}");
            }
            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="reseed"></param>
        /// <returns></returns>
        public static async Task<ReseedModel> UpdateReseedAsync(ReseedModel reseed) {
            int rst = -1;

            string mySqlDateFormat = reseed.ReSeedDate.ToString("yyyy-MM-dd HH:mm:ss");
            string query = $@"UPDATE plcrecipes.reseededproduct
                                    SET OriginalSite = @OriginalSite,
                                        BagNo = @BagNo,
                                         reseedDate = @reseedDate, 
                                         notes = @notes
                                    WHERE entryNo = @EntryNo AND OriginalSite = @OriginalSite AND BagNo = @BagNo";
            try {
                using (MySqlConnection conn = new MySqlConnection(Controller.SETTINGS.ConnectionString)) {
                    conn.Open();
                    using (var tran = conn.BeginTransaction()) {
                        using (MySqlCommand command = new MySqlCommand(query, conn, tran)) {
                            command.Parameters.AddWithValue("@EntryNo", reseed.UniqueIndentifier);
                            command.Parameters.AddWithValue("@ReSeedDate", mySqlDateFormat);
                            command.Parameters.AddWithValue("@Bagno", reseed.BagNo);
                            command.Parameters.AddWithValue("@OriginalSite", reseed.OriginalSite);
                            command.Parameters.AddWithValue("@Site_id", Controller.SETTINGS.SiteId);
                            command.Parameters.AddWithValue("@notes", reseed.Notes);

                            try {
                                rst = await command.ExecuteNonQueryAsync();
                                tran.Commit();
                                if (rst > 0) {
                                    return reseed;
                                }
                            } catch (InvalidOperationException ex) {
                                tran.Rollback();
                                Program.ConsoleWriteLineColor($"UpdateReseedAsync() => InvalidOperationException {ex.Message}");
                            } catch (DBConcurrencyException ex) {
                                tran.Rollback();
                                Program.ConsoleWriteLineColor($"UpdateReseedAsync() => DBConcurrencyException {ex.Message}");
                            } catch (MySqlException ex) {
                                tran.Rollback();
                                Program.ConsoleWriteLineColor($"UpdateReseedAsync() => MySqlException {ex.Message}");
                            } catch (Exception ex) {
                                tran.Rollback();
                                Program.ConsoleWriteLineColor($"UpdateReseedAsync() => {ex.Message}\nType: {ex.GetType()}");
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex) {
                if (!ex.Message.ToLower().Contains("syntax")) {
                    FileRepository.SaveModelToFile(reseed, Controller.SETTINGS.PendingUpdateFilePath, Controller.SETTINGS.Delimiter, "INSERT");
                }
                Console.WriteLine($"UpdateReseedAsync() => {ex.Message}");
            }
            catch (Exception ex) {
                Console.WriteLine($"UpdateReseedAsync() => {ex.Message}\n{ex.GetType()}");
            }
            return null;
        }

        /// <summary>
        ///  returns an array of ReseedModel objects, null if no data is retrieved
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="DBConcurrencyException"></exception>
        /// <exception cref="MySqlException"></exception>
        /// <exception cref="Exception"></exception>
        public static async Task<ReseedModel[]> GetReseedFromDBAsync(string query) {
            List<ReseedModel> values = new List<ReseedModel>();
            try {
                using (MySqlConnection conn = new MySqlConnection(Controller.SETTINGS.ConnectionString)) {
                    conn.Open();
                    using (MySqlCommand command = new MySqlCommand(query, conn)) {
                        var reader = await command.ExecuteReaderAsync();
                        if (reader.HasRows) {
                            while (reader.Read()) {
                                ReseedModel row = new ReseedModel();

                                row.UniqueIndentifier = !reader.IsDBNull(0) ? uint.Parse(reader.GetString(0)): 0; //EntryNo
                                row.ReSeedDate = DateTime.Parse(reader.GetString(1)); //ReSeedDate
                                row.BagNo =  !reader.IsDBNull(2) ? (uint)reader.GetInt32(2) : 0; // BagNo
                                row.OriginalSite = !reader.IsDBNull(3) ? (uint)reader.GetInt32(3) : 0; //OriginalSite
                                row.Notes = !reader.IsDBNull(4) ? reader.GetString(4) : string.Empty; //notes
                                values.Add(row);
                            }
                        }
                        reader.Dispose();
                    }
                    return values.ToArray();
                }
            } catch (MySqlException ex) {
                Console.WriteLine("GetReseedFromDBAsync() => MySqlException {0}", ex.Message);
                throw ex;
            }
            catch (InvalidOperationException ex) {
                Console.WriteLine("GetReseedFromDBAsync() => InvalidOperationException {0}", ex.Message);
            }
            catch (DBConcurrencyException ex) {
                Console.WriteLine("GetReseedFromDBAsync() => DBConcurrencyException {0}", ex.Message);
            }

            catch (AggregateException ex) {
                Console.WriteLine("GetReseedFromDBAsync() => AggregateException {0}", ex.Message);
            }
            return null;
        }
    }
}
