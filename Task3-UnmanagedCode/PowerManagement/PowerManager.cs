using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace PowerManagement
{
    public enum NTSTATUS
    {
        STATUS_SUCCESS = 0,
        STATUS_ACCESS_DENIED = 22,
        STATUS_BUFFER_TOO_SMALL = 23,
    }

    [ComVisible(true)]
    [Guid("7915e1d0-0265-4fb2-9a1c-1eaa26e622b2")]
    [ClassInterface(ClassInterfaceType.None)]
    public class PowerManager : IPowerManager
    {
        public UInt64 GetLastSleepTimeMilliseconds()
        {
            UInt64 result;
            UInt32 status = LastTimeInfo.CallNtPowerInformation(POWER_INFORMATION_LEVEL.LastSleepTime, IntPtr.Zero, 0, out result, sizeof(UInt64));
            switch ((NTSTATUS)status)
            {
                case NTSTATUS.STATUS_SUCCESS:
                    return result;
                case NTSTATUS.STATUS_ACCESS_DENIED:
                    throw new Win32Exception((int)status, "Access denied");
                case NTSTATUS.STATUS_BUFFER_TOO_SMALL:
                    throw new Win32Exception((int)status, "Buffer too small");
                default:
                    throw new Win32Exception((int)status, "Something went wrong :(");
            }
        }

        public UInt64 GetLastWakeTimeMilliseconds()
        {
            UInt64 result;
            UInt32 status = LastTimeInfo.CallNtPowerInformation(POWER_INFORMATION_LEVEL.LastWakeTime, IntPtr.Zero, 0, out result, sizeof(UInt64));
            switch ((NTSTATUS)status)
            {
                case NTSTATUS.STATUS_SUCCESS:
                    return result;
                case NTSTATUS.STATUS_ACCESS_DENIED:
                    throw new Win32Exception((int)status, "Access denied");
                case NTSTATUS.STATUS_BUFFER_TOO_SMALL:
                    throw new Win32Exception((int)status, "Buffer too small");
                default:
                    throw new Win32Exception((int)status, "Something went wrong :(");
            }
        }

        public SystemBatteryState GetSystemBatteryState()
        {
            SystemBatteryState result;
            UInt32 status = BatteryStateInfo.CallNtPowerInformation(
                POWER_INFORMATION_LEVEL.SystemBatteryState,
                IntPtr.Zero,
                0,
                out result,
                (UInt32)Marshal.SizeOf(typeof(SystemBatteryState)));

            switch ((NTSTATUS)status)
            {
                case NTSTATUS.STATUS_SUCCESS:
                    return result;
                case NTSTATUS.STATUS_ACCESS_DENIED:
                    throw new Win32Exception((int)status, "Access denied");
                case NTSTATUS.STATUS_BUFFER_TOO_SMALL:
                    throw new Win32Exception((int)status, "Buffer too small");
                default:
                    throw new Win32Exception((int)status, "Something went wrong :(");
            }
        }
    }
}
