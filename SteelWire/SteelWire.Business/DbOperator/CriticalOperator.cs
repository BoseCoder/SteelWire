using SteelWire.Business.Database;
using System;
using System.Linq;

namespace SteelWire.Business.DbOperator
{
    public static class CriticalOperator
    {
        #region Critical Dictionary

        public static CriticalDictionary GetLastDictionary(long searchUserId)
        {
            using (SteelWireBaseContext dbContext = DbContextFactory.GenerateDbContext())
            {
                return GetLastDictionary(dbContext, searchUserId);
            }
        }

        public static CriticalDictionary GetLastDictionary(SteelWireBaseContext dbContext, long searchUserId)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            IQueryable<CriticalDictionary> query = dbContext.CriticalDictionary;
            if (searchUserId > 0)
            {
                query = query.Where(d => d.ConfigUserID == searchUserId);
            }
            return query.OrderByDescending(d => d.ConfigTime).FirstOrDefault();
        }

        public static bool IsNeedUpdateCriticalDictionary(SteelWireBaseContext dbContext, long searchUserId,
            long timeStamp, out CriticalDictionary dicData)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            IQueryable<CriticalDictionary> quary = dbContext.CriticalDictionary;
            if (searchUserId > 0)
            {
                quary = quary.Where(d => d.ConfigUserID == searchUserId);
            }
            dicData = quary.FirstOrDefault(d => d.ConfigTimeStamp == timeStamp);
            return dicData == null;
        }

        public static void UpdateCriticalDictionary(SteelWireBaseContext dbContext,
            CriticalDictionary criticalDictionary)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (criticalDictionary == null)
            {
                throw new ArgumentNullException(nameof(criticalDictionary));
            }
            dbContext.CriticalDictionary.Add(criticalDictionary);
            dbContext.SaveChanges();
        }

        #endregion

        #region Critical Config

        public static CriticalConfig GetLastConfig(long searchUserId)
        {
            using (SteelWireBaseContext dbContext = DbContextFactory.GenerateDbContext())
            {
                return GetLastConfig(dbContext, searchUserId);
            }
        }

        public static CriticalConfig GetLastConfig(SteelWireBaseContext dbContext, long searchUserId)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            IQueryable<CriticalConfig> query = dbContext.CriticalConfig;
            if (searchUserId > 0)
            {
                query = query.Where(d => d.ConfigUserID == searchUserId);
            }
            return query.OrderByDescending(d => d.ConfigTime).FirstOrDefault();
        }

        public static bool IsNeedUpdateCriticalConfig(SteelWireBaseContext dbContext, long searchUserId, long timeStamp,
            out CriticalConfig configData)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            IQueryable<CriticalConfig> quary = dbContext.CriticalConfig;
            if (searchUserId > 0)
            {
                quary = quary.Where(d => d.ConfigUserID == searchUserId);
            }
            configData = quary.FirstOrDefault(d => d.ConfigTimeStamp == timeStamp);
            return configData == null;
        }

        public static void UpdateCriticalConfig(SteelWireBaseContext dbContext, CriticalConfig criticalConfig)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (criticalConfig == null)
            {
                throw new ArgumentNullException(nameof(criticalConfig));
            }
            dbContext.CriticalConfig.Add(criticalConfig);
            dbContext.SaveChanges();
        }

        #endregion

        #region Critical Record

        public static CriticalRecord GetLastRecord(long searchUserId, long lineId)
        {
            if (lineId < 1)
            {
                throw new ArgumentException("lineId is invalid.", nameof(lineId));
            }
            using (SteelWireBaseContext dbContext = DbContextFactory.GenerateDbContext())
            {
                return GetLastRecord(dbContext, searchUserId, lineId);
            }
        }

        public static CriticalRecord GetLastRecord(SteelWireBaseContext dbContext, long searchUserId, long lineId)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (lineId < 1)
            {
                throw new ArgumentException("lineId is invalid.", nameof(lineId));
            }
            IQueryable<CriticalRecord> query = dbContext.CriticalRecord.Where(d => d.WirelineID == lineId);
            if (searchUserId > 0)
            {
                query = query.Where(d => d.CalculateUserID == searchUserId);
            }
            return query.OrderByDescending(d => d.CalculateTime).FirstOrDefault();
        }

        public static bool IsNeedUpdateCriticalRecord(SteelWireBaseContext dbContext, long searchUserId, long lineId,
            decimal criticalValue)
        {
            if (lineId < 1)
            {
                throw new ArgumentException("lineId is invalid.", nameof(lineId));
            }
            CriticalRecord lastRecord = GetLastRecord(dbContext, searchUserId, lineId);
            return lastRecord == null || lastRecord.CriticalValue != criticalValue;
        }

        public static void UpdateCriticalRecord(SteelWireBaseContext dbContext, CriticalRecord criticalRecord)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (criticalRecord == null)
            {
                throw new ArgumentNullException(nameof(criticalRecord));
            }
            dbContext.CriticalRecord.Add(criticalRecord);
            dbContext.SaveChanges();
        }

        #endregion

        public static WireropeWorkload GetLastWireropeWorkloadConfig(long searchUserId, WirelineInfo lineInfo)
        {
            if (lineInfo == null)
            {
                throw new ArgumentNullException(nameof(lineInfo));
            }
            if (lineInfo.ID < 1)
            {
                throw new ArgumentException("ID property of lineInfo is invalid.", nameof(lineInfo));
            }
            using (SteelWireBaseContext dbContext = DbContextFactory.GenerateDbContext())
            {
                return GetLastWireropeWorkloadConfig(dbContext, searchUserId, lineInfo);
            }
        }

        public static WireropeWorkload GetLastWireropeWorkloadConfig(SteelWireBaseContext dbContext, long searchUserId, WirelineInfo lineInfo)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (lineInfo == null)
            {
                throw new ArgumentNullException(nameof(lineInfo));
            }
            if (lineInfo.ID < 1)
            {
                throw new ArgumentException("ID property of lineInfo is invalid.", nameof(lineInfo));
            }
            CriticalRecord criticalRecord = GetLastRecord(dbContext, searchUserId, lineInfo.ID);
            return criticalRecord?.CriticalConfig?.CriticalDictionary?.WireropeWorkload
                .FirstOrDefault(d => d.Name == lineInfo.Diameter && d.UnitSystem == lineInfo.UnitSystem);
        }
    }
}