namespace ProjectMentorMatch.Views;

public partial class ApplyAsMentor : ContentPage
{
	public ApplyAsMentor()
	{
		InitializeComponent();
    }
    private void GoBackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Profile());
    }
}