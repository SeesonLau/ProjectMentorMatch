using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch.Models
{
    public class Chat : Account
    {
        private int messageID = RandomID.messageID(); //serves like a channel
        private string? messageText;
        private int userIDSender;
        private int userIDReceiver;

        public int GetMessageID()
        {
            return messageID;
        }
        public string? GetMessageText()
        {
            return messageText;
        }
        public int GetUserIDSender()
        {
            return userIDSender;
        }
        public int GetUserIDReceiver()
        {
            return userIDReceiver;
        }
        public void SetMessageText(string messageText)
        {
            this.messageText = messageText;
        }
    }
}
