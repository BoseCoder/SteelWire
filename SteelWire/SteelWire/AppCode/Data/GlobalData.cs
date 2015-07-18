using IntelliLock.Licensing;
using SteelWire.AppCode.Dependencies;
using SteelWire.Lang;
using SteelWire.WindowData;

namespace SteelWire.AppCode.Data
{
    public class GlobalData
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static readonly GlobalData Data;

        public DependencyItem<string> AppLicenceStatus { get; private set; }
        public DependencyItem<string> UserDisplay { get; private set; }

        static GlobalData()
        {
            Data = new GlobalData
            {
                UserDisplay = Sign.Data.UserDisplay,
                AppLicenceStatus = new DependencyItem<string>()
            };
            Data.InitializeData();
        }

        public void InitializeData()
        {
            if (CurrentLicense.License.LicenseStatus != LicenseStatus.Licensed)
            {
                if (CurrentLicense.License.ExpirationDays_Enabled)
                {
                    this.AppLicenceStatus.ItemValue = string.Format("  [{0} {1}{2}]",
                        LanguageManager.GetLocalResourceStringRight("License", "Trial"),
                        CurrentLicense.License.ExpirationDays,
                        LanguageManager.GetLocalResourceStringRight("License", "TrialRemoveDay"));
                }
                else
                {
                    this.AppLicenceStatus.ItemValue = string.Format("  [{0}]",
                        LanguageManager.GetLocalResourceStringRight("License", "Trial"));
                }
            }
            this.UserDisplay = Sign.Data.UserDisplay;
        }
    }
}
