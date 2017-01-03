using BaseConfig;
using SteelWire.Business.Database;

namespace SteelWire.Business.Config
{
    public class DatabaseConfigManager : BaseConfigManager
    {
        public static DatabaseConfigManager OnceInstance { get; private set; }

        [BaseConfigKey]
        public DatabaseType DatabaseType { get; set; }

        [BaseConfigKey]
        public bool DataIsolation { get; set; }

        private DatabaseConfigManager(string key, string subFolder, string fileName)
            : base(key, subFolder, fileName)
        { }

        static DatabaseConfigManager()
        {
            InitializeConfig();
        }

        public static void InitializeConfig()
        {
            OnceInstance = new DatabaseConfigManager("DatabaseConfig", "Config", "DatabaseConfig.config");
            OnceInstance.ReadConfig();

            if (!OnceInstance.AppConfig.HasFile)
            {
                OnceInstance.SaveConfig();
            }
        }
    }
}
