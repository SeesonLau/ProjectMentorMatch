using Plugin.LocalNotification;
using ProjectMentorMatch.Methods;

namespace ProjectMentorMatch
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();         
        }


        //You can delete OnCounterClicked
        //the function below is just an example


        private async void OnCreateAccountClicked(object sender, EventArgs e)
        {        
            AccessPermissions.RequestNotificationPermission();
            await Navigation.PushAsync(new CreateAccount());
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            AccessPermissions.RequestNotificationPermission();
            await Navigation.PushAsync(new SignIn());
        }

      
    }

}
