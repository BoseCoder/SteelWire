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
            return dbContext.WorkConfig.Where(d =>
                d.ConfigUserID == updater && d.ConfigTime >= starTime && d.ConfigTime <= endTime)
                .Sum(d => d.WorkValue);
        }

        public static WorkConfig GetLastConfig(int updater)
        {
            SteelWireContext dbContext = new SteelWireContext();
            return dbContext.WorkConfig.OrderByDescending(d => d.ConfigTime)
                .FirstOrDefault(d => d.ConfigUserID == updater);
        }

        public static List<WorkConfig> GetWorkHistory(int updater, DateTime startTime, int count)
        {
            SteelWireContext dbContext = new SteelWireContext();
            return GetWorkHistory(dbContext, updater, startTime, count);
        }

        public static List<WorkConfig> GetWorkHistory(SteelWireContext dbContext, int updater, DateTime startTime,
            int count)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            IQueryable<WorkConfig> data = dbContext.WorkConfig.Where(d =>
                d.ConfigUserID == updater && d.ConfigTime >= startTime);
            if (count > 0)
            {
                data = data.Take(count);
            }
            return data.OrderBy(d => d.ConfigTime).ToList();
        }

        public static WorkDictionary GetCurrentDictionary(int updater)
        {
            SteelWireContext dbContext = new SteelWireContext();
            return dbContext.WorkDictionary.OrderByDescending(d => d.ConfigTime)
                .FirstOrDefault(d => d.ConfigUserID == updater);
        }

        public static bool ExistWork(int updater, DateTime date)
        {
            SteelWireContext dbContext = new SteelWireContext();
            return ExistWork(dbContext, updater, date);
        }

        public static bool ExistWork(SteelWireContext dbContext, int updater, DateTime date)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            DateTime startTime = date.Date;
            DateTime endTime = startTime.AddDays(1);
            return dbContext.WorkConfig.Any(d =>
                d.ConfigUserID == updater && d.ConfigTime >= startTime && d.ConfigTime < endTime);
        }

        public static void UpdateWork(int updater, string wireNo, WorkConfig work, decimal criticalValue)
        {
            SteelWireContext dbContext = new SteelWireContext();
            UpdateWork(dbContext, updater, wireNo, work, criticalValue);
        }

        public static void UpdateWork(SteelWireContext dbContext, int updater, string wireNo, WorkConfig work,
            decimal criticalValue)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (work == null)
            {
                throw new ArgumentNullException("work");
            }
            CumulationReset data = ResetOperator.GetCurrentData(dbContext, updater, wireNo);
            if (data == null)
            {
                data = new CumulationReset
                {
                    CriticalValue = criticalValue,
                    CumulationValue = work.WorkValue,
                    RemainValue = 0,
                    ResetValue = 0,
                    UpdateTime = DateTime.Now,
                    UpdateUserID = updater,
                    SteelWireNo = wireNo
                };
                dbContext.CumulationReset.Add(data);
            }
            else
            {
                data.CumulationValue += work.WorkValue;
            }
            dbContext.WorkConfig.Add(work);
        }

        public static bool IsNeedUpdateDictionary(int updater, long timeStamp, out bool refreshTime,
            out WorkDictionary dicData)
        {
            SteelWireContext dbContext = new SteelWireContext();
            return IsNeedUpdateDictionary(dbContext, updater, timeStamp, out refreshTime, out dicData);
        }

        public static bool IsNeedUpdateDictionary(SteelWireContext dbContext, int updater, long timeStamp,
            out bool refreshTime, out WorkDictionary dicData)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (dbContext.WorkDictionary.Any(d => d.ConfigUserID == updater && d.ConfigTimeStamp > timeStamp))
            {
                refreshTime = true;
                dicData = null;
                return true;
            }
            refreshTime = false;
            dicData = dbContext.WorkDictionary.FirstOrDefault(d =>
                d.ConfigUserID == updater && d.ConfigTimeStamp == timeStamp);
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