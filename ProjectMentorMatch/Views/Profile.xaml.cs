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
    ProfileInformation profileInfo;
    public Profile()
	{
        InitializeComponent();
        profile = new ProfileModels();
        profileInfo = new ProfileInformation();
        LoadProfileData();

    }
    private void LoadProfileData()
    {
        int userID = App.UserID;
        string? fullname = profile.GetFullName(userID);
        string? email = profile.GetEmail(userID);
        string? cN = profile.GetContactNumber(userID);
        DateTime? birthday = profile.GetBirthday(userID);

        //int profileID = App.ProfileID;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            UpperNameEntry.Text = fullname;
            userNameEntry.Text = fullname;
            emailTextField.Text = email;
            contactNumberTextField.Text = cN;
            if (birthday.HasValue)
            {
                birthDatePicker.Date = birthday.Value; 
            }
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
        DateTime? birthday = birthDatePicker.Date;
        string? contactNumber = contactNumberTextField.Text;
        string? gender = genderChipGroup.ToString();


        //string? aboutMe;
        //string? qualification = q;
        // string? isMentor;
        try
        {
            profile.SetBirthday(birthday);
            profile.SetContactNumber(contactNumber);
            profileInfo.SetGender(gender);


            int userID = App.UserID;
            int profileID = App.ProfileID;

            profile.InsertProfileData(userID);
           profileInfo.InserProfileData2(profileID);

            await DisplayAlert("Success", "User information has been saved.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
}
