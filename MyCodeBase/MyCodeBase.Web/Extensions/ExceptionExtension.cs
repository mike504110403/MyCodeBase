using MyCodeBase.Library.Extensions;

using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyCodeBase.Web.Extensions
{
    /// <summary>
    /// 例外狀態處理-擴充方法
    /// </summary>
    public static class ExceptionExtension
    {
        /// <summary>
        /// 忽略 的例外狀態
        /// </summary>
        public static int[] PassExceptionHttpCode = new int[] { (int)HttpStatusCode.NotFound, (int)HttpStatusCode.RequestTimeout };
        /// <summary>
        /// 例外狀態 檢視用模型
        /// </summary>
        public class ExceptionLogViewModel
        {
            public string Message { get; set; }
            public string Url { get; set; }
            public string ControllerName { get; set; }
            public string ActionName { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        private static readonly string RequestUrl = $"{HttpContext.Current?.Request.Url.Scheme}://{HttpContext.Current?.Request.Url.Authority}{HttpContext.Current?.Request.ApplicationPath}".Trim('/');

        /// <summary>
        /// 取得所有ExceptionMessage
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string ErrorMessage(this Exception ex)
        {
            return ExceptionMessageIntegration(ex);
        }

        /// <summary>
        /// Exception寫入log
        /// </summary>
        /// <param name="exception"></param>
        public static void ExceptionLogRun(this Exception exception)
        {
            var viewModel = new ExceptionLogViewModel
            {
                Message = ExceptionMessageIntegration(exception),
                Url = HttpContext.Current.Request.Url.OriginalString
            };
            viewModel.Info();
        }

        /// <summary>
        /// ExceptionContext寫入log
        /// </summary>
        /// <param name="exceptionContext"></param>
        public static void ExceptionLogRun(this ExceptionContext exceptionContext)
        {
            var viewModel = new ExceptionLogViewModel
            {
                Message = ExceptionMessageIntegration(exceptionContext.Exception),
                Url = exceptionContext.RequestContext?.HttpContext.Request.RawUrl
            };
            viewModel.Info();
        }

        /// <summary>
        /// 當系統發生Exception後導向的Url
        /// </summary>
        /// <returns></returns>
        public static string RedirectUrl()
        {
            return $"{RequestUrl}?msg=系統執行過程中發生錯誤！";
        }
        /// <summary>
        /// 取得 ModelState 驗證失敗訊息
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static string GetErrorMessage(this ModelStateDictionary modelState)
        {
            if (modelState.IsValid)
            {
                return string.Empty;
            }
            return "驗證失敗:\r\n" + string.Join("\r\n", modelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));

        }

        /// <summary>
        /// 整合Exception Type
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="isInnerException"></param>
        /// <returns></returns>
        private static string ExceptionMessageIntegration(Exception exception, bool isInnerException = false)
        {
            var innerExceptionMessage = string.Empty;

            if (exception.InnerException != null)
            {
                innerExceptionMessage = ExceptionMessageIntegration(exception.InnerException, true);
            }

            var inner = isInnerException ? "InnerException" : string.Empty;
            string result;

            if (exception is DbEntityValidationException dbEntityValidationException)
            {
                var _ = dbEntityValidationException.EntityValidationErrors
                                                   .SelectMany(x => x.ValidationErrors)
                                                   .Select(x => x.ErrorMessage)
                                                   .Aggregate((strBuilder, msg) => strBuilder + ";" + msg);

                result = string.Concat($"[{inner} DbEntityValidationException Message：", exception.Message,
                                       Environment.NewLine, " 驗證錯誤: ", _,
                                       Environment.NewLine, exception.StackTrace, "]")
                               .Trim();
            }
            else
            {
                result = string.Concat($"[{inner} Message：", exception.Message, Environment.NewLine, exception.StackTrace, "]");
            }

            return innerExceptionMessage + Environment.NewLine + result;
        }
    }
}