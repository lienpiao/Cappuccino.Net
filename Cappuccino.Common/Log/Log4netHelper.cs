using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.Common.Log
{
    public static class Log4netHelper
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("LogHelper");
        public static void Debug(string message)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug(message);
            }
        }
        public static void Debug(Object message, Exception exception)
        {
            if (log.IsErrorEnabled)
            {
                log.Debug(message, exception);
            }
        }
        public static void Error(string message)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
        }
        public static void Error(Object message, Exception exception)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(message, exception);
            }
        }
        public static void Fatal(string message)
        {
            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
            }
        }
        public static void Fatal(Object message, Exception exception)
        {
            if (log.IsFatalEnabled)
            {
                log.Fatal(message, exception);
            }
        }
        public static void Info(string message)
        {
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }
        }
        public static void Info(Object message, Exception exception)
        {

            log.Info(message, exception);
        }
        public static void Warn(string message)
        {
            if (log.IsWarnEnabled)
            {
                log.Warn(message);
            }
        }
        public static void Warn(Object message, Exception exception)
        {
            log.Warn(message, exception);
        }
    }
}