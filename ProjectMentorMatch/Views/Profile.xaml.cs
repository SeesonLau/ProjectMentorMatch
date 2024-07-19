using System.Collections.ObjectModel;
using ProjectMentorMatch.ViewModels;
using UraniumUI.Material.Controls;
using ProjectMentorMatch.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Maui.ApplicationModel.Communication;
using Syncfusion.Maui.Core;

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
        int profileID = App.ProfileID;
        string? fullname = profile.GetFullName(userID);
        string? email = profile.GetEmail(userID);
        string? cN = profile.GetContactNumber(userID);
        DateTime? birthday = profile.GetBirthday(userID);
        string? qualification = profile.GetQualification(userID);
        //string? gender = profileInfo.GetGender(profileID);
        //SelectGenderChip(gender);

        //int profileID = App.ProfileID;

        string? addressCity = profileInfo.GetAddressCity(profileID);
        string? addressProvince = profileInfo.GetAddressProvince(profileID);

        MainThread.BeginInvokeOnMainThread(() =>
        {
            UpperNameEntry.Text = fullname;
            userNameEntry.Text = fullname;
            emailTextField.Text = email;
            contactNumberTextField.Text = cN;
            gradeCourseEditor.Text = qualification;
            cityTextField.Text = addressCity;
            provinceTextField.Text = addressProvince;

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
        string? gender = "";
        // Retrieve the selected gender
        if (genderChipGroup.SelectedItem is SfChip selectedChip)
        {
            gender = selectedChip.Text;
        }

        string? qualification = gradeCourseEditor.Text;
        string? addressCity = cityTextField.Text;
        string? addressProvince = provinceTextField.Text;

        //string? aboutMe;
        //string? qualification = q;
        // string? isMentor;
        try
        {
            profile.SetBirthday(birthday);
            profile.SetContactNumber(contactNumber);
            profileInfo.SetGender(gender);
            profile.SetQualification(qualification);
            profileInfo.SetAddressCity(addressCity);
            profileInfo.SetAddressProvince(addressProvince);


            int userID = App.UserID;
            int profileID = App.ProfileID;

            profile.InsertProfileData(userID);
            profileInfo.InsertProfileGender(profileID);
            profileInfo.InsertProfileAddress(profileID);

            await DisplayAlert("Success", "User information has been saved.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
    private async Task SelectGenderChip(string? gender)
    {
        try
        {
            if (genderChipGroup != null && genderChipGroup.ItemsSource != null)
            {
                // Cast ItemsSource to a list if necessary
                var chips = genderChipGroup.ItemsSource.OfType<SfChip>().ToList();

                foreach (var chip in chips)
                {
                    if (chip.Text == gender)
                    {
                        // Set the matching chip as selected
                        genderChipGroup.SelectedItem = chip; // Use the appropriate method to select
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Pick Image please...",
            FileTypes = FilePickerFileType.Images
        });

        if (result == null) { 
            return;
        }

        var stream = await result.OpenReadAsync();

        ProfileImage.ImageSource = ImageSource.FromStream(() => stream);
    }
}
