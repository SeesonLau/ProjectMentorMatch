using ProjectMentorMatch.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace ProjectMentorMatch.Views
{
    public partial class ChatSpecific : ContentPage
    {
        private readonly HubConnection hubConnection;
        private bool isSendingMessage = false;

        public ChatSpecific(string ReceiverName)
        {
            InitializeComponent();
            BindingContext = new ChatViewModel(ReceiverName);

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
                    AddMessageToChat(user, message, user == txtUsername.Text);
                });
            });

            Task.Run(async () =>
            {
                await hubConnection.StartAsync();
            });
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
                TextColor = Colors.Gray,
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
                BackgroundColor = isCurrentUser ? Colors.LightBlue : Colors.LightGray,
                CornerRadius = 15,
                Padding = new Thickness(10),
                Margin = new Thickness(0, 5),
                HorizontalOptions = isCurrentUser ? LayoutOptions.End : LayoutOptions.Start
            };

            ChatMessagesStack.Children.Add(messageFrame);
        }
    }
}
