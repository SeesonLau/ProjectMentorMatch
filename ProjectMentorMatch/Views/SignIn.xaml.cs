using Microsoft.Data.SqlClient;
using Microsoft.Maui.Controls;
using ProjectMentorMatch.Models;
using ProjectMentorMatch.Views;
using System;

namespace ProjectMentorMatch;

public partial class SignIn : ContentPage
{
   // Database db = new Database();
    
    public SignIn()
	{
		InitializeComponent();
    }

    public int GetUserID()
    {
        try
        {
            using (SqlConnection cn = Database.GetConnection())
            {
                cn.Open();
                using (SqlCommand cm = new SqlCommand("SELECT [UserID] FROM [CreateAccount] WHERE [Email] = @email AND [Password] = @pass", cn))
                {
                    cm.Parameters.AddWithValue("@email", EmailEntry.Text);
                    cm.Parameters.AddWithValue("@pass", PasswordEntry.Text);
                    using (SqlDataReader dr = cm.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (dr["UserID"] != DBNull.Value && int.TryParse(dr["UserID"].ToString(), out int ID))
                            {
                                return ID;
                            }
                        }
                    }
                }
            }
            return 0;
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "OK");
            return 0;
        }
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
            account.SetEmail(email);
            account.SetPassword(password);


            bool isUserFound = account.LogIn(email, password);

            if (isUserFound)
            {
                int userId = GetUserID();
                App.UserID = userId;

                await DisplayAlert("Success", "Login successful.", $"{userId}", "OK");
                // Check if Application.Current is not null
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