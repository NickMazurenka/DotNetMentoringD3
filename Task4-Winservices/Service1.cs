using System;
using System.IO;
using System.Timers;
using System.ServiceProcess;

namespace UselessWindowsService
{
    public partial class UselessService : ServiceBase
    {
        private Timer _timer;

        public UselessService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _timer = new Timer();
            _timer.Interval = 30 * 1000;
            _timer.Elapsed += (object sender, ElapsedEventArgs e) => ShowMessage("Hello from Useless Windows Service. Thanks for choosing us!");
            _timer.Enabled = true;
        }

        protected override void OnStop()
        {
            _timer.Enabled = false;
            ShowMessage("Good bye from Useless Windows Service. Thanks for choosing us!");
        }

        private void ShowMessage(string text)
        {
            try
            {
                using (var sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\log.txt", true))
                {
                    sw.WriteLine(text);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch
            {
            }
        }
    }
}
