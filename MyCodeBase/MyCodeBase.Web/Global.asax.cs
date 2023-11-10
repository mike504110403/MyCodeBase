using MyCodeBase.Library.Extensions;
using MyCodeBase.Web.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MyCodeBase.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // 全域註冊
            GlobalFilters.Filters.Add(new Filters.NLogFilters.ActionLogFilter());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();

            if (exception is HttpException httpException && ExceptionExtension.PassExceptionHttpCode.Contains(httpException.GetHttpCode()))
            {
                return;
            }
            if (exception is HttpAntiForgeryException)
            {
                return;
            }

            exception.ExceptionLogRun();

            //排除Ajax錯誤
            var isAjaxCall = new HttpRequestWrapper(Context.Request).IsAjaxRequest();
            if (isAjaxCall)
            {
                return;
            }
            // 回導首頁、提示訊息
            //Response.SetrCookice(AlertHelper.AlertsExceptionKey, "系統執行過程中發生錯誤！", DateTime.Now.AddMinutes(5));
            //Response.Redirect(ExceptionExtention.RedirectUrl());
        }
    }
}
