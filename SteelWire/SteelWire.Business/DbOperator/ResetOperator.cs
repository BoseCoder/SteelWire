using SteelWire.Business.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SteelWire.Business.DbOperator
{
    public static class ResetOperator
    {
        public static CumulationReset GetCurrentData(int updater, string wireNo)
        {
            SteelWireContext dbContext = new SteelWireContext();
            return GetCurrentData(dbContext, updater, wireNo);
        }

        public static CumulationReset GetCurrentData(SteelWireContext dbContext, int updater, string wireNo)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (updater < 1)
            {
                throw new ArgumentException("updater is invalid.", "updater");
            }
            if (string.IsNullOrWhiteSpace(wireNo))
            {
                throw new ArgumentNullException("wireNo");
            }
            CumulationReset data = dbContext.CumulationReset.OrderByDescending(d => d.UpdateTime)
                .FirstOrDefault(d => d.UpdateUserID == updater && d.SteelWireNo == wireNo);
            if (data != null && data.ResetValue > 0)
            {
                data = null;
            }
            return data;
        }

        public static List<CumulationReset> GetResetHistory(int updater, int count)
        {
            SteelWireContext dbContext = new SteelWireContext();
            return GetResetHistory(dbContext, updater, count);
        }

        public static List<CumulationReset> GetResetHistory(SteelWireContext dbContext, int updater, int count)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (updater < 1)
            {
                throw new ArgumentException("updater is invalid.", "updater");
            }
            if (count < 1)
            {
                return new List<CumulationReset>();
            }
            return dbContext.CumulationReset.Where(d => d.UpdateUserID == updater && d.ResetValue > 0)
                .Take(count).OrderBy(d => d.UpdateTime).ToList();
        }

        public static List<CumulationReset> GetResetHistoryForCut(string wireNo)
        {
            SteelWireContext dbContext = new SteelWireContext();
            return GetResetHistoryForCut(dbContext, wireNo);
        }

        public static List<CumulationReset> GetResetHistoryForCut(SteelWireContext dbContext, string wireNo)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (string.IsNullOrWhiteSpace(wireNo))
            {
                throw new ArgumentNullException("wireNo");
            }
            return dbContext.CumulationReset.Where(d => d.SteelWireNo == wireNo && d.ResetValue > 0)
                .OrderBy(d => d.UpdateTime).ToList();
        }

        public static bool ExistReset(int updater, string wireNo, DateTime date)
        {
            SteelWireContext dbContext = new SteelWireContext();
            return ExistReset(dbContext, updater, wireNo, date);
        }

        public static bool ExistReset(SteelWireContext dbContext, int updater, string wireNo, DateTime date)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (updater < 1)
            {
                throw new ArgumentException("updater is invalid.", "updater");
            }
            if (string.IsNullOrWhiteSpace(wireNo))
            {
                throw new ArgumentNullException("wireNo");
            }
            DateTime startTime = date.Date;
            DateTime endTime = startTime.AddDays(1);
            return dbContext.CumulationReset.Any(d =>
                d.UpdateUserID == updater
                && d.SteelWireNo == wireNo
                && d.UpdateTime >= startTime
                && d.UpdateTime < endTime
                && d.ResetValue > 0);
        }

        public static void Reset(int updater, string wireNo)
        {
            SteelWireContext dbContext = new SteelWireContext();
            Reset(dbContext, updater, wireNo);
            dbContext.SaveChanges();
        }

        public static void Reset(SteelWireContext dbContext, int updater, string wireNo, CumulationReset data = null)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (updater < 1)
            {
                throw new ArgumentException("updater is invalid.", "updater");
            }
            if (string.IsNullOrWhiteSpace(wireNo))
            {
                throw new ArgumentNullException("wireNo");
            }
            if (data == null)
            {
                data = GetCurrentData(dbContext, updater, wireNo);
            }
            data.ResetValue = data.CumulationValue;
            data.RemainValue = 0;
            data.UpdateTime = DateTime.Now;
        }
    }
}