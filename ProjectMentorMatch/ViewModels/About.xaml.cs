using CommunityToolkit.Maui.Views;

namespace ProjectMentorMatch.ViewModels;

public partial class About : Popup
{
	public About()
	{
		InitializeComponent();
	}

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
		this.Close();
    }
}