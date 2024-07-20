namespace ProjectMentorMatch.Views;

public partial class ApplyAsMentor : ContentPage
{
	public ApplyAsMentor()
	{
		InitializeComponent();
    }
    private async void GoBackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Profile", animate: true);
    }
}