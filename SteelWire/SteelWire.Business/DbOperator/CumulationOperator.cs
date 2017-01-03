using System;
using SteelWire.Business.Database;
using System.Collections.Generic;
using System.Linq;

namespace SteelWire.Business.DbOperator
{
    public class CumulationOperator
    {
        #region Cumulation Dictionary

        public static CumulationDictionary GetLastDictionary(long searchUserId)
        {
            using (SteelWireBaseContext dbContext = DbContextFactory.GenerateDbContext())
            {
                return GetLastDictionary(dbContext, searchUserId);
            }
        }

        public static CumulationDictionary GetLastDictionary(SteelWireBaseContext dbContext, long searchUserId)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            IQueryable<CumulationDictionary> query = dbContext.CumulationDictionary;
            if (searchUserId > 0)
            {
                query = query.Where(d => d.ConfigUserID == searchUserId);
            }
            return query.OrderByDescending(d => d.ConfigTime).FirstOrDefault();
        }

        public static bool IsNeedUpdateCumulationDictionary(SteelWireBaseContext dbContext, long searchUserId,
            long timeStamp, out CumulationDictionary dicData)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            IQueryable<CumulationDictionary> quary = dbContext.CumulationDictionary;
            if (searchUserId > 0)
            {
                quary = quary.Where(d => d.ConfigUserID == searchUserId);
            }
            dicData = quary.FirstOrDefault(d => d.ConfigTimeStamp == timeStamp);
            return dicData == null;
        }

        public static void UpdateCumulationDictionary(SteelWireBaseContext dbContext,
            CumulationDictionary cumulationDictionary)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (cumulationDictionary == null)
            {
                throw new ArgumentNullException(nameof(cumulationDictionary));
            }
            dbContext.CumulationDictionary.Add(cumulationDictionary);
            dbContext.SaveChanges();
        }

        #endregion

        #region Cumulation Dictionary

        public static CumulationConfig GetLastConfig(long searchUserId)
        {
            using (SteelWireBaseContext dbContext = DbContextFactory.GenerateDbContext())
            {
                return GetLastConfig(dbContext, searchUserId);
            }
        }

        public static CumulationConfig GetLastConfig(SteelWireBaseContext dbContext, long searchUserId)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            IQueryable<CumulationConfig> query = dbContext.CumulationConfig;
            if (searchUserId > 0)
            {
                query = query.Where(d => d.ConfigUserID == searchUserId);
            }
            return query.OrderByDescending(d => d.ConfigTime).FirstOrDefault();
        }

        public static bool IsNeedUpdateCumulationConfig(SteelWireBaseContext dbContext, long searchUserId,
            long timeStamp, out CumulationConfig configData)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            IQueryable<CumulationConfig> quary = dbContext.CumulationConfig;
            if (searchUserId > 0)
            {
                quary = quary.Where(d => d.ConfigUserID == searchUserId);
            }
            configData = quary.FirstOrDefault(d => d.ConfigTimeStamp == timeStamp);
            return configData == null;
        }

        public static void UpdateCumulationConfig(SteelWireBaseContext dbContext, CumulationConfig cumulationConfig)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (cumulationConfig == null)
            {
                throw new ArgumentNullException(nameof(cumulationConfig));
            }
            dbContext.CumulationConfig.Add(cumulationConfig);
            dbContext.SaveChanges();
        }

        #endregion

        #region Cumulation Record

        public static void UpdateCumulationRecord(SteelWireBaseContext dbContext, long searchUserId, long lineId,
            CumulationRecord cumulationRecord)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (cumulationRecord == null)
            {
                throw new ArgumentNullException(nameof(cumulationRecord));
            }
            if (lineId < 1)
            {
                throw new ArgumentException("lineId is invalid.", nameof(lineId));
            }
            CutRecord cutRecord = CutOperator.GetLastRecord(dbContext, searchUserId, lineId);
            if (cutRecord == null)
            {
                cutRecord = new CutRecord
                {
                    WirelineID = lineId,
                    UpdateUserID = cumulationRecord.CalculateUserID,
                    UpdateTime = cumulationRecord.CalculateTime,
                    CumulationValue = cumulationRecord.CumulationValue,
                    CutValue = 0,
                    RemainValue = cumulationRecord.CumulationValue,
                    CutLength = 0
                };
                dbContext.CutRecord.Add(cutRecord);
                dbContext.SaveChanges();
            }
            else
            {
                cutRecord.CumulationValue = cutRecord.CumulationRecord.Sum(d => d.CumulationValue);
            }
            cumulationRecord.CutRecordID = cutRecord.ID;
            dbContext.CumulationRecord.Add(cumulationRecord);
            dbContext.SaveChanges();
        }

        #endregion

        public static bool ExistWork(long searchUserId, DateTime date)
        {
            using (SteelWireBaseContext dbContext = DbContextFactory.GenerateDbContext())
            {
                return ExistWork(dbContext, searchUserId, date);
            }
        }

        public static bool ExistWork(SteelWireBaseContext dbContext, long searchUserId, DateTime date)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            DateTime startTime = date.Date;
            DateTime endTime = startTime.AddDays(1);
            IQueryable<CumulationRecord> quary = dbContext.CumulationRecord;
            if (searchUserId > 0)
            {
                quary = quary.Where(d => d.CalculateUserID == searchUserId);
            }
            return quary.Any(d => d.CalculateTime >= startTime && d.CalculateTime < endTime);
        }

        public static List<CumulationRecord> GetHistory(long searchUserId, DateTime startTime, int count)
        {
            using (SteelWireBaseContext dbContext = DbContextFactory.GenerateDbContext())
            {
                return GetHistory(dbContext, searchUserId, startTime, count);
            }
        }

        public static List<CumulationRecord> GetHistory(SteelWireBaseContext dbContext, long searchUserId,
            DateTime startTime, int count)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (count < 1)
            {
                count = 10;
            }
            IQueryable<CumulationRecord> quary = dbContext.CumulationRecord;
            if (searchUserId > 0)
            {
                quary = quary.Where(d => d.CalculateUserID == searchUserId);
            }
            return quary.Where(d => d.CalculateTime >= startTime).OrderBy(d => d.CalculateTime).Take(count).ToList();
        }

        public static List<CumulationRecord> GetHistory(SteelWireBaseContext dbContext, long searchUserId, int count = 10)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (count < 1)
            {
                count = 10;
            }
            IQueryable<CumulationRecord> quary = dbContext.CumulationRecord;
            if (searchUserId > 0)
            {
                quary = quary.Where(d => d.CalculateUserID == searchUserId);
            }
            return quary.OrderByDescending(d => d.CalculateTime).Take(count).ToList();
        }
    }
}