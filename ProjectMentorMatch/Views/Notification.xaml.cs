using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification.iOSOption;
using ProjectMentorMatch.Methods;

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
               // NotifyTime = DateTime.Now.AddSeconds(0.5),
               // NotifyRepeatInterval = TimeSpan.FromDays(1),
               // RepeatType = NotificationRepeat.TimeInterval
            },

            Android = new AndroidOptions()
            {
                IsProgressBarIndeterminate = true,           
            },

            iOS = new iOSOptions()
            {


            }

        };

        LocalNotificationCenter.Current.Show(request);
    }
}
