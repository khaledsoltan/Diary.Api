using NLog;
using Shared.Helpers;
using LogLevel = NLog.LogLevel;

namespace Api.Diary.Extensions
{
    public static class NLogExtender
    {
        private static LogEventInfo logEventInfo;

        public static void LogError(this Logger logger, string message, string exception = null, string stackTrace = null, string targetSite = null, string controller = null,
            string action = null, string userName = null, string ip = null, string userAgent = null)
        {
            logEventInfo = new LogEventInfo
            {
                Level = LogLevel.Error
            };

            FireLogging(logger, message, exception, stackTrace, targetSite, controller, action, userName, ip, userAgent);
        }

        public static void LogWarning(this Logger logger, string message, string exception = null, string stackTrace = null, string targetSite = null,
            string controller = null, string action = null, string userName = null, string ip = null, string userAgent = null)
        {
            logEventInfo = new LogEventInfo
            {
                Level = LogLevel.Warn
            };

            FireLogging(logger, message, exception, stackTrace, targetSite, controller, action, userName, ip, userAgent);
        }

        public static void LogInfo(this Logger logger, string message, string exception = null, string stackTrace = null, string targetSite = null,
            string controller = null, string action = null, string userName = null, string ip = null, string userAgent = null)
        {
            logEventInfo = new LogEventInfo
            {
                Level = LogLevel.Info
            };

            FireLogging(logger, message, exception, stackTrace, targetSite, controller, action, userName, ip, userAgent);
        }

        private static void FireLogging(Logger logger,
            string message, string exception, string stackTrace, string targetSite,
            string controller, string action, string userName, string ip, string userAgent)
        {
            string emptyFlag = "-";

            logEventInfo.Properties.Add("Message", string.IsNullOrWhiteSpace(message) ? emptyFlag : SpecialCharReplace.Replacecomma(message));
            logEventInfo.Properties.Add("Exception", string.IsNullOrWhiteSpace(exception) ? emptyFlag : SpecialCharReplace.Replacecomma(exception));
            logEventInfo.Properties.Add("StackTrace", string.IsNullOrWhiteSpace(stackTrace) ? emptyFlag : SpecialCharReplace.Replacecomma(stackTrace));
            logEventInfo.Properties.Add("TargetSite", string.IsNullOrWhiteSpace(targetSite) ? emptyFlag : SpecialCharReplace.Replacecomma(targetSite));
            logEventInfo.Properties.Add("Controller", string.IsNullOrWhiteSpace(controller) ? emptyFlag : SpecialCharReplace.Replacecomma(controller));
            logEventInfo.Properties.Add("Action", string.IsNullOrWhiteSpace(action) ? emptyFlag : SpecialCharReplace.Replacecomma(action));
            logEventInfo.Properties.Add("UserName", string.IsNullOrWhiteSpace(userName) ? emptyFlag : SpecialCharReplace.Replacecomma(userName));
            logEventInfo.Properties.Add("IP", string.IsNullOrWhiteSpace(ip) ? emptyFlag : SpecialCharReplace.Replacecomma(ip));
            logEventInfo.Properties.Add("UserAgent", string.IsNullOrWhiteSpace(userName) ? emptyFlag : SpecialCharReplace.Replacecomma(userAgent));

            logger.Log(logEventInfo);
        }

        internal static void TraceLogger(Logger tracelogger, dynamic info)
        {
            LogEventInfo log = new LogEventInfo();
            log.Level = LogLevel.Trace;
            log.Properties.Add("UserId", SpecialCharReplace.Replacecomma(info.UserId));
            log.Properties.Add("UserName", SpecialCharReplace.Replacecomma(info.UserName));
            log.Properties.Add("parameter", SpecialCharReplace.Replacecomma(info.parameter));
            log.Properties.Add("ControllerName", SpecialCharReplace.Replacecomma(info.ControllerName));
            log.Properties.Add("ActioName", SpecialCharReplace.Replacecomma(info.ActioName));
            log.Properties.Add("BrowserVersion", SpecialCharReplace.Replacecomma(info.BrowserVersion));
            log.Properties.Add("Browser", SpecialCharReplace.Replacecomma(info.Browser));
            log.Properties.Add("Ip", SpecialCharReplace.Replacecomma(info.Ip));

            //logEventInfo.Properties.Add("UserAgent", userName.IsNullOrWhiteSpace() ? emptyFlag : userAgent);

            tracelogger.Trace(log);
        }
    }
}
