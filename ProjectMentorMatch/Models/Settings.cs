using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch.Models
{
    public class Settings : ProfileInformation
    {
        private int password;
        private string? notification; // notification may be uncessary for setting. if so, then use either string or bool.
        private string? email;
        private string? aboutMe;

       /* public int GetPassword()
        {
            return password;
        }*/
        public string? GetNotificaiton()
        {
            return notification;
        }
        public string? GetChangeEmail()
        {
            return email;
        }
        public string? GetChangeAboutMe()
        {
            return aboutMe;
        }

        public void SetPassword(int password)
        {
            this.password = password;
        }
        /*public void SetEmail(string email)
        {
            this.email = email;
        }*/
        //public void SetAboutMe(string aboutMe)

    }
}
