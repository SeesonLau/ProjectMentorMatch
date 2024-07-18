using CommunityToolkit.Maui.Views;

namespace ProjectMentorMatch.Views;

public partial class PrivacyAndSecurity : Popup
{
	public PrivacyAndSecurity()
	{
		InitializeComponent();
	}

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
		this.Close();
    }
}