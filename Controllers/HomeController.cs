using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;

namespace BankTr.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            
            var Logger = NLog.LogManager.GetCurrentClassLogger();
            try
            {
                Logger.Info("Test Logger.Info in ActionResult About");
                throw new ApplicationException();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Test Logger.Error in ActionResult About");                
            }

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}