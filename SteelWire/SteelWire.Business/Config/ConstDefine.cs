namespace SteelWire.Business.Config
{
    /// <summary>
    /// 单位体系
    /// </summary>
    public enum UnitSystemEnum
    {
        /// <summary>
        /// 国际单位体系
        /// </summary>
        InternationalSystem,
        /// <summary>
        /// 英制单位体系
        /// </summary>
        ImperialSystem
    }

    /// <summary>
    /// 钻类型（钻杆/加重钻杆/钻铤/套管）
    /// </summary>
    public enum DrillDeviceTypeEnum
    {
        /// <summary>
        /// 钻杆
        /// </summary>
        DrillPipe,
        /// <summary>
        /// 加重钻杆
        /// </summary>
        HeavierDrillPipe,
        /// <summary>
        /// 钻铤
        /// </summary>
        DrillCollar,
        /// <summary>
        /// 套管
        /// </summary>
        Bushing
    }
}
