using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Maui.Chat;

namespace ProjectMentorMatch.ViewModels
{
    internal class ChatViewModel
    {
        public Author CurrentUser { get; set; }
        public Author Receiver { get; set; }
        public ObservableCollection<object> Messages { get; set; }

        public ChatViewModel(string ReceiverName)
        {
            Messages = new ObservableCollection<object>();
            CurrentUser = new Author { Name = "Charles", Avatar = "chat_placeholder.png" }; // change to logged in user
            Receiver = new Author { Name = ReceiverName, Avatar = "chat_placeholder.png" };
            GenerateMessages();
        }

        private void GenerateMessages()
        {
            Messages.Add(new TextMessage
            {
                Author = CurrentUser,
                Text = "Hi guys, good morning! I'm very delighted to share with you the news that our team is going to go abroad. "
            });

            Messages.Add(new TextMessage
            {
                Author = Receiver,
                Text = "Ohh wow, that's amazing! "
            });
        }
    }
}
