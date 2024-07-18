namespace ProjectMentorMatch.Views;

public partial class MenteeSearch : ContentPage
{
	public MenteeSearch()
	{
		InitializeComponent();
	}
    private void GoBackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Search());
    }
}