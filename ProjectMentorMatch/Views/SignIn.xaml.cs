using Microsoft.Maui.Controls;
using ProjectMentorMatch.Models;
using ProjectMentorMatch.Views;
using System;

namespace ProjectMentorMatch;

public partial class SignIn : ContentPage
{
	public SignIn()
	{
		InitializeComponent();
	}
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await DisplayAlert("Error", "No internet connection. Please check your connection and try again.", "OK");
            return;
        }

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Error", "Email and password are required.", "OK");
            return;
        }

        try
        {
            Account account = new Account();
            ProfileInformation pf = new ProfileInformation();
            account.SetEmail(email);
            account.SetPassword(password);

            bool isUserFound = account.LogIn(email, password);

            if (isUserFound)
            {                                                           
                await DisplayAlert("Success", "Login successful.", "OK", $"{pf.GetUserID()}");
                account.SetUserID(account.GetUserID());

                if (Application.Current != null)
                {
                    Application.Current.MainPage = new AppShell();
                    await Shell.Current.GoToAsync("//MentorMatch");
                }
            }
            else
            {
                await DisplayAlert("Error", "Invalid email or password.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
    private async void OnSignUpClicked(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new CreateAccount());
    }
}