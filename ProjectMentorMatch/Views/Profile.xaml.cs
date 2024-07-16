namespace ProjectMentorMatch.Views;

public partial class Profile : ContentPage
{
	public Profile()
	{
		InitializeComponent();
	}

    private void OnApplyMentorClicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new ApplyAsMentor());
    }
}