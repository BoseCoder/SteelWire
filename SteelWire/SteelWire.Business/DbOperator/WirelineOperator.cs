using System;
using System.Linq;
using SteelWire.Business.Database;

namespace SteelWire.Business.DbOperator
{
    public static class WirelineOperator
    {
        public static WirelineInfo GetWireline(long lineId)
        {
            if (lineId < 1)
            {
                throw new ArgumentException("lineId is invalid.", nameof(lineId));
            }
            using (SteelWireBaseContext dbContext = DbContextFactory.GenerateDbContext())
            {
                return GetWireline(dbContext, lineId);
            }
        }

        public static WirelineInfo GetWireline(SteelWireBaseContext dbContext, long lineId)
        {
            if (lineId < 1)
            {
                throw new ArgumentException("lineId is invalid.", nameof(lineId));
            }
            return dbContext.WirelineInfo.FirstOrDefault(d => d.ID == lineId);
        }

        public static WirelineInfo GetWireline(string lineNumber)
        {
            if (string.IsNullOrWhiteSpace(lineNumber))
            {
                throw new ArgumentNullException(nameof(lineNumber));
            }
            using (SteelWireBaseContext dbContext = DbContextFactory.GenerateDbContext())
            {
                return GetWireline(dbContext, lineNumber);
            }
        }

        public static WirelineInfo GetWireline(SteelWireBaseContext dbContext, string lineNumber)
        {
            if (string.IsNullOrWhiteSpace(lineNumber))
            {
                throw new ArgumentNullException(nameof(lineNumber));
            }
            return dbContext.WirelineInfo.FirstOrDefault(d => d.Number == lineNumber);
        }

        public static void UpdateWireline(WirelineInfo lineInfo, out bool diameterChanged, out bool unitSystemChanged)
        {
            if (lineInfo == null)
            {
                throw new ArgumentNullException(nameof(lineInfo));
            }
            using (SteelWireBaseContext dbContext = DbContextFactory.GenerateDbContext())
            {
                UpdateWireline(dbContext, lineInfo, out diameterChanged, out unitSystemChanged);
                dbContext.SaveChanges();
            }
        }

        public static void UpdateWireline(SteelWireBaseContext dbContext, WirelineInfo lineInfo,
            out bool diameterChanged, out bool unitSystemChanged)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (lineInfo == null)
            {
                throw new ArgumentNullException(nameof(lineInfo));
            }
            WirelineInfo regionInfo = GetWireline(dbContext, lineInfo.Number);
            if (regionInfo == null)
            {
                lineInfo.ID = 0;
                lineInfo.UpdateTime = DateTime.Now;
                dbContext.WirelineInfo.Add(lineInfo);
                dbContext.SaveChanges();
                diameterChanged = true;
                unitSystemChanged = false;
                return;
            }
            diameterChanged = lineInfo.Diameter != regionInfo.Diameter;
            unitSystemChanged = lineInfo.UnitSystem != regionInfo.UnitSystem;
            regionInfo.UpdateUserID = lineInfo.UpdateUserID;
            regionInfo.Diameter = lineInfo.Diameter;
            regionInfo.Struct = lineInfo.Struct;
            regionInfo.StrongLevel = lineInfo.StrongLevel;
            regionInfo.TwistDirection = lineInfo.TwistDirection;
            regionInfo.OrderLength = lineInfo.OrderLength;
            regionInfo.UnitSystem = lineInfo.UnitSystem;
            regionInfo.UpdateTime = DateTime.Now;
        }
    }
}