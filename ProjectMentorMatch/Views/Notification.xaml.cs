using Microsoft.Maui.Controls.Compatibility;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification.iOSOption;

namespace ProjectMentorMatch.Views;

public partial class Notification : ContentPage
{
    public Notification()
    {
        InitializeComponent();
    }

    private void OnNotificationClicked(object sender, EventArgs e)
    {
        var request = new NotificationRequest
        {
            NotificationId = 1337,
            Title = "Mentor Application Confirmed",
            Subtitle = "Thank you for trusting MentorMatch! Happy Teaching :)",
            Description = "Your application for being a mentor is confirmed",
            BadgeNumber = 42,
            CategoryType = NotificationCategoryType.Status,
            Schedule = new NotificationRequestSchedule
            {
                //NotifyTime = DateTime.Now.AddSeconds(0.5),
               //RepeatType = NotificationRepeat.TimeInterval
            },
            Android = new AndroidOptions
            {
                IsProgressBarIndeterminate = true,
            },
            iOS = new iOSOptions()
        };

        LocalNotificationCenter.Current.Show(request);
    }

    private void OnRemoveMentorClicked(object sender, EventArgs e)
    {
        // Logic to remove the mentor from the recommended list
        var button = sender as Button;
        var parentFrame = button?.Parent?.Parent as Frame;
        if (parentFrame != null)
        {
            var parentLayout = parentFrame.Parent as Layout<View>;
            if (parentLayout != null)
            {
                parentLayout.Children.Remove(parentFrame);
            }
        }
    }
}
