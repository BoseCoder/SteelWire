using System;
using SteelWire.AppCode.Config;
using SteelWire.AppCode.Dependencies;
using SteelWire.Business.Database;
using SteelWire.Language;

namespace SteelWire.AppCode.Data
{
    public class HistoryData
    {
        public DateTime Time { get; }
        public WirelineInfo LineInfo { get; }
        public SecurityUser User { get; }
        public HistoryEnum Action { get; }
        public decimal Result { get; }
        public DependencyLanguage Text { get; }

        public HistoryData(DateTime time, WirelineInfo lineInfo, SecurityUser user, HistoryEnum action, decimal result)
        {
            this.Time = time;
            this.LineInfo = lineInfo;
            this.User = user;
            this.Action = action;
            this.Result = result;
            this.Text = DependencyLanguage.Generate(() =>
                $"{this.Time:yyyy-MM-dd HH:mm:ss}  {LanguageManager.GetLocalResourceStringLeft("HistoryDataLineNumber")}{this.LineInfo.Number}  {(string.IsNullOrWhiteSpace(this.User.Name) ? this.User.Account : this.User.Name)}{LanguageManager.GetLocalResourceStringLeft(nameof(HistoryEnum), this.Action.ToString())}{this.Result:F3}{LanguageManager.GetLocalResourceStringLeft($"{this.LineInfo.UnitSystem}{UnitEnum.TonKilometre}")}");
        }
    }
}
