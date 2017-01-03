using SteelWire.Business.Config;

namespace SteelWire.Business.Database
{
    public class DbContextFactory
    {
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
