using System.ServiceProcess;

namespace NamedPipeSerialProxy.Service
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static int Main()
        {
            ServiceBase.Run(new NamedPipeSerialProxyService());
            return 0;
        }
    }
}
