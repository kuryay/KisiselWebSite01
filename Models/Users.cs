using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog001.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Users()
        {

        }

        public Users(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

    }
}
