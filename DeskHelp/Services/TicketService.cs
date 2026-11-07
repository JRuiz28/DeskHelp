using DeskHelp.DataSets.HelpDeskDataSetTableAdapters;
using DeskHelp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeskHelp.Services
{
    public class TicketService
    {
        private ticketTableAdapter ticketAdapter;
        private stateTicketTableAdapter stateAdapter;

        public TicketService()
        {
            ticketAdapter = new ticketTableAdapter();
            stateAdapter = new stateTicketTableAdapter();
        }

        //---------------- USER ROLE -----------------
        public List<TicketModel> GetTicketsByUser(int userID)
        {
            var ticketsData = ticketAdapter.GetDataByUserID(userID);

            List<TicketModel> tickets = new List<TicketModel>();

            foreach (var ticket in ticketsData)
            {
                tickets.Add(new TicketModel
                {
                    ID = ticket.pk_ticket_id,
                    Title = ticket.title,
                    Description = ticket.description,
                    UserID = ticket.fk_user_id_creator,
                    AssignedUserID = ticket.Isfk_user_id_assignedNull() ? (int?) null  : ticket.fk_user_id_assigned,
                    StateID = ticket.fk_state_id,
                    Category = ticket.category,
                    CreatedIn = ticket.createdIn
                });
            }

            return tickets;
        }

        public void CreateTicket(TicketModel ticket)
        {
            ticketAdapter.InsertTicket(ticket.Title, ticket.Description, ticket.UserID, ticket.Category);
        }


        //Function for rename join state
        public List<StateTicketModel> GetAllStates()
        {
            var statesData = stateAdapter.GetData();

            List<StateTicketModel> states = new List<StateTicketModel>();

            foreach (var state in statesData)
            {
                states.Add(new StateTicketModel
                {
                    ID = state.pk_state_id,
                    Name = state.name
                });
            }

            return states;
        }

        //---------------- ADMIN ROLE -----------------
        public List<TicketModel> GetAllTickets()
        {
            var ticketsData = ticketAdapter.GetAllTickets();

            List<TicketModel> tickets = new List<TicketModel>();

            foreach (var ticket in ticketsData)
            {
                tickets.Add(new TicketModel
                {
                    ID = ticket.pk_ticket_id,
                    Title = ticket.title,
                    Description = ticket.description,
                    UserID = ticket.fk_user_id_creator,
                    AssignedUserID = ticket.Isfk_user_id_assignedNull() ? (int?)null : ticket.fk_user_id_assigned,
                    StateID = ticket.fk_state_id,
                    Category = ticket.category,
                    CreatedIn = ticket.createdIn
                });
            }

            return tickets;
        }

        public void UpdateTicketState(int ticketID, int stateID)
        {
            ticketAdapter.UpdateTicketState(stateID, ticketID);
        }

        public void UpdateTicketAssignment(int ticketID, int stateID, int? assignedUserID)
        {
            ticketAdapter.UpdateTicketAssignment(stateID, assignedUserID, ticketID);
        }
    }
}