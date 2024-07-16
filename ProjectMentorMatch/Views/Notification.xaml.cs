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
            
            Title = "MENTOR CONFIRMED",
            Subtitle = "Please message him for updates",
            Description = "John Laurence G. Sison confirmed your appointment",
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
