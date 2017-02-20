using SteelWire.Business.Config;

namespace SteelWire.Business.Database
{
    public class DbContextFactory
    {
        static DbContextFactory()
        {
            DatabaseUpgrade upgrade;
            switch (DatabaseConfigManager.OnceInstance.DatabaseType)
            {
                case DatabaseType.SqlServer:
                    upgrade = new SqlServerUpgrade();
                    break;
                case DatabaseType.SqlLite:
                    upgrade = new SqlLiteUpgrade();
                    break;
                default:
                    return;
            }
            upgrade.UpgradeDatabase();
        }

        public static SteelWireBaseContext GenerateDbContext()
        {
            switch (DatabaseConfigManager.OnceInstance.DatabaseType)
            {
                case DatabaseType.SqlServer:
                    return new SteelWireSqlServerContext();
                case DatabaseType.SqlLite:
                    return new SteelWireSqlLiteContext();
                default:
                    return null;
            }
        }
    }
}
