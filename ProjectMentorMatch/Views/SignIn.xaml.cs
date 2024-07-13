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

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Error", "Email and password are required.", "OK");
            return;
        }

        try
        {
            Account account = new Account("", email, password);
            bool isUserFound = account.LogIn();

            if (isUserFound)
            {
                await DisplayAlert("Success", "Login successful.", "OK");
                // Check if Application.Current is not null
                if (Application.Current != null)
                {
                    Application.Current.MainPage = new AppShell();
                    Shell.Current.GoToAsync("//MentorMatch");
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