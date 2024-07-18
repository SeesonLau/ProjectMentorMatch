namespace ProjectMentorMatch.Views;

public partial class Search : ContentPage
{
	public Search()
	{
		InitializeComponent();
	}
    private void OnClickedMentor(object sender, EventArgs e)
    {
        var mentorName = (sender as Element).BindingContext as string;
        Navigation.PushAsync(new MentorSearch());
    }
    private void OnClickedMentee(object sender, EventArgs e)
    {
        var mentorName = (sender as Element).BindingContext as string;
        Navigation.PushAsync(new MenteeSearch());
    }
}