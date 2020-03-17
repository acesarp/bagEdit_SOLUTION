using log4net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System;

namespace Bagging.Controller {
    internal class AppSettings {
        private static ILog log = LogManager.GetLogger(typeof(AppSettings));
        internal ushort DaysInThePast { get; private set; } = 30; /// preset value: 30
        internal string PrinterIp { get; private set; }
        internal int PrinterPort { get; private set; }
        internal ushort SiteId { get; private set; }
        internal string PendingUpdateFilePath { get; private set; }
        public string[] QueryMode { get; private set; }
        internal string Delimiter { get; private set; }
        internal string LogoImagePath { get; private set; }
        internal string QualityValuesFilePath { get; private set; }
        internal string ConnectionString { get; private set; }
        internal string SelectCommand { get; private set; }
        internal string SelectReseedCommand { get; private set; }
        internal string Server { get; private set; }
        internal uint Port { get; private set; }
        internal string Database { get; private set; }
        internal string Uid { get; private set; }
        internal string Password { get; private set; }
        internal decimal MinWeight { get; private set; }
        internal decimal MaxWeight { get; private set; }
        internal string SavedBagsLogFile { get; private set; }
        internal string[] ProdyctTypeList { get; private set; }
        internal string ProductPrefix { get; private set; }
        internal bool ActivateConsole { get; private set; }

        private readonly IConfigurationRoot Configuration;

        internal AppSettings(string jsonFile) {
            log.Info(("Loading config settings..."));
            Configuration = new ConfigurationBuilder()
            .AddJsonFile(jsonFile, false, true)
            .Build();

            IFileInfo settingsFileInfo = new ConfigurationBuilder().GetFileProvider().GetFileInfo(jsonFile);

            try {
                PrinterIp = Configuration["config:PrinterIp"];
                PrinterPort = Convert.ToInt32(Configuration["config:PrinterPort"]);
                SiteId = Convert.ToUInt16(Configuration["config:SiteId"]);
                PendingUpdateFilePath = Configuration["config:BagsPendingUpdateFilePath"];
                QueryMode = Configuration["config:queryMode"].Split();
                Delimiter = Configuration["config:Delimiter"];
                LogoImagePath = Configuration["config:LogoImagePath"];
                SavedBagsLogFile = Configuration["config:SavedBagslogFile"];
                Database = Configuration["config:Database"];
                Server = Configuration["config:Server"];
                Port = Convert.ToUInt32(Configuration["config:Port"]);
                Uid = Configuration["config:uid"];
                Password = Configuration["config:Password"];

                //Bag edit
                QualityValuesFilePath = Configuration["bagedit:QualityValuesFilePath"];
                ProdyctTypeList = Configuration["bagedit:ProductTypes"].Split(',');
                //ConnectionString = Configuration["config:ConnectionString"];
                ConnectionString = $"SERVER={Server};PORT={Port};DATABASE={Database};UID={Uid};PASSWORD={Password};";
                SelectCommand = Configuration["bagedit:SelectCommand"];
                DaysInThePast = ushort.Parse(Configuration["bagedit:DaysInThePast"]);
                ProductPrefix = Configuration["bagedit:ProductPrefix"];

                MinWeight = Convert.ToDecimal(Configuration["bagedit:MinWeight"]);
                MaxWeight = Convert.ToDecimal(Configuration["bagedit:MaxWeight"]);

                //Bag reseed
                SelectReseedCommand = Configuration["reseed:SelectCommand"];

                //Debug
                bool result = false;
                bool.TryParse(Configuration["debug:ActivateConsole"], out result);
                ActivateConsole = result;
            } 
            catch(FormatException ex) {
                log.Fatal("Error", ex);
                throw ex;
            }
            //Console.WriteLine($"WrikeApiUrl: {WrikeApiUrl}\ntoken: {WrikeToken}\nwrikeSheetName: {WrikeSheetName}\nsharePointBaseUrl: {SharePointBaseUrl}\nxlsfileList: {xlsfileList}\nimgFormat: {ImgFormat}\n\n");
            //Console.WriteLine($"PBI_Tenant: {PBI_Tenant}\nPBI_ClientId: {PBI_ClientId}\nPBI_ClientSecret: {PBI_ClientSecret}\nPBI_username: {PBI_username}\nPBI_password: {PBI_password}\nPBI_info: {PBI_powerbi_data_file}\n\n");
            log.Info(("config settings Loaded successfully!"));
        }
    }
}
