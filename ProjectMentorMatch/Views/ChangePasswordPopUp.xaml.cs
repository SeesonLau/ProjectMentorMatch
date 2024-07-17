using CommunityToolkit.Maui.Views;

namespace ProjectMentorMatch.Views;

public partial class ChangePasswordPopUp : Popup
{
	public ChangePasswordPopUp()
	{
		InitializeComponent();
	}

    private void BackImageButton_Clicked(object sender, EventArgs e)
    {
		Close();
    }
}