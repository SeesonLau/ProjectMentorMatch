using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch
{
    internal class Account
    {
        private string userID;
        private string fullname;
        private string email;
        private string password;
        public Account(string userID, string fullname, string email, string password)
        {
            this.userID = userID;
            this.fullname = fullname;
            this.email = email;
            this.password = password;
        }
        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        public string Fullname
        {
            get { return fullname; }
            set { fullname = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
