namespace SteelWire.AppCode.Config
{
    /// <summary>
    /// 钻杆类型
    /// </summary>
    public enum DrillPipeTypeEnum
    {
        /// <summary>
        /// 钻杆
        /// </summary>
        DrillPipe,
        /// <summary>
        /// 加重钻杆
        /// </summary>
        HeavierDrillPipe
    }

    /// <summary>
    /// 钻井方式
    /// </summary>
    public enum DrillingTypeEnum
    {
        /// <summary>
        /// 使用顶驱动
        /// </summary>
        TopDrive,
        /// <summary>
        /// 使用铰刀和顶驱连接
        /// </summary>
        ReamerAndTopDrive,
        /// <summary>
        /// 无划眼
        /// </summary>
        NoRedressing,
        /// <summary>
        /// 划眼一遍
        /// </summary>
        RedressingOnce,
        /// <summary>
        /// 划眼两遍
        /// </summary>
        RedressingTwice
    }

    /// <summary>
    /// 钻井难度
    /// </summary>
    public enum DrillingDifficultyEnum
    {
        /// <summary>
        /// 容易
        /// </summary>
        Easy,
        /// <summary>
        /// 中等
        /// </summary>
        Normal,
        /// <summary>
        /// 难
        /// </summary>
        Hard,
        /// <summary>
        /// 非常难
        /// </summary>
        Difficult
    }
}
