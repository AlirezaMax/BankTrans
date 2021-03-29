using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using BankTr.Models;
using BankTr.Core.Domain;
using BankTr.ViewModels;
using BankTr.Models.Logging;

namespace BankTr.Controllers
{
    public class TransController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            /*using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                var trans = unitOfWork.Trans.GetAllTrans();
                var stat6 = new ApplicationDbContext();
                var sta7 = stat6.States.GetAll();
                return View("TrListView", trans);
            }*/

            var statesTr = _context.States.ToList();

            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                var trans = unitOfWork.Trans.GetAllTrans();
                //var stat6 = new ApplicationDbContext();
                var sta7 = unitOfWork.Trans.GetAll();
                
            }

            var viewModel = new NewTranViewModel
            {
                States = statesTr
            };
            
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Tran tran)
        {
            _context.Trans.Add(tran);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteForm()
        {
            var tran = new Tran();

            return View("GetTranIdView", tran);
        }

        [HttpDelete]
        public ActionResult Delete(Tran tran)
        {
            int tranId = tran.Id;
            var Logger = NLog.LogManager.GetCurrentClassLogger();
            try
            {
                //...
                /*ActionResult trans = Details(tranId);
                if (trans == HttpNotFound())
                    return new HttpStatusCodeResult(404);*/

                //using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
                //{
                //    var tran2 = unitOfWork.Trans.FindTranById(tranId);                
                //throw new ApplicationException();
                var tran2 = _context.Trans.SingleOrDefault(c => c.Id == tran.Id); 
                if (tran2 != null)
                {
                    _context.Trans.Remove(tran2);
                    _context.SaveChanges();

                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
                }
                else
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound,
                        "We did not find that action with requested parameters");
                }
            }
            catch (Exception ex)
            {                
                Logger.Error(ex);
                return new HttpStatusCodeResult(500);
            }
        }

        public ActionResult FindTrById()
        {
            var Logger = NLog.LogManager.GetCurrentClassLogger();
            try
            {
                Logger.Info("Test Logger.Info in ActionResult FindTrById");
                throw new ApplicationException();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Test Logger.Error in ActionResult FindTrById");
            }

            try
            {
                var tran2 = _context.Trans.SingleOrDefault(c => c.Id == 15);
                if (tran2 != null)
                {
                    return View("OneTranView", tran2);
                    //return new HttpStatusCodeResult(200);
                }
                else
                {
                    return new HttpStatusCodeResult(404);
                }                
            }
            catch (Exception ex)
            {
                //log ex
                Logger.Error(ex);
                return new HttpStatusCodeResult(500);
            }                        
        }

        public ActionResult FindTr()
        {
            var statesTr = _context.States.ToList();
            var viewModel = new FindTranViewModel
            {
                States = statesTr
            };

            return View(viewModel);
        }

        //GET, POST, PUT, DELETE, PATCH, OPTIONS, ...
                
        public ViewResult FindResult(FindTranViewModel viewModel)
        {
            int stId = viewModel.Tran.StateId;
            float minAmount = viewModel.MinAmount;
            float maxAmount = viewModel.MaxAmount;

            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                var searchResult = unitOfWork.Trans.FindTrans(stId, minAmount, maxAmount);
                return View("TrListView", searchResult);
            }
        }

        public ViewResult AllTrans()
        {
            //var trans = _context.Trans.ToList();
            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext())) {
                var trans = unitOfWork.Trans.GetAllTrans();

            return View("TrListView", trans);
            }
        }

        public ViewResult MaxTransAmounts()
        {
            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {                
                var tranMaxInState = unitOfWork.Trans.GetMaxListInAllStates();
                return View("TranSumOrMaxView", tranMaxInState);
            }
        }

        public ViewResult SumTransAmounts()
        {
            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                var tranSumInState = unitOfWork.Trans.GetSumListInAllStates();
                return View("TranSumOrMaxView", tranSumInState);
            }
        }

        public ViewResult GetSecondTop3TransPerState()
        {
            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                var trans = unitOfWork.Trans.GetTopTransAllState(3, 6);
                return View("TranSumOrMaxView", trans);
            }
        }

        public ActionResult Details(int id)
        {
            var tran = _context.Trans.SingleOrDefault(c => c.Id == id);
            //var tran = _context.Trans.Include(c => c.Customer).SingleOrDefault(c => c.Id == id);
            if (tran == null)
                return HttpNotFound();

            return View(tran);
        }


    }
}