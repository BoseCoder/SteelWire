using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SteelWire.Business.Database
{
    public class SqlServerUpgrade : DatabaseUpgrade
    {
        protected override Dictionary<int, List<string>> UpgradeSql { get; }

        public SqlServerUpgrade()
        {
            UpgradeSql = new Dictionary<int, List<string>>
            {
                {
                    1,
                    new List<string>
                    {
                        @"if not exists(select 1 from syscolumns where id=object_id('WireropeCutRole') and name='AllowMinDerrickHeight') 
	alter table WireropeCutRole add AllowMinDerrickHeight bit;",
                        @"update WireropeCutRole set AllowMinDerrickHeight = '0' where AllowMinDerrickHeight is null;",
                        @"alter table WireropeCutRole alter column AllowMinDerrickHeight bit not null;",
                        @"if not exists(select 1 from syscolumns where id=object_id('WireropeCutRole') and name='AllowMaxDerrickHeight') 
	alter table WireropeCutRole add AllowMaxDerrickHeight bit;",
                        @"update WireropeCutRole set AllowMaxDerrickHeight = '0' where AllowMaxDerrickHeight is null;",
                        @"alter table WireropeCutRole alter column AllowMaxDerrickHeight bit not null;",
                        @"update SystemConfig set [Value] = @Value where [Key] = @Key;"
                    }
                }
            };
        }

        protected override int GetDatabaseVersion()
        {
            using (SteelWireSqlServerContext dbContext = new SteelWireSqlServerContext())
            {
                try
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    DbCommand dbCommand = dbContext.Database.Connection.CreateCommand();
                    dbCommand.CommandText = @"if not exists (select 1
            from  sysobjects
           where  id = object_id('SystemConfig')
            and   type = 'U')
    create table SystemConfig ([Key] varchar(50) not null, [Value] varchar(200) not null);";
                    dbCommand.ExecuteNonQuery();
                    dbCommand.CommandText = @"if not exists (select 1 from SystemConfig where [Key]=@Key)
	insert into SystemConfig ([Key],[Value]) values (@Key,@Value);
select [Value] from SystemConfig where [Key]=@Key;";
                    dbCommand.Parameters.Add(new SqlParameter("@Key", DatabaseVersionKey));
                    dbCommand.Parameters.Add(new SqlParameter("@Value", DatabaseDefaultVersion));
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
            using (SteelWireSqlServerContext dbContext = new SteelWireSqlServerContext())
            {
                try
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    return dbContext.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@Key", DatabaseVersionKey),
                        new SqlParameter("@Value", $"{version}"));
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