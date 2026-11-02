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

        public TicketService()
        {
            ticketAdapter = new ticketTableAdapter();
        }

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
    }
}