using CommunityToolkit.Maui.Views;
using ProjectMentorMatch.ViewModels;
using Plugin.LocalNotification;
using Syncfusion.Maui.Core;

namespace ProjectMentorMatch.Views;

public partial class Settings : ContentPage
{
	public Settings()
	{
		InitializeComponent();
        notifSwitch.IsToggled = LoadNotificationSetting();
	}

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Profile", animate: true);
    }

    private void btnChangePass_Clicked(object sender, EventArgs e)
    {
        var popup = new ChangePassPopUp();

        this.ShowPopup(popup);
    }

    private void btnPrivacyandSecurity_Clicked(object sender, EventArgs e)
    {
        var popup = new PrivacyAndSecurity();

        this.ShowPopup(popup);
    }

    private void btnAbout_Clicked(object sender, EventArgs e)
    {
        var popup = new About();

        this.ShowPopup(popup);
    }

    private async void btnLogout_Clicked(object sender, EventArgs e)
    {
        bool isConfirmed = await DisplayAlert("Log Out", "Are you sure you want to log out?", "Yes", "No");

        if (isConfirmed)
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
    }

    private void notifSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        bool isEnabled = e.Value;

        // Save the switch state
        SaveNotificationSetting(isEnabled);

        // Enable or disable notifications based on switch state
        if (isEnabled)
        {
            // Enable notifications
            EnableNotifications();
        }
        else
        {
            // Disable notifications
            DisableNotifications();
        }
    }

    private bool LoadNotificationSetting()
    {
        // Retrieve the saved notification setting from preferences or settings
        return Preferences.Get("NotificationsEnabled", true); // Default to true if not set
    }

    private void SaveNotificationSetting(bool isEnabled)
    {
        // Save the notification setting to preferences or settings
        Preferences.Set("NotificationsEnabled", isEnabled);
    }

    private void EnableNotifications()
    {
        // Logic to enable notifications
        // For example, you could configure default notification settings
    }

    private void DisableNotifications()
    {
        // Logic to disable notifications
        // For example, you might cancel all notifications
      //  NotificationCenter.Current.CancelAll();
    }



}