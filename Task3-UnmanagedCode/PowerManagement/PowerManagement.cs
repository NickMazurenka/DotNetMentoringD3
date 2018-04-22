using System;
using System.Runtime.InteropServices;

namespace PowerManagement
{
    public enum POWER_INFORMATION_LEVEL
    {
        SystemPowerPolicyAc,
        SystemPowerPolicyDc,
        VerifySystemPolicyAc,
        VerifySystemPolicyDc,
        SystemPowerCapabilities,
        SystemBatteryState,
        SystemPowerStateHandler,
        ProcessorStateHandler,
        SystemPowerPolicyCurrent,
        AdministratorPowerPolicy,
        SystemReserveHiberFile,
        ProcessorInformation,
        SystemPowerInformation,
        ProcessorStateHandler2,
        LastWakeTime,
        LastSleepTime,
        SystemExecutionState,
        SystemPowerStateNotifyHandler,
        ProcessorPowerPolicyAc,
        ProcessorPowerPolicyDc,
        VerifyProcessorPowerPolicyAc,
        VerifyProcessorPowerPolicyDc,
        ProcessorPowerPolicyCurrent,
        SystemPowerStateLogging,
        SystemPowerLoggingEntry,
        SetPowerSettingValue,
        NotifyUserPowerSetting,
        PowerInformationLevelUnused0,
        SystemMonitorHiberBootPowerOff,
        SystemVideoState,
        TraceApplicationPowerMessage,
        TraceApplicationPowerMessageEnd,
        ProcessorPerfStates,
        ProcessorIdleStates,
        ProcessorCap,
        SystemWakeSource,
        SystemHiberFileInformation,
        TraceServicePowerMessage,
        ProcessorLoad,
        PowerShutdownNotification,
        MonitorCapabilities,
        SessionPowerInit,
        SessionDisplayState,
        PowerRequestCreate,
        PowerRequestAction,
        GetPowerRequestList,
        ProcessorInformationEx,
        NotifyUserModeLegacyPowerEvent,
        GroupPark,
        ProcessorIdleDomains,
        WakeTimerList,
        SystemHiberFileSize,
        ProcessorIdleStatesHv,
        ProcessorPerfStatesHv,
        ProcessorPerfCapHv,
        ProcessorSetIdle,
        LogicalProcessorIdling,
        UserPresence,
        PowerSettingNotificationName,
        GetPowerSettingValue,
        IdleResiliency,
        SessionRITState,
        SessionConnectNotification,
        SessionPowerCleanup,
        SessionLockState,
        SystemHiberbootState,
        PlatformInformation,
        PdcInvocation,
        MonitorInvocation,
        FirmwareTableInformationRegistered,
        SetShutdownSelectedTime,
        SuspendResumeInvocation,
        PlmPowerRequestCreate,
        ScreenOff,
        CsDeviceNotification,
        PlatformRole,
        LastResumePerformance,
        DisplayBurst,
        ExitLatencySamplingPercentage,
        RegisterSpmPowerSettings,
        PlatformIdleStates,
        ProcessorIdleVeto,
        PlatformIdleVeto,
        SystemBatteryStatePrecise,
        ThermalEvent,
        PowerRequestActionInternal,
        BatteryDeviceState,
        PowerInformationInternal,
        ThermalStandby,
        SystemHiberFileType,
        PhysicalPowerButtonPress,
        QueryPotentialDripsConstraint,
        EnergyTrackerCreate,
        EnergyTrackerQuery,
        UpdateBlackBoxRecorder,
        PowerInformationLevelMaximum
    };

    public enum NTSTATUS
    {
        STATUS_SUCCESS = 0,
        STATUS_ACCESS_DENIED = 22,
        STATUS_BUFFER_TOO_SMALL = 23,
    }

    public class PowerManagement
    {
        [DllImport("PowrProf.dll")]
        public static extern UInt32 CallNtPowerInformation
        (
            POWER_INFORMATION_LEVEL InformationLevel,
            IntPtr lpInputBuffer,
            UInt32 nInputBufferSize,
            out UInt64 lpOutputBuffer,
            UInt32 nOutputBufferSize
        );

        public UInt64 GetLastSleepTimeMilliseconds()
        {
            UInt64 result = 0;
            UInt32 status = CallNtPowerInformation(POWER_INFORMATION_LEVEL.LastSleepTime, IntPtr.Zero, 0, out result, sizeof(UInt64));
            switch ((NTSTATUS)status)
            {
                case NTSTATUS.STATUS_SUCCESS:
                    return result;
                case NTSTATUS.STATUS_ACCESS_DENIED:
                    throw new Exception("Access denied");
                case NTSTATUS.STATUS_BUFFER_TOO_SMALL:
                    throw new Exception("Buffer too small");
                default:
                    throw new Exception("Something went wrong :(");
            }
        }

        public UInt64 GetLastWakeTimeMilliseconds() {
            UInt64 result = 1;
            UInt32 status = CallNtPowerInformation(POWER_INFORMATION_LEVEL.LastWakeTime, IntPtr.Zero, 0, out result, sizeof(UInt64));
            switch ((NTSTATUS)status)
            {
                case NTSTATUS.STATUS_SUCCESS:
                    return result;
                case NTSTATUS.STATUS_ACCESS_DENIED:
                    throw new Exception("Access denied");
                case NTSTATUS.STATUS_BUFFER_TOO_SMALL:
                    throw new Exception("Buffer too small");
                default:
                    throw new Exception("Something went wrong :(");
            }
        }
    }
}
