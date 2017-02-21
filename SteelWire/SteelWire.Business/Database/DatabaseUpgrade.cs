using System.Collections.Generic;
using System.Linq;

namespace SteelWire.Business.Database
{
    public abstract class DatabaseUpgrade
    {
        protected const string DatabaseVersionKey = "DatabaseVersion";
        protected const string DatabaseDefaultVersion = "0";
        protected abstract Dictionary<int, List<string>> UpgradeSql { get; }

        protected abstract int GetDatabaseVersion();
        protected abstract int UpgradeDatabase(int version, string sql);

        public void UpgradeDatabase()
        {
            int oldVersion = GetDatabaseVersion();
            IOrderedEnumerable<KeyValuePair<int, List<string>>> sqls =
                UpgradeSql.Where(s => s.Key > oldVersion).OrderBy(s => s.Key);
            foreach (KeyValuePair<int, List<string>> sqlGroup in sqls)
            {
                foreach (string sql in sqlGroup.Value)
                {
                    UpgradeDatabase(sqlGroup.Key, sql);
                }
            }
        }
    }
}