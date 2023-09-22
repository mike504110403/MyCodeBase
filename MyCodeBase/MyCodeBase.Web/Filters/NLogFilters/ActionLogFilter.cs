using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCodeBase.Web.Filters.NLogFilters
{
    public class ActionLogFilter : ActionFilterAttribute
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller.ControllerContext;
            var userName = controller.HttpContext.User.Identity.Name;
            var ip = controller.HttpContext.Request.UserHostAddress;
            var actionName = controller.RouteData.Values["action"];

            logger
              .WithProperty("Property1", "test1")
              .WithProperty("Property2", "test2")
              .Trace($"{userName} Request {actionName} Page");

            base.OnActionExecuting(filterContext);
        }
    }
}