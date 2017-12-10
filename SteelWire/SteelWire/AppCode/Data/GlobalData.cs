using SteelWire.AppCode.Config;
using SteelWire.AppCode.Dependencies;

namespace SteelWire.AppCode.Data
{
    public static class GlobalData
    {
        public static string AppVersion { get; } = "2.1.0.3";

        public static DependencyObject<LanguageEnum> Language { get; } = new DependencyObject<LanguageEnum>();

        /// <summary>
        /// 当前登录用户Id
        /// </summary>
        public static long UserId { get; set; }

        /// <summary>
        /// 查询数据时使用的用户Id（当不启用数据隔离时，该值为0）
        /// </summary>
        public static long SearchUserId { get; set; }

        /// <summary>
        /// 当前登录用户账户
        /// </summary>
        public static string Account { get; set; }

        /// <summary>
        /// 当前登录用户显示文本
        /// </summary>
        public static DependencyObject<string> UserDisplay { get; } = new DependencyObject<string>();

        /// <summary>
        /// 当前是否登录
        /// </summary>
        public static bool IsSignIn => !string.IsNullOrWhiteSpace(Account);


        public static WirelineData Wireline { get; } = new WirelineData();
    }
}
