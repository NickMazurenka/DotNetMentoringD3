using System;
using Xunit;

namespace PowerManagementTest
{
    public class PowerManagementTest
    {
        [Fact]
        public void TestGetLastSleepTimeMilliseconds()
        {
            Assert.True(PowerManagement.PowerManagement.GetLastSleepTimeMilliseconds() >= 0, "Last Sleep Time is greater or equal to 0");
        }

        [Fact]
        public void TestGetLastWakeTimeMilliseconds()
        {
            Assert.True(PowerManagement.PowerManagement.GetLastWakeTimeMilliseconds() >= 0, "Last Wake Time is greater or equal to 0");
        }

        [Fact]
        public void TestSystemBatteryState()
        {
            var batteryState = PowerManagement.PowerManagement.GetSystemBatteryState();
            Assert.True(batteryState.AcOnLine, "Battery charger is currently operating on external power");
        }
    }
}
