using ProjectMentorMatch.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectMentorMatch.Views
{
    public partial class ChatSpecific : ContentPage
    {
        private readonly HubConnection hubConnection;
        private bool isSendingMessage = false;

        public ChatSpecific()
        {
            InitializeComponent();
           
            var baseUrl = "http://localhost";

            // Android can't connect to localhost
            if (DeviceInfo.Current.Platform == DevicePlatform.Android)
            {
                baseUrl = "http://10.0.2.2";
            }

            hubConnection = new HubConnectionBuilder()
                .WithUrl($"{baseUrl}:5127/chatHub")
                .Build();

            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Dispatcher.Dispatch(() =>
                {
                    // Ensure the message is only added when received from others
                    if (user != txtUsername.Text)
                    {
                        AddMessageToChat(user, message, false);
                    }
                });
            });

            Task.Run(async () =>
            {
                await hubConnection.StartAsync();
            });

            // Add predefined messages
            InitializePredefinedMessages();
        }

        private void InitializePredefinedMessages()
        {
            var predefinedMessages = new List<(string user, string message, bool isCurrentUser)>
            {

                ("Charles", "Hey.", true),
                ("Charles", "Would you be available for a tutoring schedule about Mathematics next week.", true),
                ("Jamel", "Hello, yes I am, I just got your booking notification.", false),
               
            };

            foreach (var (user, message, isCurrentUser) in predefinedMessages)
            {
                AddMessageToChat(user, message, isCurrentUser);
            }
        }

        private async void btnSend_Clicked(object sender, EventArgs e)
        {
            if (isSendingMessage)
                return;

            isSendingMessage = true;

            try
            {
                await hubConnection.InvokeCoreAsync("SendMessageToAll", args: new[]
                {
                    txtUsername.Text,
                    txtMessage.Text
                });

                // Add the message to the chat immediately for the sender
                AddMessageToChat(txtUsername.Text, txtMessage.Text, true);
                txtMessage.Text = string.Empty;
            }
            finally
            {
                isSendingMessage = false;
            }
        }

        private void AddMessageToChat(string user, string message, bool isCurrentUser)
        {
            var messageLabel = new Label
            {
                Text = message,
                TextColor = Colors.Black
            };

            var userLabel = new Label
            {
                Text = user,
                FontSize = 10,
                TextColor = Colors.White,
                HorizontalOptions = LayoutOptions.Start
            };

            var messageStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 5,
                Children = { userLabel, messageLabel }
            };

            var messageFrame = new Frame
            {
                Content = messageStack,
                BackgroundColor = isCurrentUser ? Colors.Gold : Colors.CornflowerBlue,
                CornerRadius = 15,
                Padding = new Thickness(10),
                Margin = new Thickness(0, 5),
                HorizontalOptions = isCurrentUser ? LayoutOptions.End : LayoutOptions.Start
            };

            ChatMessagesStack.Children.Add(messageFrame);
        }
    }
}
