using System;
using System.Configuration;
using OsmSharp.Tools.Output;

namespace OsmDataService.SelfHost
{
    class Program
    {
        /// <summary>
        /// The main entry point of the application.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // direct the output to the console.
            OsmSharp.Tools.Output.OutputStreamHost.RegisterOutputStream(
                new ConsoleOutputStream());

            // get the hostname.
            string hostname = ConfigurationManager.AppSettings["hostname"];
            OsmSharp.Tools.Output.OutputStreamHost.WriteLine("Service will listen to: {0}",
                hostname);

            // start the self-hosting.
            var host = new AppHost();
            host.Init();
            host.Start(hostname);

            OsmSharp.Tools.Output.OutputStreamHost.WriteLine("OsmDataService started.");
            Console.ReadLine();
        }
    }
}
