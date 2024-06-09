using log4net;
using log4net.Config;
using System;

namespace AuditLogger
{
    public class Logger
    {
        private static readonly ILog log = LogManager.GetLogger("AdoNetAppender");
        public static void LogAuditEvent(string message)
        {
            XmlConfigurator.Configure();
            Console.WriteLine("log: {0}",log);
            Console.WriteLine("isInfoEnabled: {0}", log.IsInfoEnabled);
            if (log.IsInfoEnabled)
            {
                Console.WriteLine("here-2");
                log.Info(message);
            }
        }

        public static void LogError(string message, Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                XmlConfigurator.Configure();
                log.Error(message, ex);
            }
        }
    }
}