using System;
using System.Runtime.InteropServices;

namespace PowerManagement
{
    internal class BatteryStateInfo
    {
        [DllImport("PowrProf.dll")]
        public static extern UInt32 CallNtPowerInformation
        (
            POWER_INFORMATION_LEVEL InformationLevel,
            IntPtr lpInputBuffer,
            UInt32 nInputBufferSize,
            out SystemBatteryState lpOutputBuffer,
            UInt32 nOutputBufferSize
        );
    }
}