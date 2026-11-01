using DeskHelp.DataSets.HelpDeskDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeskHelp.Services
{
    public class UserService
    {
        private userTableAdapter userAdapter;

        public UserService()
        {
            userAdapter = new userTableAdapter();
        }

        public bool ValidateUser(string email, string password)
        {
            var users = userAdapter.GetDataByEmail(email);

            if (users.Count == 0)
            {
                return false;
            }

            var user = users[0];

            return user.password == password;
        }
    }
}