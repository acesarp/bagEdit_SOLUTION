using Bagging.Controller.Controllers;
using Bagging.Controller.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bagging.Controller.Repositories {
    public static class FileRepository {
        private static ILog log = LogManager.GetLogger(typeof(FileRepository));

        /// <summary>
        /// Returns an Dictionary<BagModel, string>  of bags
        /// </summary>
        /// <returns></returns>
        /// <exception cref="IOException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="FileFormatException"></exception>
        internal static Dictionary<Model, string> ReadModelsFromFile(string filePath, string delimiter, Type bagType) {
            log.Info("Reading models from file...");
            if (!File.Exists(filePath)) {
                return null;
            }

            Dictionary<Model, string> modelList = new Dictionary<Model, string>();
            try {
                string[] linesOfBags = File.ReadAllLines(filePath);

                foreach (string line in linesOfBags) {
                    if (0 < line.Length) {
                        string[] modelArray = line.Split(delimiter.Split(), StringSplitOptions.None);
                        Model model_ = null;

                        if ((bagType == typeof(ReseedModel) || bagType == typeof(Model)) && 6 == modelArray.Length - 1) { // -1 to account for the queryMode filed
                            model_ = new ReseedModel();

                            ReseedModel model = model_ as ReseedModel;
                            model.UniqueIndentifier = Convert.ToUInt32(modelArray[1]);
                            model.ReSeedDate = Convert.ToDateTime(modelArray[2]);
                            model.BagNo = Convert.ToUInt32(modelArray[3]);
                            model.OriginalSite = Convert.ToUInt16(modelArray[4]);
                            model.SiteId = Convert.ToUInt16(modelArray[5]);
                            model.Notes = modelArray[6];
                            modelList.Add(model_, modelArray[0]);
                        } 
                        else if ((bagType == typeof(BagModel) || bagType == typeof(Model)) && 8 == modelArray.Length - 1) { // -1 to account for the queryMode filed

                            model_ = new BagModel();
                            BagModel model = model_ as BagModel;
                            model.UniqueIndentifier = Convert.ToUInt32(modelArray[1]);
                            model.SiteId = Convert.ToUInt16(modelArray[2]);
                            model.ProductType = modelArray[3];
                            model.ProductDate = Convert.ToDateTime(modelArray[4]);
                            model.ProductWeight = Convert.ToDecimal(modelArray[5]);
                            model.Notes = modelArray[6];
                            model.QualityFlag = Convert.ToInt16(modelArray[7]);
                            model.QualityFlagName = modelArray[8];
                            modelList.Add(model_, modelArray[0]);
                        } 
                    }
                }
            }
            catch (Exception ex) {
                log.Error("Error:", ex);
                Console.WriteLine($"Exception => {ex.Message}\n{ex.StackTrace}\n{ex.Data}\n{ex.GetType()}");
                throw ex;
            }
            log.Info("Models read from file successfully!");
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
        //        Console.WriteLine(ex.Message, ConsoleColor.Red);
        //        throw ex;
        //    } catch (UnauthorizedAccessException ex) {
        //        Console.WriteLine(ex.Message, ConsoleColor.Red);
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
            log.Info("Saving model to file...");
            StringBuilder builder = new StringBuilder();
            builder.Append(queryMode);
            builder.Append(delimiter);
            Console.WriteLine(model.GetType());

            if(model.GetType() == typeof(BagModel)) {
                BagModel model_ = model as BagModel;
                builder.Append(model_.UniqueIndentifier);
                builder.Append(delimiter);
                builder.Append(model_.SiteId);
                builder.Append(delimiter);
                builder.Append(model_.ProductType);
                builder.Append(delimiter);
                builder.Append(model_.ProductDate);
                builder.Append(delimiter);
                builder.Append(model_.ProductWeight);
                builder.Append(delimiter);
                builder.Append(model_.Notes);
                builder.Append(delimiter);
                builder.Append(model_.QualityFlag);
                builder.Append(delimiter);
                builder.Append(model_.QualityFlagName);
            }
            else if (model.GetType() == typeof(ReseedModel)) {
                ReseedModel model_ = model as ReseedModel;
                builder.Append(model_.UniqueIndentifier);
                builder.Append(delimiter);
                builder.Append(model_.ReSeedDate);
                builder.Append(delimiter);
                builder.Append(model_.BagNo);
                builder.Append(delimiter);
                builder.Append(model_.OriginalSite);
                builder.Append(delimiter);
                builder.Append(model_.SiteId);
                builder.Append(delimiter);
                builder.Append(model_.Notes);
            }

            try {
                using (StreamWriter stream = new StreamWriter(filePath, true)) {
                    stream.WriteLine(builder.ToString());

                    log.Info("Model Saved to file successfully!");
                    return model;
                }
            } 
            catch (IOException ex) {
                log.Error(" Exception thrown!", ex);
                throw ex;
            } 
            catch (UnauthorizedAccessException ex) {
                log.Error(" Exception thrown!", ex);
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
            log.Info("Updating model in file...");
            string[] linesOfBags = File.ReadAllLines(filePath);
            Type oldModelType = oldModel.GetType();

            for (int i = 0; i < linesOfBags.Length; i++) {
                StringBuilder builder = new StringBuilder();
                if (0 < linesOfBags[i].Length) {
                    string[] array = linesOfBags[i].Split(delimiter.Split(), StringSplitOptions.None);
                    int numOfFields = oldModel.GetType().GetProperties().Length;
                    string queryMode = array[0];
                    Model modelFromFile = null;
                    if ((array.Length - 1) == numOfFields) { // -1 to account for the queryMode filed
                        if (typeof(BagModel) == oldModel.GetType()) {
                            modelFromFile = new BagModel {
                                UniqueIndentifier = Convert.ToUInt32(array[1]),
                                SiteId = Convert.ToUInt16(array[2]),
                                ProductType = array[3],
                                ProductDate = Convert.ToDateTime(array[4]),
                                ProductWeight = Convert.ToDecimal(array[5]),
                                Notes = array[6],
                                QualityFlag = Convert.ToInt16(array[7]),
                                QualityFlagName = array[8]
                            };

                            BagModel newBag = newModel as BagModel;
                            builder.Append(queryMode);
                            builder.Append(delimiter);
                            builder.Append(newBag.UniqueIndentifier);
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

                        } 
                        else if (typeof(ReseedModel) == oldModel.GetType()) {
                            modelFromFile = new ReseedModel {
                                UniqueIndentifier = Convert.ToUInt32(array[1]),
                                ReSeedDate = Convert.ToDateTime(array[2]),
                                BagNo = Convert.ToUInt16(array[3]),
                                OriginalSite = Convert.ToUInt16(array[4]),
                                SiteId = Convert.ToUInt16(array[5]),
                                Notes = array[6]
                            };
                        }
                        if (oldModel.ToString().Equals(modelFromFile.ToString())) {
                            linesOfBags[i] = builder.ToString();

                            using (StreamWriter stream = new StreamWriter(filePath, false)) {
                                string bagLines = string.Join("\n", linesOfBags);
                                stream.WriteLine(bagLines);
                                log.Info("Model Updated in file successfully!");
                                return newModel;
                            }
                        }
                    }
                }
            }
            log.Info("No model found...");
            return null;
        }

        internal static void DeleteFile(string pendingUpdateFilePath) {
            log.Info("Deleting file...");
            try {
                File.Delete(pendingUpdateFilePath);
            }catch(Exception ex){
                log.Error("File not deleted...", ex);
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
                        builder.Append(model.ReSeedDate);
                        builder.Append(delimiter);
                        builder.Append(model.BagNo);
                        builder.Append(delimiter);
                        builder.Append(model.OriginalSite);
                        builder.Append(delimiter);
                        builder.Append(model.SiteId);
                        builder.Append(delimiter);
                        builder.Append(model.Notes);
                    }
                    builder.Append("\n");
                }
                var info = Directory.CreateDirectory($@"{Directory.GetCurrentDirectory()}\logs" );

                //Console.WriteLine(info.FullName);

                string fullLogFilePath = $@"{info.FullName}\{DateTime.Now:yyyy-MM-dd_HH-mm-ss- fff}{logFileName}";
                using (StreamWriter stream = new StreamWriter(fullLogFilePath, true)) {
                    stream.WriteLine(builder.ToString());
                    log.Info("Model logged to file successfully!");
                    return true;
                }
            } catch (IOException ex) {
                log.Error("Exception thrown!", ex);
            } catch (UnauthorizedAccessException ex) {
                log.Error("Exception thrown!", ex);
            }
            log.Error("Model wasn't logged to file, fail!");
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelList"></param>
        /// <param name="filePath"></param>
        /// <param name="delimiter"></param>
        /// <param name="queryMode"></param>
        /// <exception cref="IOException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        internal static void SaveRemainingModelsToFile(Dictionary<Model, string> modelList, string filePath, string delimiter) {
            log.Info("Save Remaining Models ToF ile...");
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
                    builder.Append(model.UniqueIndentifier);
                    builder.Append(delimiter);
                    builder.Append(model.ReSeedDate);
                    builder.Append(delimiter);
                    builder.Append(model.BagNo);
                    builder.Append(delimiter);
                    builder.Append(model.OriginalSite);
                    builder.Append(delimiter);
                    builder.Append(model.SiteId);
                    builder.Append(delimiter);
                    builder.Append(model.Notes);
                } else {
                    log.Error("Invalid model");
                    throw new FormatException("Invalid model.");
                }

                builder.Append("\n");
            }
            try {
                using (StreamWriter stream = new StreamWriter(filePath, true)) {
                    stream.WriteLine(builder.ToString());
                }
            } catch (IOException ex) {
                log.Error(" Exception thrown!", ex);
                throw ex;
            } catch (UnauthorizedAccessException ex) {
                log.Error(" Exception thrown!", ex);
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
                log.Error(" Exception thrown!", ex);
                return dictionary;
            }
        }

        public static void SaveQualityValuesToFile(string qualityFlagValuesFilePath_, string[] values) {
            log.Info(" Saving quality values to file...");
            try {
                File.Delete(qualityFlagValuesFilePath_);
                File.WriteAllLines(qualityFlagValuesFilePath_, values);
            } catch (IOException ex) {
                log.Error(" Exception thrown!", ex);
            } catch (Exception ex) {
                log.Error(" Exception thrown!", ex);
            }
        }

    }
}
