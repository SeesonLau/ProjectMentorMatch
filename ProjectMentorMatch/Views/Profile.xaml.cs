using System.Collections.ObjectModel;
using ProjectMentorMatch.ViewModels;
using UraniumUI.Material.Controls;
using ProjectMentorMatch.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Maui.ApplicationModel.Communication;

namespace ProjectMentorMatch.Views;

public partial class Profile : ContentPage
{
    Account account;
    ProfileInformation profileInfo;
    public Profile()
	{
        InitializeComponent();
        profileInfo = new ProfileInformation();
        account = new Account();
        LoadProfileData();

    }
    private void LoadProfileData()
    {
        //int userID = 943678; //ID ni EDJER
        int userID = App.UserID;
        string? fullname = profileInfo.GetFullName(userID);

        MainThread.BeginInvokeOnMainThread(() =>
        {
            UpperNameEntry.Text = fullname;
            userNameEntry.Text = fullname;
        });
    }
    private void OnApplyMentorClicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new ApplyAsMentor());
    }

    private void GoBackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Dashboard());
    }

    private void SettingsButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Settings());
    }
}