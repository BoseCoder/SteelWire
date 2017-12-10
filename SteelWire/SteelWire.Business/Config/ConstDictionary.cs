using System.Collections.Generic;

namespace SteelWire.Business.Config
{
    /// <summary>
    /// 初始字典
    /// </summary>
    public static class ConstDictionary
    {
        /// <summary>
        /// 单位体制参数字典
        /// </summary>
        public static Dictionary<UnitSystemEnum, UnitSystemCoefficient> UnitSystemDictionary { get; }

        static ConstDictionary()
        {
            UnitSystemDictionary = new Dictionary<UnitSystemEnum, UnitSystemCoefficient>
            {
                {
                    UnitSystemEnum.InternationalSystem, new UnitSystemCoefficient
                    {
                        FluidDensityCoefficient = 1/7.856M,
                        LeftDenominatorCoefficient = 1000000M,
                        RightDenominatorCoefficient = 250000M
                    }
                },
                {
                    UnitSystemEnum.ImperialSystem, new UnitSystemCoefficient
                    {
                        FluidDensityCoefficient = 0.015M,
                        LeftDenominatorCoefficient = 10560000M,
                        RightDenominatorCoefficient = 2640000M
                    }
                }
            };
        }
    }
}