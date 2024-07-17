using ProjectMentorMatch.Models;

namespace ProjectMentorMatch.Views;

public partial class Profile : ContentPage
{
	public Profile()
	{
		InitializeComponent();
    }

    private void OnApplyMentorClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
        Navigation.PushAsync(new ApplyAsMentor());
    }
}