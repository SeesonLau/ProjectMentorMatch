using Microsoft.Maui.Controls;

namespace ProjectMentorMatch.Views
{
    public partial class Chat : ContentPage
    {
        public Chat()
        {
            InitializeComponent();
        }

        async void OnMentorClicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            if (button?.CommandParameter != null)
            {
                var mentorName = button.CommandParameter.ToString();
                if (!string.IsNullOrEmpty(mentorName))
                {
                    await Navigation.PushAsync(new ChatSpecific(mentorName));
                }
            }
        }

        async void OnChatClicked(object sender, EventArgs e)
        {
            var grid = sender as Grid;
            if (grid?.BindingContext != null)
            {
                var mentorName = grid.BindingContext.ToString();
                if (!string.IsNullOrEmpty(mentorName))
                {
                    await Navigation.PushAsync(new ChatSpecific(mentorName));
                }
            }
        }
    }
}
