using System;
using System.Runtime.InteropServices;

namespace PowerManagement
{
    [ComVisible(true)]
    [Guid("2815c500-6fcc-4d7e-a252-babd6631ae80")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IPowerManager
    {
        UInt64 GetLastSleepTimeMilliseconds();
        UInt64 GetLastWakeTimeMilliseconds();
        SystemBatteryState GetSystemBatteryState();
    }
}