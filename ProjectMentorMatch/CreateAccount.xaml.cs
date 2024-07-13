using Microsoft.Maui.Controls;

namespace ProjectMentorMatch;

public partial class CreateAccount : ContentPage
{
	public CreateAccount()
	{
		InitializeComponent();
	}
    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string fullname = FullnameEntry.Text;
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
            Account account = new Account(fullname, email, password);
            account.SignUp();
            await DisplayAlert("Success", "Account created successfully.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    private void OnLoginClicked(object sender, EventArgs e)
    {
       
    }

}