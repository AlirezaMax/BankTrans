using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;

namespace BankTr
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /*private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            logger.Debug(ex.Message.ToString() + Environment.NewLine + DateTime.Now);
            NLog.LogManager.Flush();
            HttpContext.Current.ClearError();
            //Response.Redirect("~/Error/Index", false);
            Response.Redirect("~/Home/Index", false);
        }*/
    }
}
