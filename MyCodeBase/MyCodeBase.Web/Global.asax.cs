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
            // ������U
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

            //�ư�Ajax���~
            var isAjaxCall = new HttpRequestWrapper(Context.Request).IsAjaxRequest();
            if (isAjaxCall)
            {
                return;
            }
            // �^�ɭ����B���ܰT��
            //Response.SetrCookice(AlertHelper.AlertsExceptionKey, "�t�ΰ���L�{���o�Ϳ��~�I", DateTime.Now.AddMinutes(5));
            //Response.Redirect(ExceptionExtention.RedirectUrl());
        }
    }
}
