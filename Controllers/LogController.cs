using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using NLog;
using BankTr.Models.Logging;
using System.Text.RegularExpressions;

namespace BankTr.Controllers
{
    public class LogController : Controller
    {

        // private static Logger logger = LogManager.GetCurrentClassLogger();

        // GET: Home
        public ActionResult Index()
        {
            try
            {
                string studentName = "Vaibhav123";
                ValidateStudent(studentName);
                return View();
            }


            catch (Exception)
            {
                //throw ex;
                
                //var config = new NLog.Config.LoggingConfiguration();
                //var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "LogTest2.txt" };
                //config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
                //NLog.LogManager.Configuration = config;
                var Logger = NLog.LogManager.GetCurrentClassLogger();

                try
                {
                    Logger.Info("Test Logger.Info in ActionResult Index-LogController");
                    //System.Console.ReadKey();
                    //Log.Instance.Debug("We're going to throw an exception now.");
                    //Log.Instance.Warn("It's gonna happen!!");
                    throw new ApplicationException();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "Test Logger.Error in ActionResult Index-LogController");
                    //Log.Instance.Error(ex, "Error doing something...");
                    ViewBag.Message = ex.Message;
                    return View("Error");
                }
            }
        }
        private static void ValidateStudent(string studentName)
        {
            Regex regex = new Regex("^[a-zA-Z]+$");
            if (!regex.IsMatch(studentName))
                throw new Exception(studentName);

        }
    }
}