using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeskHelp.Services;
using DeskHelp.Models;

namespace DeskHelp.Controllers
{
    public class TicketsController :  Controller
    {
        private TicketService ticketService = new TicketService();

        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            UserModel user = Session["User"] as UserModel;

            var tickets = ticketService.GetTicketsByUser(user.ID);

            return View(tickets);
        }
    }
}