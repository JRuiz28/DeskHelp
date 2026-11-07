using DeskHelp.DataSets.HelpDeskDataSetTableAdapters;
using DeskHelp.Models;
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

        public UserModel GetUserByEmail(string email)
        {
            var users = userAdapter.GetDataByEmail(email);

            if (users.Count == 0)
            {
                return null;
            }

            var user = users[0];

            return new UserModel
            {
                ID = user.pk_user_id,
                Name = user.name,
                Email = user.email,
                Role = user.role,
                State = user.State
            };
        }

        public List<UserModel> GetUsersByRole(string role)
        {
            var users = userAdapter.GetDataByRole(role);

            List<UserModel> usersList = new List<UserModel>();

            foreach (var user in users)
            {
                usersList.Add(new UserModel
                {
                    ID = user.pk_user_id,
                    Name = user.name
                });
            }

            return usersList;
        }
    }
}