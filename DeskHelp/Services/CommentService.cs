using DeskHelp.DataSets.HelpDeskDataSetTableAdapters;
using DeskHelp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeskHelp.Services
{
    public class CommentService
    {
        private commentTableAdapter commentAdapter;

        public CommentService()
        {
            commentAdapter = new commentTableAdapter();
        }

        public List<CommentModel> GetCommentsByTicketID(int ticketID)
        {
            var commentsData = commentAdapter.GetCommentsByTicketID(ticketID);

            List<CommentModel> comments = new List<CommentModel>();

            foreach (var comment in commentsData)
            {
                comments.Add(new CommentModel
                {
                    ID = comment.pk_comment_id,
                    TicketID = comment.fk_ticket_id,
                    UserID = comment.fk_user_id,
                    Text = comment.text,
                    CreatedIn = comment.createdIn,
                    UserName = comment.userName
                });
            }

            return comments;
        }

        public void CreateComment(CommentModel comment)
        {
            commentAdapter.InsertComment(comment.TicketID, comment.UserID, comment.Text, comment.UserName);
        }
    }
}