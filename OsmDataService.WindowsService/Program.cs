﻿using System;
using System.Collections;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;

namespace OsmDataService.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length > 0 &&
                args[0] != null &&
                (args[0] == "-i" || args[0] == "-u"))
            {
                // install the service.
                if (!(args[0] == "-i"))
                {
                    if (args[0] == "-u")
                    {
                        Install(true, args);
                        return;
                    }
                }
                else
                {
                    Install(false, args);
                    return;
                }
            }
            else
            {
                // runs the service.
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                                    {
                                        new TileDataServiceService()
                                    };
                ServiceBase.Run(ServicesToRun);
            }
        }

        /// <summary>
        /// Actually installs/uninstalls this service.
        /// </summary>
        /// <param name="undo"></param>
        /// <param name="args"></param>
        private static void Install(bool undo, string[] args)
        {
            try
            {
                using (AssemblyInstaller installer = new AssemblyInstaller(
                    Assembly.GetEntryAssembly(), args))
                {
                    IDictionary savedState = new Hashtable();
                    installer.UseNewContext = true;
                    try
                    {
                        if (undo)
                        {
                            installer.Uninstall(savedState);
                        }
                        else
                        {
                            installer.Install(savedState);
                            installer.Commit(savedState);
                        }
                    }
                    catch
                    {
                        try
                        {
                            installer.Rollback(savedState);
                        }
                        catch
                        {
                        }
                        throw;
                    }
                }
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message);
            }
        }
        
    }
}
