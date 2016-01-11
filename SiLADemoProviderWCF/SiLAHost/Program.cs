using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace SiLAHost
{
    /// <summary>
    /// 
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            // resolve debug mode
            bool runAsService = !args.Contains("-debug");
            SiLADemoProviderService service = new SiLADemoProviderService();
            if (runAsService)
            {
                ServiceBase[] servicesToRun;
                servicesToRun = new ServiceBase[] 
			{ 
				service
			};

                ServiceBase.Run(servicesToRun);
            }
            else
            {
                // start service for debugging
                service.Start();

                Console.WriteLine("- Running: Hit any key to abort -");
                Console.ReadKey();

                // stop
                service.Stop();
            }        
        
        }
    }
}
