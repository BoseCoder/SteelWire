﻿using System.Collections.Generic;
using System.Linq;

namespace SteelWire.Business.Database
{
    public abstract class DatabaseUpgrade
    {
        protected const string DatabaseVersionKey = "DatabaseVersion";
        protected const string DatabaseDefaultVersion = "0";
        protected abstract Dictionary<int, string> UpgradeSql { get; }

        protected abstract int GetDatabaseVersion();
        protected abstract int UpgradeDatabase(int version, string sql);

        public void UpgradeDatabase()
        {
            int oldVersion = GetDatabaseVersion();
            IOrderedEnumerable<KeyValuePair<int, string>> sqls =
                UpgradeSql.Where(s => s.Key > oldVersion).OrderBy(s => s.Key);
            foreach (KeyValuePair<int, string> sql in sqls)
            {
                UpgradeDatabase(sql.Key, sql.Value);
            }
        }
    }
}