using System;
using SteelWire.Business.Database;
using System.Collections.Generic;
using System.Linq;

namespace SteelWire.Business.DbOperator
{
    public class WorkOperator
    {
        public static decimal RefreshSum(int updater, DateTime starTime, DateTime endTime)
        {
            SteelWireContext dbContext = new SteelWireContext();
            return RefreshSum(dbContext, updater, starTime, endTime);
        }

        public static decimal RefreshSum(SteelWireContext dbContext, int updater, DateTime starTime, DateTime endTime)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            return dbContext.WorkConfig.Where(d => d.ConfigUserID == updater && d.ConfigTime >= starTime && d.ConfigTime <= endTime)
                .Sum(d => d.WorkValue);
        }

        public static WorkConfig GetLastConfig(int updater)
        {
            SteelWireContext dbContext = new SteelWireContext();
            return dbContext.WorkConfig.OrderByDescending(d => d.ConfigTime).FirstOrDefault(d => d.ConfigUserID == updater);
        }

        public static List<WorkConfig> GetWorkHistory(int updater, DateTime startTime, int count)
        {
            SteelWireContext dbContext = new SteelWireContext();
            return GetWorkHistory(dbContext, updater, startTime, count);
        }

        public static List<WorkConfig> GetWorkHistory(SteelWireContext dbContext, int updater, DateTime startTime, int count)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            IQueryable<WorkConfig> data = dbContext.WorkConfig.Where(d => d.ConfigUserID == updater && d.ConfigTime >= startTime);
            if (count > 0)
            {
                data = data.Take(count);
            }
            return data.OrderBy(d => d.ConfigTime).ToList();
        }

        public static WorkDictionary GetCurrentDictionary(int updater)
        {
            SteelWireContext dbContext = new SteelWireContext();
            return dbContext.WorkDictionary.OrderByDescending(d => d.ConfigTime).FirstOrDefault(d => d.ConfigUserID == updater);
        }

        public static void UpdateWork(int updater, WorkConfig work)
        {
            SteelWireContext dbContext = new SteelWireContext();
            UpdateWork(dbContext, updater, work);
        }

        public static void UpdateWork(SteelWireContext dbContext, int updater, WorkConfig work)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (work == null)
            {
                throw new ArgumentNullException("work");
            }
            CumulationReset data = ResetOperator.GetCurrentData(dbContext, updater);
            dbContext.WorkConfig.Add(work);
            data.CumulationValue += work.WorkValue;
        }

        public static bool IsNeedUpdateDictionary(int updater, DateTime dicTime, out bool refreshTime, out WorkDictionary dicData)
        {
            SteelWireContext dbContext = new SteelWireContext();
            return IsNeedUpdateDictionary(dbContext, updater, dicTime, out refreshTime, out dicData);
        }

        public static bool IsNeedUpdateDictionary(SteelWireContext dbContext, int updater, DateTime dicTime, out bool refreshTime, out WorkDictionary dicData)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (dbContext.WorkDictionary.Any(d => d.ConfigUserID == updater && d.ConfigTime > dicTime))
            {
                refreshTime = true;
                dicData = null;
                return true;
            }
            refreshTime = false;
            dicData = dbContext.WorkDictionary.FirstOrDefault(d => d.ConfigUserID == updater && d.ConfigTime == dicTime);
            return dicData == null;
        }

        public static void UpdateDictionary(WorkDictionary workDictionary)
        {
            SteelWireContext dbContext = new SteelWireContext();
            UpdateDictionary(dbContext, workDictionary);
        }

        public static void UpdateDictionary(SteelWireContext dbContext, WorkDictionary workDictionary)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            dbContext.WorkDictionary.Add(workDictionary);
        }
    }
}
