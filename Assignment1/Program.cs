using log4net;
using log4net.Config;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Asg1
{
    class Program
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            ProgramStopwatch.startWatch();

            DirWalker dirWalker = new DirWalker();
            dirWalker.walk(@"C:\SMU\Software Developement\Sample Data\Sample Data");
            dirWalker.writeCSVFile();
            log.Info("Total execution time: " + ProgramStopwatch.stopWatchAndGetTime());
            Console.WriteLine("RunTime " + ProgramStopwatch.stopWatchAndGetTime());
        }
    }

    public class ProgramStopwatch
    {
        private static ProgramStopwatch instance = new ProgramStopwatch();
        private static Stopwatch stopWatch;

        private ProgramStopwatch()
        {
        }

        public static ProgramStopwatch Instance
        {
            get
            {
                return instance;
            }
        }

        public static void startWatch()
        {
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }
        public static string stopWatchAndGetTime()
        {
            if(stopWatch == null)
            {
                return string.Empty;
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            return elapsedTime;
        }
    }
}