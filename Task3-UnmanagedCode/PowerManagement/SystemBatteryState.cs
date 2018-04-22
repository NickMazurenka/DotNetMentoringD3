using System;

namespace PowerManagement
{
    public struct SystemBatteryState
    {
        public bool AcOnLine;
        public bool BatteryPresent;
        public bool Charging;
        public bool Discharging;
        public bool Spare1;
        public UInt32 MaxCapacity;
        public UInt32 RemainingCapacity;
        public UInt32 Rate;
        public UInt32 EstimatedTime;
        public UInt32 DefaultAlert1;
        public Int32 DefaultAlert2;
    }
}