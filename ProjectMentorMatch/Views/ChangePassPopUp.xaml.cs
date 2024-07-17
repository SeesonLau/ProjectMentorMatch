using CommunityToolkit.Maui.Views;

namespace ProjectMentorMatch.Views;

public partial class ChangePassPopUp : Popup
{
	public ChangePassPopUp()
	{
		InitializeComponent();
	}

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
		this.Close();
    }
}