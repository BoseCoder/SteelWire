using SteelWire.Business.Database;
using System;
using System.Linq;

namespace SteelWire.Business.DbOperator
{
    public static class CriticalOperator
    {
        public static CuttingCriticalConfig GetLastConfig(int updater)
        {
            SteelWireContext dbContext = new SteelWireContext();
            return dbContext.CuttingCriticalConfig.OrderByDescending(d => d.ConfigTime).FirstOrDefault(d => d.ConfigUserID == updater);
        }

        public static bool IsNeedUpdateCritical(SteelWireContext dbContext, int updater, DateTime dateTime, decimal criticalValue, out bool overWrite)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (updater < 1)
            {
                throw new ArgumentException("updater is invalid.", "updater");
            }
            if (dbContext.CuttingCriticalConfig.Any(d => d.ConfigUserID == updater && d.ConfigTime > dateTime))
            {
                overWrite = true;
                return true;
            }
            overWrite = false;
            CuttingCriticalConfig config = dbContext.CuttingCriticalConfig
                .FirstOrDefault(d => d.ConfigUserID == updater && d.ConfigTime == dateTime);
            if (config != null)
            {
                CumulationReset resetData = ResetOperator.GetCurrentData(dbContext, updater);
                if (resetData != null && config.CuttingCriticalValue == resetData.CriticalValue && resetData.CriticalValue == criticalValue)
                {
                    return false;
                }
            }
            return true;
        }

        public static CumulationReset UpdateCriticalValue(int updater, CuttingCriticalConfig data)
        {
            SteelWireContext dbContext = new SteelWireContext();
            return UpdateCriticalValue(dbContext, updater, data);
        }

        public static CumulationReset UpdateCriticalValue(SteelWireContext dbContext, int updater, CuttingCriticalConfig data)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (updater < 1)
            {
                throw new ArgumentException("updater is invalid.", "updater");
            }
            dbContext.CuttingCriticalConfig.Add(data);
            CumulationReset resetData = ResetOperator.GetCurrentData(dbContext, updater);
            if (resetData == null)
            {
                resetData = new CumulationReset
                {
                    CriticalValue = data.CuttingCriticalValue,
                    CumulationValue = 0,
                    ResetValue = 0,
                    RemainValue = data.CuttingCriticalValue,
                    UpdateUserID = updater,
                    UpdateTime = data.ConfigTime
                };
                dbContext.CumulationReset.Add(resetData);
            }
            else
            {
                resetData.CriticalValue = data.CuttingCriticalValue;
            }
            return resetData;
        }

        public static CuttingCriticalDictionary GetCurrentDictionary(int updater)
        {
            SteelWireContext dbContext = new SteelWireContext();
            return dbContext.CuttingCriticalDictionary.OrderByDescending(d => d.ConfigTime).FirstOrDefault(d => d.ConfigUserID == updater);
        }

        public static bool IsNeedUpdateDictionary(int updater, DateTime dicTime, out bool overWrite, out CuttingCriticalDictionary dicData)
        {
            SteelWireContext dbContext = new SteelWireContext();
            return IsNeedUpdateDictionary(dbContext, updater, dicTime, out overWrite, out dicData);
        }

        public static bool IsNeedUpdateDictionary(SteelWireContext dbContext, int updater, DateTime dateTime, out bool overWrite, out CuttingCriticalDictionary dicData)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (updater < 1)
            {
                throw new ArgumentException("updater is invalid.", "updater");
            }
            if (dbContext.CuttingCriticalDictionary.Any(d => d.ConfigUserID == updater && d.ConfigTime > dateTime))
            {
                overWrite = true;
                dicData = null;
                return true;
            }
            overWrite = false;
            dicData = dbContext.CuttingCriticalDictionary.FirstOrDefault(d => d.ConfigUserID == updater && d.ConfigTime == dateTime);
            return dicData == null;
        }

        public static void UpdateDictionary(CuttingCriticalDictionary cuttingCriticalDictionary)
        {
            SteelWireContext dbContext = new SteelWireContext();
            UpdateDictionary(dbContext, cuttingCriticalDictionary);
        }

        public static void UpdateDictionary(SteelWireContext dbContext, CuttingCriticalDictionary cuttingCriticalDictionary)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            dbContext.CuttingCriticalDictionary.Add(cuttingCriticalDictionary);
        }
    }
}
