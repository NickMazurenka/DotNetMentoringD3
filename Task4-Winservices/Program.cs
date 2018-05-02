using System.ServiceProcess;

namespace UselessWindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new UselessService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
