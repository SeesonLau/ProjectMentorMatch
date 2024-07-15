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

            try
            {
                //Account account = new Account(fullname, email, password);
                Account account = new Account();
                account.SetFullname(fullname);
                account.SetPassword(password);
                account.SetEmail(email);

                account.SignUp();
                await DisplayAlert("Success", "Account created successfully.", "OK");
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

    }
}

