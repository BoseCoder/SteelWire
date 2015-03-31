using SteelWire.Business.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SteelWire.Business.DbOperator
{
    public static class ResetOperator
    {
        public static CumulationReset GetCurrentData(int updater)
        {
            SteelWireContext dbContext = new SteelWireContext();
            return GetCurrentData(dbContext, updater);
        }

        public static CumulationReset GetCurrentData(SteelWireContext dbContext, int updater)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (updater < 1)
            {
                throw new ArgumentException("updater is invalid.", "updater");
            }
            CumulationReset data = dbContext.CumulationReset
                .OrderByDescending(d => d.UpdateTime).FirstOrDefault(d => d.UpdateUserID == updater);
            if (data != null && data.ResetValue != 0)
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

        public static void Reset(int updater)
        {
            SteelWireContext dbContext = new SteelWireContext();
            Reset(dbContext, updater);
            dbContext.SaveChanges();
        }

        public static void Reset(SteelWireContext dbContext, int updater, CumulationReset data = null)
        {
            if (data == null)
            {
                data = GetCurrentData(dbContext, updater);
            }
            data.ResetValue = data.CumulationValue;
            data.RemainValue = 0;
            data.UpdateTime = DateTime.Now;
        }
    }
}
