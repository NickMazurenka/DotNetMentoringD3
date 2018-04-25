using System;
using Xunit;
using PowerManagement;

namespace PowerManagementTest
{
    public class PowerManagementTest
    {
        [Fact]
        public void TestGetLastSleepTimeMilliseconds()
        {
            Assert.True(new PowerManager().GetLastSleepTimeMilliseconds() >= 0, "Last Sleep Time is greater or equal to 0");
        }

        [Fact]
        public void TestGetLastWakeTimeMilliseconds()
        {
            Assert.True(new PowerManager().GetLastWakeTimeMilliseconds() >= 0, "Last Wake Time is greater or equal to 0");
        }

        [Fact]
        public void TestSystemBatteryState()
        {
            var batteryState = new PowerManager().GetSystemBatteryState();
            Assert.True(batteryState.AcOnLine, "Battery charger is currently operating on external power");
        }
    }
}
