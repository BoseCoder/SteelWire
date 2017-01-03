namespace SteelWire.AppCode.Config
{
    /// <summary>
    /// 语言
    /// </summary>
    public enum LanguageEnum
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default,
        /// <summary>
        /// 英文
        /// </summary>
        English,
        /// <summary>
        /// 中文
        /// </summary>
        Chinese
    }

    public enum UnitEnum
    {
        /// <summary>
        /// 毫米
        /// </summary>
        Millimetre,
        /// <summary>
        /// 米
        /// </summary>
        Metre,
        /// <summary>
        /// 千克
        /// </summary>
        Kilogram,
        /// <summary>
        /// 千克/米
        /// </summary>
        KilogramPerMetre,
        /// <summary>
        /// 克/立方厘米
        /// </summary>
        GramPerCubicCentimetre,
        /// <summary>
        /// 千克/立方米
        /// </summary>
        KilogramPerCubicMeter,
        /// <summary>
        /// 千牛
        /// </summary>
        Kilonewton,
        /// <summary>
        /// 吨
        /// </summary>
        Ton,
        /// <summary>
        /// 吨*公里
        /// </summary>
        TonKilometre
    }

    /// <summary>
    /// 钻机驱动方式
    /// </summary>
    public enum DrillingTypeEnum
    {
        /// <summary>
        /// 顶驱动
        /// </summary>
        TopDrive,
        /// <summary>
        /// 非顶驱动
        /// </summary>
        NotTopDrive
    }

    /// <summary>
    /// 历史动作
    /// </summary>
    public enum HistoryEnum
    {
        /// <summary>
        /// 累积
        /// </summary>
        Cumulate,
        /// <summary>
        /// 切绳
        /// </summary>
        Cut
    }
}
