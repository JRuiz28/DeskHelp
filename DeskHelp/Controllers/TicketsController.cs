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
        private UserService userService = new UserService();
        private CommentService commentService = new CommentService();

        //---------------- USER ROLE -----------------
        public ActionResult Index()
        {
            if (ValidateUser(false) != null) return ValidateUser(false);

            UserModel user = Session["User"] as UserModel;
            List<TicketModel> tickets = ticketService.GetTicketsByUser(user.ID);

            ViewBag.States = ticketService.GetAllStates();

            return View(tickets);
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (ValidateUser(false) != null) return ValidateUser(false); 

            return View();
        }

        [HttpPost]
        public ActionResult Create(TicketModel ticket)
        {
            try
            {
                if (ValidateUser(false) != null) return ValidateUser(false);

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


        //---------------- ADMIN ROLE -----------------
        public ActionResult AdminIndex()
        {
            if (ValidateUser(true) != null) return ValidateUser(true);

            var tickets = ticketService.GetAllTickets();

            return View(tickets);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                if (ValidateUser(true) != null) return ValidateUser(true);

                var tickets = ticketService.GetAllTickets();
                var ticket = tickets.FirstOrDefault(t => t.ID == id);

                ViewBag.States = ticketService.GetAllStates();
                ViewBag.Responsibles = userService.GetUsersByRole("Administrador");

                return View(ticket);
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Edit(TicketModel ticket)
        {
            try
            {
                if (ValidateUser(true) != null) return ValidateUser(true);

                ticketService.UpdateTicketAssignment(
                    ticket.ID,
                    ticket.StateID,
                    ticket.AssignedUserID
                );

                return RedirectToAction("Details", new { id = ticket.ID });
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Search()
        {
            if (ValidateUser(true) != null) return ValidateUser(true);

            ViewBag.States = ticketService.GetAllStates();

            return View();
        }

        [HttpGet]
        public ActionResult SearchTickets(int ticketID = 0, string name = "", int stateID = 0)
        {
            if (ValidateUser(true) != null) return ValidateUser(true);

            var tickets = ticketService.SearchTickets(ticketID, name, stateID);

            return PartialView("_TicketSearchResults", tickets);
        }

        // -----------------------------------------------------

        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                if (ValidateUser(false) != null) return ValidateUser(false);

                List<CommentModel> comments = commentService.GetCommentsByTicketID(id);
                List<StateTicketModel> states = ticketService.GetAllStates();
                ViewBag.Comments = comments;
                ViewBag.States = states;

                UserModel user = Session["User"] as UserModel;
                List<TicketModel> tickets;

                if (user.Role == "Administrador")
                {
                    tickets = ticketService.GetAllTickets();
                }
                else
                {
                    tickets = ticketService.GetTicketsByUser(user.ID);
                }

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
                if (ValidateUser(false) != null) return ValidateUser(false);

                UserModel user = Session["User"] as UserModel;
                List<TicketModel> tickets;

                if (user.Role == "Administrador")
                {
                    tickets = ticketService.GetAllTickets();
                }
                else
                {
                    tickets = ticketService.GetTicketsByUser(user.ID);
                }

                TicketModel ticket = tickets.FirstOrDefault(t => t.ID == ticketID);
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