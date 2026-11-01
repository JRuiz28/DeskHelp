using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeskHelp.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        //public string Password { get; set; }
        public string Role { get; set; }
        public bool State { get; set; }
        public DateTime CreatedIn { get; set; }

        public UserModel()
        {
            Name = "";
            Email = "";
            //Password = "";
            Role = "";
            State = false;
            CreatedIn = DateTime.Now;
        }

        public UserModel(int id, string name, string email, string role, bool state)
        {
            this.ID = id;
            this.Name = name;
            this.Email = email;
            //this.Password = password;
            this.Role = role;
            this.State = state;
            CreatedIn = DateTime.Now;
        }
    }
}