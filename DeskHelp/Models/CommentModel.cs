using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeskHelp.Models
{
    public class CommentModel
    {
        public int ID { get; set; }
        public int TicketID { get; set; }
        public int UserID { get; set; }
        public string Text { get; set; }
        public DateTime CreatedIn { get; set; }
        public string UserName { get; set; }

        public CommentModel()
        {
            TicketID = 0;
            UserID = 0;
            Text = "";
            CreatedIn = DateTime.Now;
            UserName = "";
        }

        public CommentModel(int id, int ticketID, int userID, string text, string userName)
        {
            this.ID = id;
            this.TicketID = ticketID;
            this.UserID = userID;
            this.Text = text;
            this.UserName = userName;
            CreatedIn = DateTime.Now;
        }
    }
}