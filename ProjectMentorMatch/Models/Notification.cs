using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch.Models
{
    public class Notification : Account
    {
        private int notificationID;
        private string? notificationText; //notification text needs to be automatic. Make resources for the default texts that is frequently used.
        private int userID;

        public int GetNotificationID()
        {
            return notificationID;
        }
        public string? GetNotificationText()
        {
            return notificationText;
        }

    }
}
