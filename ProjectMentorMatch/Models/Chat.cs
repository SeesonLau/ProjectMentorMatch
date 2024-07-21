using Microsoft.Data.SqlClient;
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
        private string? senderName;
        private string receiverName;
        private int userIDSender;
        private int userIDReceiver;
        private DateTime timeSent { get; set; } //Will add more fields as needed


        public DateTime GetTimeSent() {  return timeSent; }

        public string? GetSenderName() { return senderName; }

        public string? GetReceiverName() { return receiverName; }   
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


        public void LoadMessages(int userID)
        {

            string query = "SELECT messageText, userID_sender, userID_receiver FROM Chat WHERE messageID = @messageID";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // command.Parameters.AddWithValue("@UserID", userID);


                connection.Open();
                command.ExecuteNonQuery();
            }

        }

    }
}
