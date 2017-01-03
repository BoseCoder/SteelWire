using System;
using System.Collections.Generic;
using System.Linq;
using SteelWire.Business.CalculateCommander;
using SteelWire.Business.Database;

namespace SteelWire.Business.DbOperator
{
    public static class CutOperator
    {
        public static CutRecord GetLastRecord(long searchUserId, long lineId)
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

        public static CutRecord GetLastRecord(SteelWireBaseContext dbContext, long searchUserId, long lineId)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (lineId < 1)
            {
                throw new ArgumentException("lineId is invalid.", nameof(lineId));
            }
            IQueryable<CutRecord> quary = dbContext.CutRecord.Where(d => d.WirelineID == lineId);
            if (searchUserId > 0)
            {
                quary = quary.Where(d => d.UpdateUserID == searchUserId);
            }
            CutRecord record = quary.OrderByDescending(d => d.UpdateTime).FirstOrDefault();
            if (record != null && record.CutValue > 0)
            {
                record = null;
            }
            return record;
        }

        public static decimal GetTotalCuttedValue(SteelWireBaseContext dbContext, long searchUserId, long lineId)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (lineId < 1)
            {
                throw new ArgumentException("lineId is invalid.", nameof(lineId));
            }
            IQueryable<CutRecord> quary = dbContext.CutRecord.Where(d => d.WirelineID == lineId);
            if (searchUserId > 0)
            {
                quary = quary.Where(d => d.UpdateUserID == searchUserId);
            }
            return quary.Sum(d => d.CutValue);
        }

        public static List<CutRecord> GetHistory(long searchUserId, int count = 10)
        {
            using (SteelWireBaseContext dbContext = DbContextFactory.GenerateDbContext())
            {
                return GetHistory(dbContext, searchUserId, count);
            }
        }

        public static List<CutRecord> GetHistory(SteelWireBaseContext dbContext, long searchUserId, int count = 10)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (count < 1)
            {
                count = 10;
            }
            IQueryable<CutRecord> quary = dbContext.CutRecord;
            if (searchUserId > 0)
            {
                quary = quary.Where(d => d.UpdateUserID == searchUserId);
            }
            return quary.Where(d => d.CutValue > 0).OrderByDescending(d => d.UpdateTime).Take(count).ToList();
        }

        public static List<CutRecord> GetAllHistory(long searchUserId, long lineId)
        {
            if (lineId < 1)
            {
                throw new ArgumentException("lineId is invalid.", nameof(lineId));
            }
            using (SteelWireBaseContext dbContext = DbContextFactory.GenerateDbContext())
            {
                return GetAllHistory(dbContext, searchUserId, lineId);
            }
        }

        public static List<CutRecord> GetAllHistory(SteelWireBaseContext dbContext, long searchUserId, long lineId)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (lineId < 1)
            {
                throw new ArgumentException("lineId is invalid.", nameof(lineId));
            }
            IQueryable<CutRecord> quary = dbContext.CutRecord.Where(d => d.WirelineID == lineId);
            if (searchUserId > 0)
            {
                quary = quary.Where(d => d.UpdateUserID == searchUserId);
            }
            return quary.Where(d => d.CutValue > 0).OrderBy(d => d.UpdateTime).ToList();
        }

        public static bool ExistCutRecord(long searchUserId, long lineId, DateTime date)
        {
            if (lineId < 1)
            {
                throw new ArgumentException("lineId is invalid.", nameof(lineId));
            }
            using (SteelWireBaseContext dbContext = DbContextFactory.GenerateDbContext())
            {
                return ExistCutRecord(dbContext, searchUserId, lineId, date);
            }
        }

        public static bool ExistCutRecord(SteelWireBaseContext dbContext, long searchUserId, long lineId, DateTime date)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (lineId < 1)
            {
                throw new ArgumentException("lineId is invalid.", nameof(lineId));
            }
            DateTime startTime = date.Date;
            DateTime endTime = startTime.AddDays(1);
            IQueryable<CutRecord> quary = dbContext.CutRecord.Where(d => d.WirelineID == lineId
                                                                         && d.UpdateTime >= startTime
                                                                         && d.UpdateTime < endTime
                                                                         && d.CutValue > 0);
            if (searchUserId > 0)
            {
                quary = quary.Where(d => d.UpdateUserID == searchUserId);
            }
            return quary.Any();
        }

        public static void Cut(long searchUserId, long updaterId, long lineId, decimal cutLength)
        {
            if (updaterId < 1)
            {
                throw new ArgumentException("updaterId is invalid.", nameof(updaterId));
            }
            if (lineId < 1)
            {
                throw new ArgumentException("lineId is invalid.", nameof(lineId));
            }
            using (SteelWireBaseContext dbContext = DbContextFactory.GenerateDbContext())
            {
                Cut(dbContext, searchUserId, updaterId, lineId, cutLength);
                dbContext.SaveChanges();
            }
        }

        public static void Cut(SteelWireBaseContext dbContext, long searchUserId, long updaterId, long lineId,
            decimal cutLength, CutRecord data = null)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (updaterId < 1)
            {
                throw new ArgumentException("updaterId is invalid.", nameof(updaterId));
            }
            if (lineId < 1)
            {
                throw new ArgumentException("lineId is invalid.", nameof(lineId));
            }
            if (data == null)
            {
                data = GetLastRecord(dbContext, searchUserId, lineId);
            }
            data.CutValue = data.CumulationValue;
            data.RemainValue = 0M;
            data.CutLength = cutLength;
            data.UpdateUserID = updaterId;
            data.UpdateTime = DateTime.Now;
        }

        public static void CalculateAllRecord(SteelWireBaseContext dbContext, long searchUserId, long updaterId,
            long lineId, DateTime now)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (updaterId < 1)
            {
                throw new ArgumentException("updaterId is invalid.", nameof(updaterId));
            }
            if (lineId < 1)
            {
                throw new ArgumentException("lineId is invalid.", nameof(lineId));
            }
            WirelineInfo lineInfo = WirelineOperator.GetWireline(dbContext, lineId);
            if (lineInfo == null)
            {
                return;
            }
            List<CutRecord> cutRecords = GetAllHistory(dbContext, searchUserId, lineId);
            foreach (CutRecord cutRecord in cutRecords)
            {
                decimal cumulationValue = 0;
                foreach (CumulationRecord cumulationRecord in cutRecord.CumulationRecord)
                {
                    CriticalConfig criticalConfig = cumulationRecord.CriticalConfig;
                    WireropeEfficiency wireropeEfficiency = criticalConfig.CriticalDictionary.WireropeEfficiency
                        .FirstOrDefault(d => d.RopeCount == cumulationRecord.CriticalConfig.RopeCount);
                    CumulationConfig cumulationConfig = cumulationRecord.CumulationConfig;
                    List<DrillDeviceConfig> drillDeviceConfigs = cumulationConfig.DrillDeviceConfig.ToList();
                    DrillingType drillingType = cumulationConfig.CumulationDictionary.DrillingType
                        .FirstOrDefault(d => d.Name == cumulationConfig.DrillingType);
                    CommanderCumulation commanderCumulation = new CommanderCumulation(lineInfo, criticalConfig,
                        wireropeEfficiency, cumulationConfig, drillDeviceConfigs, drillingType);
                    cumulationRecord.CumulationValue = commanderCumulation.CalculateValue();
                    cumulationRecord.CalculateTime = now;
                    cumulationRecord.CalculateUserID = updaterId;
                    cumulationValue += cumulationRecord.CumulationValue;
                }
                cutRecord.CumulationValue = cumulationValue;
                if (cutRecord.CutValue > 0)
                {
                    cutRecord.CutValue = cumulationValue;
                    cutRecord.RemainValue = 0;
                }
                else
                {
                    cutRecord.CutValue = 0;
                    cutRecord.RemainValue = cumulationValue;
                }
                cutRecord.UpdateUserID = updaterId;
                cutRecord.UpdateTime = now;
            }
        }
    }
}