using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeskHelp.Models
{
    public class TicketModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
        public int? AssignedUserID { get; set; }
        public int StateID { get; set; }
        public string Category { get; set; }
        public DateTime CreatedIn { get; set; }

        public TicketModel()
        {
            Title = "";
            Description = "";
            UserID = 0;
            StateID = 0;
            Category = "";
            CreatedIn = DateTime.Now;
        }

        public TicketModel(int id, string title, string description, int userID, int? assignedUserID, int stateID, string category)
        {
            this.ID = id;
            this.Title = title;
            this.Description = description;
            this.UserID = userID;
            this.AssignedUserID = assignedUserID;
            this.StateID = stateID;
            this.Category = category;
            CreatedIn = DateTime.Now;
        }
    }
}