using System;
using Xunit;

namespace PowerManagementTest
{
    public class PowerManagementTest
    {
        [Fact]
        public void TestGetLastSleepTimeMilliseconds()
        {
            var t = new PowerManagement.PowerManagement();
            Assert.True(t.GetLastSleepTimeMilliseconds() > 0, "Last Sleep Time is greater then 0");
        }

        [Fact]
        public void TestGetLastWakeTimeMilliseconds()
        {
            var t = new PowerManagement.PowerManagement();
            Assert.True(t.GetLastWakeTimeMilliseconds() > 0, "Last Wake Time is greater then 0");
        }
    }
}
