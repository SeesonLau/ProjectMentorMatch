using System.Collections.ObjectModel;
using ProjectMentorMatch.ViewModels;
using UraniumUI.Material.Controls;
using ProjectMentorMatch.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Maui.ApplicationModel.Communication;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.Core.Carousel;

namespace ProjectMentorMatch.Views;

public partial class Profile : ContentPage
{
    ProfileModels profile;
    ProfileInformation profileInfo;

    private byte[]? selectedImageBytes;

    public void SetProfilePicture(byte[]? imageBytes)
    {
        selectedImageBytes = imageBytes;
    }

    public Profile()
	{
        InitializeComponent();
        profile = new ProfileModels();
        profileInfo = new ProfileInformation();
        LoadProfileData();
    }

    
    private async void LoadProfileData()
    {
        try
        {
            int userID = App.UserID;
            int profileID = App.ProfileID;

            string? fullname = profile.GetFullName(userID);
            string? email = profile.GetEmail(userID);

            string? cN = profile.GetContactNumber(userID);
            DateTime? birthday = profile.GetBirthday(userID);
            string? qualification = profile.GetQualification(userID);
            string? eduback = profile.GetEducBack(userID);
            string? gender = profile.GetGender(userID);

            string? addressCity = profile.GetAddressCity(userID);
            string? addressProvince = profile.GetAddressProvince(userID);
         
            var subjects = await profile.GetSubjectMenteeAsync(userID);

            byte[]? profileImageBytes = profile.GetProfileImage(userID);
            
            MainThread.BeginInvokeOnMainThread(() =>
            {
                userNameEntry.Text = fullname;
                emailTextField.Text = email;
                contactNumberTextField.Text = cN;
                gradeCourseEditor.Text = qualification;
                cityTextField.Text = addressCity;
                provinceTextField.Text = addressProvince;
                //educationalBackgroundEditor.Text = eduback;
                if (birthday.HasValue)
                {
                    birthDatePicker.Date = birthday.Value;
                }
                
                //academicSubjectsPicker.SelectedItems = new ObservableCollection<string>(subjects.Academic);
                //nonAcademicSubjectsPicker.SelectedItems = new ObservableCollection<string>(subjects.NonAcademic);

                if (profileImageBytes != null)
                {
                    ProfileImage.ImageSource = ImageSource.FromStream(() => new MemoryStream(profileImageBytes));
                }

                if (genderChipGroup.Items != null)
                {
                    var chipToSelect = genderChipGroup.Items.FirstOrDefault(chip => chip.Text == gender);
                    if (chipToSelect != null)
                    {
                        genderChipGroup.SelectedItem = chipToSelect;
                    }
                }
            });
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
    private void OnApplyMentorClicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new ApplyAsMentor());
    }
    private void GoBackButton_Clicked(object sender, EventArgs e)
    {
        var mentorListViewModel = new MentorListViewModel();
        Navigation.PushAsync(new Dashboard(mentorListViewModel));
    }
    private void SettingsButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Settings());
    }
    private async void OnSaveProfileClicked(object sender, EventArgs e)
    {
       // ScheduleMenteeViewModel SVM = new ScheduleMenteeViewModel();


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
        //string? educback = educationalBackgroundEditor.Text;
        //var selectedSchedules = SVM.GetSelectedDays();

        //var scheduleViewModel = new ScheduleViewModel();
        //var selectedSchedules = scheduleViewModel.GetSelectedDays();

        try
        {
            profile.SetBirthday(birthday);
            profile.SetContactNumber(contactNumber);
            profile.SetGender(gender);
            profile.SetQualification(qualification);
            profile.SetAddressCity(addressCity);
            profile.SetAddressProvince(addressProvince);
            //profile.SetAboutMe(educback);

            int userID = App.UserID;
            int profileID = App.ProfileID;

            profile.InsertProfile(userID);
            profile.InsertAddress(userID);
            profile.InsertSubject(userID);
            //profile.InsertSchedules(userID);


            //if (profile.CheckScheduleMenteeExist(userID))
            //{
           //     profile.DeleteScheduleMentee(userID);
            //}

            //profile.InsertScheduleMentee(userID, selectedSchedules);


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
                var chips = genderChipGroup.ItemsSource.OfType<SfChip>().ToList();
                foreach (var chip in chips)
                {
                    if (chip.Text == gender)
                    {
                        genderChipGroup.SelectedItem = chip;
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

        if (result == null)
        {
            return;
        }

        using (var stream = await result.OpenReadAsync())
        {
            selectedImageBytes = GetProfilePictureData(stream);
            ProfileImage.ImageSource = ImageSource.FromStream(() => new MemoryStream(selectedImageBytes));
        }
    }
    private byte[] GetProfilePictureData(Stream imageStream)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            imageStream.CopyTo(ms);
            return ms.ToArray();
        }
    }
    private void SwitchMentor_StateChanged(object sender, Syncfusion.Maui.Buttons.SwitchStateChangedEventArgs e)
    {

    }
    private void SwitchMentor_StateChanging(object sender, Syncfusion.Maui.Buttons.SwitchStateChangingEventArgs e)
    {

    }
}
