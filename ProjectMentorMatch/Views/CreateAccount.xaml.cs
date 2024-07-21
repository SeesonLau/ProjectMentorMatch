using Microsoft.Maui.Controls;
using ProjectMentorMatch.Models;

namespace ProjectMentorMatch
{
    public partial class CreateAccount : ContentPage
    {
        public CreateAccount()
        {
             InitializeComponent();
        }
        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string fullname = FullnameEntry.Text + " " + LastnameEntry.Text;
            string password = PasswordEntry.Text;
            string confirmPassword = ConfirmPasswordEntry.Text;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Error", "No internet connection. Please check your connection and try again.", "OK");
                return;
            }

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                await DisplayAlert("Error", "All fields are required.", "OK");
                return;
            }

            if (password != confirmPassword)
            {
                await DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            if (!Methods.EmailFormat.IsValidEmail(email))
            {
                await DisplayAlert("Error", "Invalid email format.", "OK");
                return;
            }

            try
            {
                Account account = new Account();
                account.SetFullname(fullname);
                account.SetPassword(password);
                account.SetEmail(email);

               
                account.SignUp();
             //   account.sendEmail(email); //sends email

                await DisplayAlert("Success", "Account created successfully.", "OK");

                //Clear entries after registering
                EmailEntry.Text = string.Empty;
                FullnameEntry.Text = string.Empty;
                LastnameEntry.Text = string.Empty;
                PasswordEntry.Text = string.Empty;
                ConfirmPasswordEntry.Text = string.Empty;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"{ex.Message}", "OK");
            }
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignIn());


        }

        [Obsolete]
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushAsync(new MainPage());
            });
            return true;
        }

    }
}

