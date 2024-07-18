namespace ProjectMentorMatch.Views;

public partial class MentorSearch : ContentPage
{
	public MentorSearch()
	{
		InitializeComponent();
	}
    private void GoBackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Search());
    }
}