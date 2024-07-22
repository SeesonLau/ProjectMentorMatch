using Microsoft.Maui.Controls;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectMentorMatch.Models;

namespace ProjectMentorMatch.Views
{
    public partial class Chat : ContentPage
    {
        public Chat()
        {
            InitializeComponent();
            LoadMentorsAndChatsAsync();
        }

        private async Task LoadMentorsAndChatsAsync()
        {
            var mentors = await FetchTopMentorsAsync();
            var chats = await FetchTopChatsAsync();

            BindMentorsToUI(mentors);
            BindChatsToUI(chats);
        }

        private async Task<List<string>> FetchTopMentorsAsync()
        {
            var mentors = new List<string>();
            string query = "SELECT TOP 5 ReceiverName FROM Chat2"; // Modify the query as per your database schema

            using (var connection = new SqlConnection("Server=tcp:mentor2.database.windows.net,1433;Initial Catalog=mentor-mentee2;Persist Security Info=False;User ID=mentormentee2;Password=#MSADSaging;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        mentors.Add(reader.GetString(0));
                    }
                }
            }

            return mentors;
        }

        private async Task<List<(string ReceiverName, string Message)>> FetchTopChatsAsync()
        {
            var chats = new List<(string, string)>();
            string query = "SELECT TOP 5 ReceiverName, message FROM Chat2"; 

            using (var connection = new SqlConnection("Server=tcp:mentor2.database.windows.net,1433;Initial Catalog=mentor-mentee2;Persist Security Info=False;User ID=mentormentee2;Password=#MSADSaging;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        chats.Add((reader.GetString(0), reader.GetString(1)));
                    }
                }
            }

            return chats;
        }

        private void BindMentorsToUI(List<string> mentors)
        {
            var mentorLabels = new List<Label> { Mentor1Label, Mentor2Label, Mentor3Label, Mentor4Label, Mentor5Label };

            for (int i = 0; i < mentors.Count; i++)
            {
                mentorLabels[i].Text = mentors[i];
            }
        }

        private void BindChatsToUI(List<(string ReceiverName, string Message)> chats)
        {
            var chatLabels = new List<(Label name, Label message)>
            {
                (Chat1ReceiverLabel, Chat1MessageLabel),
                (Chat2ReceiverLabel, Chat2MessageLabel),
                (Chat3ReceiverLabel, Chat3MessageLabel),
                (Chat4ReceiverLabel, Chat4MessageLabel),
                (Chat5ReceiverLabel, Chat5MessageLabel)
            };

            for (int i = 0; i < chats.Count; i++)
            {
                chatLabels[i].name.Text = chats[i].ReceiverName;
                chatLabels[i].message.Text = chats[i].Message;
            }
        }

        async void OnMentorClicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            if (button?.CommandParameter != null)
            {
                var ReceiverName = button.CommandParameter.ToString();
                if (!string.IsNullOrEmpty(ReceiverName))
                {
                    await Navigation.PushAsync(new ChatSpecific(ReceiverName));
                }
            }
        }

        async void OnChatClicked(object sender, EventArgs e)
        {
            var grid = sender as Grid;
            if (grid?.BindingContext != null)
            {
                var ReceiverName = grid.BindingContext.ToString();
                if (!string.IsNullOrEmpty(ReceiverName))
                {
                    await Navigation.PushAsync(new ChatSpecific(ReceiverName));
                }
            }
        }
    }
}
