using ProjectMentorMatch.ViewModels;
using Microsoft.Maui.Controls;
namespace ProjectMentorMatch.Views;
using Microsoft.AspNetCore.SignalR.Client;


public partial class ChatSpecific : ContentPage
{
     private readonly HubConnection hubConnection;
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
           // lblChat.Text += $"<b>{user}</b>: {message}<br/>";
        }); 

        Task.Run(() =>
        {
            Dispatcher.Dispatch(async () =>
            {
                await hubConnection.StartAsync();
            });
        });
    }
}