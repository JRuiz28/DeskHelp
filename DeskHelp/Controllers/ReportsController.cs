using DeskHelp.Models;
using DeskHelp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeskHelp.Controllers
{
    public class ReportsController : Controller
    {
        private ReportService reportService = new ReportService();
        private TicketService ticketService = new TicketService();

        public ActionResult Index()
        {
            if (ValidateUser(true) != null) return ValidateUser(true);

            ViewBag.States = ticketService.GetAllStates();

            return View();
        }

        [HttpPost]
        public ActionResult Index(DateTime startDate, DateTime endDate)
        {
            try
            {
                if (ValidateUser(true) != null) return ValidateUser(true);

                if (startDate > endDate)
                {
                    ViewBag.Error = "La fecha inicial no puede ser mayor que la fecha final.";
                    return View();
                }

                List<ReportModel> report = reportService.GetReport(startDate, endDate.AddDays(1));
                ViewBag.States = ticketService.GetAllStates();
                ViewBag.StartDate = startDate.ToString("yyyy-MM-dd");
                ViewBag.EndDate = endDate.ToString("yyyy-MM-dd");

                return View(report);
            }
            catch
            {
                return View();
            }
        }


        private ActionResult ValidateUser(bool isAdmin)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            UserModel user = Session["User"] as UserModel;
            if (isAdmin && user.Role != "Administrador")
            {
                return RedirectToAction("Index", "Home");
            }

            return null;
        }
    }
}