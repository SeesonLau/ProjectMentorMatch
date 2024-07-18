using CommunityToolkit.Maui.Views;
using ProjectMentorMatch.ViewModels;

namespace ProjectMentorMatch.Views;

public partial class Settings : ContentPage
{
	public Settings()
	{
		InitializeComponent();
	}

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new Profile());
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
}