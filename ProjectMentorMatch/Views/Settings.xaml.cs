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
}