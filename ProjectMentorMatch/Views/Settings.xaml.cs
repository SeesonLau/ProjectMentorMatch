using CommunityToolkit.Maui.Views;
using ProjectMentorMatch.ViewModels;

namespace ProjectMentorMatch.Views;

public partial class Settings : ContentPage
{
	public Settings()
	{
		InitializeComponent();
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



}