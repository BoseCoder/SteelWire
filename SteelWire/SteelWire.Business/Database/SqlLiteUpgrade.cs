using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;

namespace SteelWire.Business.Database
{
    public class SqlLiteUpgrade : DatabaseUpgrade
    {
        protected override Dictionary<int, string> UpgradeSql { get; }

        public SqlLiteUpgrade()
        {
            UpgradeSql = new Dictionary<int, string>
            {
                {1, @"ALTER TABLE WireropeCutRole ADD COLUMN AllowMinDerrickHeight BOOLEAN NOT NULL DEFAULT '0';
ALTER TABLE WireropeCutRole ADD COLUMN AllowMaxDerrickHeight BOOLEAN NOT NULL DEFAULT '0';"}
            };
        }

        protected override int GetDatabaseVersion()
        {
            using (SteelWireSqlLiteContext dbContext = new SteelWireSqlLiteContext())
            {
                DbCommand dbCommand = dbContext.Database.Connection.CreateCommand();
                dbCommand.CommandText = @"CREATE TABLE IF NOT EXISTS SystemConfig (Key TEXT(50) not null, Value TEXT(200) not null);
INSERT INTO SystemConfig (Key,Value) SELECT @Key as Key, @Value as Value WHERE NOT EXISTS (SELECT * FROM SystemConfig WHERE Key=@Key);
SELECT Value FROM SystemConfig WHERE Key=@Key;";
                dbCommand.Parameters.Add(new OleDbParameter("@Key", DatabaseVersionKey));
                dbCommand.Parameters.Add(new OleDbParameter("@Value", DatabaseDefaultVersion));
                object result = dbCommand.ExecuteScalar();
                int version;
                int.TryParse($"{result}", out version);
                return version;
            }
        }

        protected override int UpgradeDatabase(int version, string sql)
        {
            using (SteelWireSqlLiteContext dbContext = new SteelWireSqlLiteContext())
            {
                return dbContext.Database.ExecuteSqlCommand(sql,
                    new OleDbParameter("@Key", DatabaseVersionKey),
                    new OleDbParameter("@Value", $"{version}"));
            }
        }
    }
}
