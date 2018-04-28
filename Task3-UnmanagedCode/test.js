var powerManager = new ActiveXObject("PowerManagement.PowerManager");
var lastSleepTime = powerManager.GetLastWakeTimeMilliseconds();
WScript.Echo("Last sleep time: ");
WScript.Echo(lastSleepTime);