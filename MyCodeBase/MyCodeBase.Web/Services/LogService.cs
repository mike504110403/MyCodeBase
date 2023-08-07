using MyCodeBase.Web.Interface;

using Newtonsoft.Json;

using NLog;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCodeBase.Web.Services
{
    /// <summary>
    /// 實作Log
    /// </summary>
    public class LogService : ILog
    {
        private Logger logger;

        public LogService()
        {
            logger = NLog.LogManager.GetCurrentClassLogger();
        }

        public LogService(string name)
        {
            logger = NLog.LogManager.GetLogger(name);
        }

        #region Debug
        public void Debug(string message)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug(message);
            }
        }

        public void Debug(object outputObject)
        {
            Debug(JsonConvert.SerializeObject(outputObject, Formatting.Indented)); // 縮排方式輸出json格式
        }

        public void Debug(string message, params object[] args)
        {
            Debug(string.Format(message, args));
        }

        public void Debug(string message, object outputObject)
        {
            Debug(message + Environment.NewLine +
                JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }
        #endregion
        #region Error
        public void Error(string message)
        {
            if (logger.IsErrorEnabled)
            {
                logger.Error(message);
            }
        }

        public void Error(object outputObject)
        {
            Error(JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        public void Error(string message, params object[] args)
        {
            Error(string.Format(message, args));
        }

        public void Error(string message, object outputObject)
        {
            Error(message + Environment.NewLine +
                JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }
        #endregion
        #region Fatal
        public void Fatal(string message)
        {
            if (logger.IsFatalEnabled)
            {
                logger.Fatal(message);
            }
        }

        public void Fatal(object outputObject)
        {
            Fatal(JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        public void Fatal(string message, params object[] args)
        {
            Fatal(string.Format(message, args));
        }

        public void Fatal(string message, object outputObject)
        {
            Fatal(message + Environment.NewLine +
                JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }
        #endregion
        #region Info
        public void Info(string message)
        {
            if (logger.IsInfoEnabled)
            {
                logger.Info(message);
            }
        }

        public void Info(object outputObject)
        {
            Info(JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        public void Info(string message, params object[] args)
        {
            Info(string.Format(message, args));
        }

        public void Info(string message, object outputObject)
        {
            Info(message + Environment.NewLine +
                JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }
        #endregion
        #region Trace
        public void Trace(string message)
        {
            if (logger.IsTraceEnabled)
            {
                logger.Trace(message);
            }
        }

        public void Trace(object outputObject)
        {
            Trace(JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        public void Trace(string message, params object[] args)
        {
            Trace(string.Format(message, args));
        }

        public void Trace(string message, object outputObject)
        {
            Trace(message + Environment.NewLine +
                JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }
        #endregion
        #region Warn
        public void Warn(string message)
        {
            if (logger.IsWarnEnabled)
            {
                logger.Warn(message);
            }
        }

        public void Warn(object outputObject)
        {
            Warn(JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        public void Warn(string message, params object[] args)
        {
            Warn(string.Format(message, args));
        }

        public void Warn(string message, object outputObject)
        {
            Warn(message + Environment.NewLine +
                JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }
        #endregion
    }
}