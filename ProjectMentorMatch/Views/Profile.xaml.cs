using System.Collections.ObjectModel;
using ProjectMentorMatch.ViewModels;
using UraniumUI.Material.Controls;
using ProjectMentorMatch.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Maui.ApplicationModel.Communication;
namespace ProjectMentorMatch.Views;

public partial class Profile : ContentPage
{
    ProfileModels profile;
    public Profile()
	{
        InitializeComponent();
        profile = new ProfileModels();
        LoadProfileData();

    }
    private void LoadProfileData()
    {
        //int userID = 943678; //ID ni EDJER
        int userID = App.UserID;
        string? fullname = profile.GetFullName(userID);
        string? email = profile.GetEmail(userID);

        MainThread.BeginInvokeOnMainThread(() =>
        {
            UpperNameEntry.Text = fullname;
            userNameEntry.Text = fullname;
            emailTextField.Text = email;
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
    private async void OnSaveProfileClicked(object sender, EventArgs e)
    {
        string? birthday = birthDatePicker.ToString();
        string? contactNumber = contactNumberTextField.Text;


        //string? aboutMe;
        //string? qualification = q;
       // string? isMentor;



        int userID = App.UserID;
        profile.InsertProfileData(userID);
        await DisplayAlert("Success", "User information has been saved.", "OK");
    }
}