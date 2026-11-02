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
        private CommentService commentService = new CommentService();

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

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Account");
            } 

            return View();
        }

        [HttpPost]
        public ActionResult Create(TicketModel ticket)
        {
            try
            {
                if (Session["User"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                UserModel user = Session["User"] as UserModel;

                ticket.UserID = user.ID;

                ticketService.CreateTicket(ticket);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                if (Session["User"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var comments = commentService.GetCommentsByTicketID(id);

                var states = ticketService.GetAllStates();

                ViewBag.Comments = comments;

                ViewBag.States = states;

                UserModel user = Session["User"] as UserModel;

                var tickets = ticketService.GetTicketsByUser(user.ID);

                var ticket = tickets.FirstOrDefault(t => t.ID == id);

                return View(ticket);
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult AddComment(int ticketID, string text)
        {
            try
            {
                if (Session["User"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                UserModel user = Session["User"] as UserModel;
                
                var tickets = ticketService.GetTicketsByUser(user.ID);

                var ticket = tickets.FirstOrDefault(t => t.ID == ticketID);

                if (ticket == null)
                {
                    return RedirectToAction("Index");
                }

                CommentModel comment = new CommentModel
                {
                    TicketID = ticketID,
                    UserID = user.ID,
                    UserName = user.Name,
                    Text = text
                };

                commentService.CreateComment(comment);

                return RedirectToAction("Details", new { id = ticketID });
            }
            catch
            {
                return View();
            }
        }
    }
}