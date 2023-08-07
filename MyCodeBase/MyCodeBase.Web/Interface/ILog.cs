using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCodeBase.Web.Interface
{
    /// <summary>
    /// Log介面
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Traces 訊息
        /// </summary>
        /// <param name="message"></param>
        void Trace(string message);
        /// <summary>
        /// Traces dump物件內容
        /// </summary>
        /// <param name="outputObject"></param>
        void Trace(object outputObject);
        /// <summary>
        /// Traces 訊息加上format的參數
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Trace(string message, params object[] args);
        /// <summary>
        /// Traces 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="outputObject"></param>
        void Trace(string message, object outputObject);
        /// <summary>
        /// Debug 訊息
        /// </summary>
        /// <param name="message"></param>
        void Debug(string message);
        /// <summary>
        /// Debug dump物件內容
        /// </summary>
        /// <param name="outputObject"></param>
        void Debug(object outputObject);
        /// <summary>
        /// Traces 訊息加上format的參數
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Debug(string message, params object[] args);
        /// <summary>
        /// Debug 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="outputObject"></param>
        void Debug(string message, object outputObject);
        /// <summary>
        /// Info 訊息
        /// </summary>
        /// <param name="message"></param>
        void Info(string message);
        /// <summary>
        /// Info dump物件內容
        /// </summary>
        /// <param name="outputObject"></param>
        void Info(object outputObject);
        /// <summary>
        /// Info 訊息加上format的參數
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Info(string message, params object[] args);
        /// <summary>
        /// Info 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="outputObject"></param>
        void Info(string message, object outputObject);
        /// <summary>
        /// Warn 訊息
        /// </summary>
        /// <param name="message"></param>
        void Warn(string message);
        /// <summary>
        /// Warn dump物件內容
        /// </summary>
        /// <param name="outputObject"></param>
        void Warn(object outputObject);
        /// <summary>
        /// Warn 訊息加上format的參數
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Warn(string message, params object[] args);
        /// <summary>
        /// Warn 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="outputObject"></param>
        void Warn(string message, object outputObject);
        /// <summary>
        /// Error 訊息
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);
        /// <summary>
        /// Error dump物件內容
        /// </summary>
        /// <param name="outputObject"></param>
        void Error(object outputObject);
        /// <summary>
        /// Error 訊息加上format的參數
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Error(string message, params object[] args);
        /// <summary>
        /// Error 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="outputObject"></param>
        void Error(string message, object outputObject);
        /// <summary>
        /// Fatal 訊息
        /// </summary>
        /// <param name="message"></param>
        void Fatal(string message);
        /// <summary>
        /// Fatal dump物件內容
        /// </summary>
        /// <param name="outputObject"></param>
        void Fatal(object outputObject);
        /// <summary>
        /// Fatal 訊息加上format的參數
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Fatal(string message, params object[] args);
        /// <summary>
        /// Fatal 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="outputObject"></param>
        void Fatal(string message, object outputObject);
    }
}
