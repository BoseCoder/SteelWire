using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace SteelWire.Business.Database
{
    public class SqlLiteUpgrade : DatabaseUpgrade
    {
        protected override Dictionary<int, List<string>> UpgradeSql { get; }

        public SqlLiteUpgrade()
        {
            UpgradeSql = new Dictionary<int, List<string>>
            {
                {
                    1,
                    new List<string>
                    {
                        @"ALTER TABLE WireropeCutRole ADD COLUMN AllowMinDerrickHeight BOOLEAN NOT NULL DEFAULT '0';",
                        @"ALTER TABLE WireropeCutRole ADD COLUMN AllowMaxDerrickHeight BOOLEAN NOT NULL DEFAULT '0';",
                        @"UPDATE SystemConfig SET [Value] = @Value WHERE [Key] = @Key;"
                    }
                }
            };
        }

        protected override int GetDatabaseVersion()
        {
            using (SteelWireSqlLiteContext dbContext = new SteelWireSqlLiteContext())
            {
                try
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    DbCommand dbCommand = dbContext.Database.Connection.CreateCommand();
                    dbCommand.CommandText = @"CREATE TABLE IF NOT EXISTS SystemConfig (Key TEXT(50) not null, Value TEXT(200) not null);";
                    dbCommand.ExecuteNonQuery();
                    dbCommand.CommandText = @"INSERT INTO SystemConfig (Key,Value) SELECT @Key as Key, @Value as Value WHERE NOT EXISTS (SELECT * FROM SystemConfig WHERE Key=@Key);
SELECT Value FROM SystemConfig WHERE Key=@Key;";
                    dbCommand.Parameters.Add(new SQLiteParameter("@Key", DatabaseVersionKey));
                    dbCommand.Parameters.Add(new SQLiteParameter("@Value", DatabaseDefaultVersion));
                    object result = dbCommand.ExecuteScalar();
                    int version;
                    int.TryParse($"{result}", out version);
                    return version;
                }
                finally
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Open)
                    {
                        dbContext.Database.Connection.Close();
                    }
                }
            }
        }

        protected override int UpgradeDatabase(int version, string sql)
        {
            using (SteelWireSqlLiteContext dbContext = new SteelWireSqlLiteContext())
            {
                try
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    return dbContext.Database.ExecuteSqlCommand(sql,
                        new SQLiteParameter("@Key", DatabaseVersionKey),
                        new SQLiteParameter("@Value", $"{version}"));
                }
                finally
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Open)
                    {
                        dbContext.Database.Connection.Close();
                    }
                }
            }
        }
    }
}
