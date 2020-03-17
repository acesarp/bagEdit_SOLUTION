using Bagging.BagEdit.Controllers;
using Bagging.BagEdit.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bagging.BagEdit.Repositories {
    public static class FileRepository {

        /// <summary>
        /// Returns an Dictionary<BagModel, string>  of bags
        /// </summary>
        /// <returns></returns>
        /// <exception cref="IOException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="FileFormatException"></exception>
        internal static Dictionary<Model, string> ReadModelsFromFile(string filePath, string delimiter, Type bagType) {
            if (!File.Exists(filePath)) {
                return null;
            }
            //Type typeOfCaller = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().DeclaringType;

            Dictionary<Model, string> modelList = new Dictionary<Model, string>();
            //var type = typeof(Dictionary<,>).MakeGenericType(typeOfCaller, typeof(string));
            //var modelList = Activator.CreateInstance(type);
            try {
                string[] linesOfBags = File.ReadAllLines(filePath);

                foreach (string line in linesOfBags) {
                    if (0 < line.Length) {
                        string[] modelArray = line.Split(delimiter.Split(), StringSplitOptions.None);
                        Model model_ = null;

                        if ((bagType == typeof(ReseedController) || bagType == typeof(Model)) && (6 == modelArray.Length)) {
                                model_ = new ReseedModel();
                                ReseedModel model = model_ as ReseedModel;
                                model.SiteId = Convert.ToUInt16(modelArray[1]);
                                model.ReSeedDate = Convert.ToDateTime(modelArray[3]);
                                model.BagNo = Convert.ToUInt32(modelArray[4]);
                                model.OriginalSite = Convert.ToUInt16(modelArray[2]);
                                model.Notes = modelArray[5];
                                modelList.Add(model_, modelArray[0]);
                                continue;
                            
                        } 
                        if ((bagType == typeof(EditController) || bagType == typeof(Model)) && (8 == modelArray.Length)) {

                                model_ = new BagModel();
                                BagModel model = model_ as BagModel;
                                model.SiteId = Convert.ToUInt16(modelArray[1]);
                                model.ProductType = Convert.ToUInt16(modelArray[2]);
                                model.ProductDate = Convert.ToDateTime(modelArray[3]);
                                model.ProductWeight = Convert.ToDecimal(modelArray[4]);
                                model.Notes = modelArray[5];
                                model.QualityFlag = Convert.ToInt16(modelArray[6]);
                                model.QualityFlagName = modelArray[7];
                                modelList.Add(model_, modelArray[0]);
                            
                        } 
                    }
                }
            }
            catch (Exception ex) {
                Program.ConsoleWriteLineColor($"Exception => {ex.Message}\n{ex.StackTrace}\n{ex.Data}\n{ex.GetType()}", ConsoleColor.Red);
                throw ex;
            }
            return modelList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bag"></param>
        /// <param name="filePath"></param>
        /// <param name="delimiter"></param>
        /// <param name="queryMode"></param>
        /// <exception cref="IOException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        //internal static BagModel SaveBagToFile(BagModel bag, string filePath, string delimiter, string queryMode) {
        //    StringBuilder builder = new StringBuilder();
        //    builder.Append(queryMode);
        //    builder.Append(delimiter);
        //    builder.Append(bag.SiteId);
        //    builder.Append(delimiter);
        //    builder.Append(bag.ProductType);
        //    builder.Append(delimiter);
        //    builder.Append(bag.ProductDate);
        //    builder.Append(delimiter);
        //    builder.Append(bag.ProductWeight);
        //    builder.Append(delimiter);
        //    builder.Append(bag.Notes);
        //    builder.Append(delimiter);
        //    builder.Append(bag.QualityFlag);
        //    builder.Append(delimiter);
        //    builder.Append(bag.QualityFlagName);

        //    try {
        //        using (StreamWriter stream = new System.IO.StreamWriter(filePath, true)) {
        //            stream.WriteLine(builder.ToString());
        //            return bag;
        //        }
        //    } catch (IOException ex) {
        //        Program.ConsoleWriteLineColor(ex.Message, ConsoleColor.Red);
        //        throw ex;
        //    } catch (UnauthorizedAccessException ex) {
        //        Program.ConsoleWriteLineColor(ex.Message, ConsoleColor.Red);
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filePath"></param>
        /// <param name="delimiter"></param>
        /// <param name="queryMode"></param>
        /// <exception cref="IOException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        internal static Model SaveModelToFile(Model model, string filePath, string delimiter, string queryMode) {
            StringBuilder builder = new StringBuilder();
            builder.Append(queryMode);
            builder.Append(delimiter);

            IList<PropertyInfo> props = new List<PropertyInfo>(model.GetType().GetProperties());

            foreach (PropertyInfo prop in props) {
                builder.Append(prop.GetValue(model));
                builder.Append(delimiter);
            }

            try {
                using (StreamWriter stream = new StreamWriter(filePath, true)) {
                    stream.WriteLine(builder.ToString());
                    return model;
                }
            } catch (IOException ex) {
                Program.ConsoleWriteLineColor(ex.Message, ConsoleColor.Red);
                throw ex;
            } catch (UnauthorizedAccessException ex) {
                Program.ConsoleWriteLineColor(ex.Message, ConsoleColor.Red);
                throw ex;
            }
        }

        /// <summary>
        /// Updates bag in file, if no bag is found null is returned
        /// </summary>
        /// <param name="oldModel"></param>
        /// <param name="newBag"></param>
        /// <param name="filePath"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        internal static Model UpdateModelInFile(Model oldModel, Model newModel, string filePath, string delimiter) {
            string[] linesOfBags = File.ReadAllLines(filePath);

            for (int i = 0; i < linesOfBags.Length; i++) {
                StringBuilder builder = new StringBuilder();
                if (0 < linesOfBags[i].Length) {
                    string[] array = linesOfBags[i].Split(delimiter.Split(), StringSplitOptions.None);
                    int numOfFields = oldModel.GetType().GetProperties().Length;
                    string queryMode = array[0];
                    Model modelFromFile = null;
                    if (array.Length == numOfFields) {
                        if (typeof(BagModel) == oldModel.GetType()) {
                            modelFromFile = new BagModel {
                                SiteId = Convert.ToUInt16(array[1]),
                                ProductType = Convert.ToUInt16(array[2]),
                                ProductDate = Convert.ToDateTime(array[3]),
                                ProductWeight = Convert.ToDecimal(array[4]),
                                Notes = array[5],
                                QualityFlag = Convert.ToInt16(array[6]),
                                QualityFlagName = array[7]
                            };

                            BagModel newBag = newModel as BagModel;
                            builder.Append(queryMode);
                            builder.Append(delimiter);
                            builder.Append(newBag.SiteId);
                            builder.Append(delimiter);
                            builder.Append(newBag.ProductType);
                            builder.Append(delimiter);
                            builder.Append(newBag.ProductDate);
                            builder.Append(delimiter);
                            builder.Append(newBag.ProductWeight);
                            builder.Append(delimiter);
                            builder.Append(newBag.Notes);
                            builder.Append(delimiter);
                            builder.Append(newBag.QualityFlag);
                            builder.Append(delimiter);
                            builder.Append(newBag.QualityFlagName);
                        } else if (typeof(ReseedModel) == oldModel.GetType()) {
                            modelFromFile = new ReseedModel {
                                UniqueIndentifier = Convert.ToUInt32(array[1]),
                                SiteId = Convert.ToUInt16(array[1]),
                                BagNo = Convert.ToUInt16(array[2]),
                                OriginalSite = Convert.ToUInt16(array[2]),
                                ReSeedDate = Convert.ToDateTime(array[3]),
                                Notes = array[5]
                            };
                        }
                        if (oldModel.ToString().Equals(modelFromFile.ToString())) {
                            linesOfBags[i] = builder.ToString();

                            using (StreamWriter stream = new System.IO.StreamWriter(filePath, false)) {
                                string bagLines = string.Join("\n", linesOfBags);
                                stream.WriteLine(bagLines);
                                return newModel;
                            }
                        }
                    }
                }
            }
            return null;
        }

        internal static void DeleteFile(string pendingUpdateFilePath) {
            try {
                File.Delete(pendingUpdateFilePath);
            }catch(Exception ex){
                Console.WriteLine($"DeleteFile() => {ex.Message}\n{ex.GetType()}");
            }
        }

        internal static bool LogSavedModelsToFile(List<Model> modelList, string logFileName, string delimiter) {
            StringBuilder builder = new StringBuilder();
            try {
                foreach (var model_ in modelList) {
                    if (typeof(BagModel) == model_.GetType()) {
                        BagModel model = model_ as BagModel;
                        builder.Append(model.UniqueIndentifier);
                        builder.Append(delimiter);
                        builder.Append(model.SiteId);
                        builder.Append(delimiter);
                        builder.Append(model.ProductType);
                        builder.Append(delimiter);
                        builder.Append(model.ProductDate);
                        builder.Append(delimiter);
                        builder.Append(model.ProductWeight);
                        builder.Append(delimiter);
                        builder.Append(model.Notes);
                        builder.Append(delimiter);
                        builder.Append(model.QualityFlag);
                        builder.Append(delimiter);
                        builder.Append(model.QualityFlagName);
                    }
                    else if (typeof(ReseedModel) == model_.GetType()) {
                        ReseedModel model = model_ as ReseedModel;
                        builder.Append(model.UniqueIndentifier);
                        builder.Append(delimiter);
                        builder.Append(model.SiteId);
                        builder.Append(delimiter);
                        builder.Append(model.BagNo);
                        builder.Append(delimiter);
                        builder.Append(model.ReSeedDate);
                        builder.Append(delimiter);
                        builder.Append(model.Notes);
                    }
                    builder.Append("\n");

                }
                var info = Directory.CreateDirectory($@"{Directory.GetCurrentDirectory()}\logs" );

                Console.WriteLine(info.FullName);

                string fullLogFilePath = $@"{info.FullName}\{DateTime.Now:yyyy-MM-dd_HH-mm-ss- fff}{logFileName}";
                using (StreamWriter stream = new System.IO.StreamWriter(fullLogFilePath, true)) {
                    stream.WriteLine(builder.ToString());
                    return true;
                }
            } catch (IOException ex) {
                Program.ConsoleWriteLineColor(ex.Message, ConsoleColor.Red);
            } catch (UnauthorizedAccessException ex) {
                Program.ConsoleWriteLineColor(ex.Message, ConsoleColor.Red);
            }
            return false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bag"></param>
        /// <param name="filePath"></param>
        /// <param name="delimiter"></param>
        /// <param name="queryMode"></param>
        /// <exception cref="IOException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        internal static void SaveRemainingModelsToFile(Dictionary<Model, string> modelList, string filePath, string delimiter) {
            StringBuilder builder = new StringBuilder();

            foreach (var modelKeyValuePair in modelList) {

                if (typeof(BagModel) == modelKeyValuePair.Key.GetType()) {
                    BagModel model = modelKeyValuePair.Key as BagModel;
                    builder.Append(modelKeyValuePair.Value);
                    builder.Append(delimiter);
                    builder.Append(model.SiteId);
                    builder.Append(delimiter);
                    builder.Append(model.ProductType);
                    builder.Append(delimiter);
                    builder.Append(model.ProductDate);
                    builder.Append(delimiter);
                    builder.Append(model.ProductWeight);
                    builder.Append(delimiter);
                    builder.Append(model.Notes);
                    builder.Append(delimiter);
                    builder.Append(model.QualityFlag);
                    builder.Append(delimiter);
                    builder.Append(model.QualityFlagName);
                }
                else if (typeof(ReseedModel) == modelKeyValuePair.Key.GetType()) {
                    ReseedModel model = modelKeyValuePair.Key as ReseedModel;
                    builder.Append(modelKeyValuePair.Value);
                    builder.Append(delimiter);
                    builder.Append(model.SiteId);
                    builder.Append(delimiter);
                    builder.Append(model.BagNo);
                    builder.Append(delimiter);
                    builder.Append(model.ReSeedDate);
                    builder.Append(delimiter);
                    builder.Append(model.Notes);
                } else {
                    throw new FormatException("Invalid model.");
                }

                builder.Append("\n");
            }
            try {
                using (StreamWriter stream = new System.IO.StreamWriter(filePath, true)) {
                    stream.WriteLine(builder.ToString());
                }
            } catch (IOException ex) {
#if DEBUG
                Program.ConsoleWriteLineColor(ex.Message, ConsoleColor.Red);
#endif
                throw ex;
            } catch (UnauthorizedAccessException ex) {
#if DEBUG
                Program.ConsoleWriteLineColor(ex.Message, ConsoleColor.Red);
#endif
                throw ex;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityFlagValuesFilePath"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static Dictionary<int, string> ReadQualityValuesFromFile(string qualityFlagValuesFilePath, string delimiter) {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            try
            {
                string[] lines = File.ReadAllLines(qualityFlagValuesFilePath);
                foreach (string line in lines)
                {
                    string[] values = line.Split(delimiter.Split(), StringSplitOptions.None);
                    dictionary.Add(Convert.ToInt16(values[0]), values[1]);
                }
                return dictionary;
            }
            catch (Exception ex) {
#if DEBUG
                Program.ConsoleWriteLineColor($"Exception => {ex.Message}\n{ex.StackTrace}\n{ex.Data}\n{ex.GetType()}", ConsoleColor.Red);
#endif
                return dictionary;
            }
        }

        public static void SaveQualityValuesToFile(string qualityFlagValuesFilePath_, string[] values) {

            try {
                File.Delete(qualityFlagValuesFilePath_);
                File.WriteAllLines(qualityFlagValuesFilePath_, values);
            } catch (IOException ex) {
                Program.ConsoleWriteLineColor($"Exception => {ex.Message}\n{ex.StackTrace}\n{ex.Data}\n{ex.GetType()}", ConsoleColor.Red);
            } catch (Exception ex) {
                Program.ConsoleWriteLineColor($"Exception => {ex.Message}\n{ex.StackTrace}\n{ex.Data}\n{ex.GetType()}", ConsoleColor.Red);
            }
        }

    }
}
