namespace SteelWire.AppCode.Config
{
    /// <summary>
    /// 钻井方式
    /// </summary>
    public enum DrillingTypeEnum
    {
        TopDrive,
        ReamerAndTopDrive,
        NoRedressing,
        RedressingOnce,
        RedressingTwice
    }

    /// <summary>
    /// 钻井难度
    /// </summary>
    public enum DrillingDifficultyEnum
    {
        Easy,
        Normal,
        Hard,
        Difficult
    }
}
