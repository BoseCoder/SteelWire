using System.Management;

namespace SteelWire.Environment
{
    public static class Machine
    {
        /// <summary>
        /// 取得设备硬盘的卷标号
        /// </summary>
        /// <returns></returns>
        public static string GetDiskVolumeSerialNumber()
        {
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            return string.Format("{0}", disk.GetPropertyValue("VolumeSerialNumber"));
        }

        /// <summary>
        /// 获得CPU的序列号
        /// </summary>
        /// <returns></returns>
        public static string GetCpuSerialNumber()
        {
            ManagementClass cpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection myCpuConnection = cpu.GetInstances();
            ManagementObjectCollection.ManagementObjectEnumerator enumerator = myCpuConnection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                return string.Format("{0}", enumerator.Current.Properties["Processorid"].Value);
            }
            return null;
        }

        /// <summary>
        /// 生成机器码
        /// </summary>
        /// <returns></returns>
        public static string GetMachineNumber()
        {
            string number = string.Format("{0}{1}", GetDiskVolumeSerialNumber(), GetCpuSerialNumber());
            if (number.Length > 24)
            {
                number = number.Substring(0, 24);
            }
            return number.ToUpper();
        }
    }
}
