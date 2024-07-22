using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.ObjectModel;

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

    private void ApplyButton_Clicked(object sender, EventArgs e)
    {
        int userID = App.UserID;
        int profileID = App.ProfileID;


    }
}