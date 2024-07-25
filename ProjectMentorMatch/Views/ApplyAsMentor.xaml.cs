using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.ObjectModel;
using ProjectMentorMatch.ViewModels;
using UraniumUI.Material.Controls;
using ProjectMentorMatch.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Maui.ApplicationModel.Communication;
using Syncfusion.Maui.Core;
//using Windows.System;

namespace ProjectMentorMatch.Views;

public partial class ApplyAsMentor : ContentPage
{
    ProfileModels profile;
    ScheduleViewModel scheduleViewModel;
    SubjectsViewModel subjectsViewModel;
    Mentor mentor;

    public string? SelectedSetupMode { get; set; }
    public string? SelectedInteractionMode { get; set; }

    public ApplyAsMentor()
	{
		InitializeComponent();
        profile = new ProfileModels();
        scheduleViewModel = new ScheduleViewModel();
        subjectsViewModel = new SubjectsViewModel();
        mentor = new Mentor();

        subjectsViewModel = new SubjectsViewModel();

        int userID = App.UserID;
        LoadApplyMentor();
        subjectsViewModel.LoadAcademicSubs(userID, academicSubjectsPicker);
        subjectsViewModel.LoadNonAcademicSubs(userID, nonAcademicSubjectsPicker);
        scheduleViewModel.LoadSelectedDays(userID, SchedulePicker);
    }
    private void OnSetupModeCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value) // Only handle when the RadioButton is checked
        {
            var radioButton = sender as Microsoft.Maui.Controls.RadioButton;
            if (radioButton != null)
            {
                SelectedSetupMode = radioButton.Content.ToString();
                DisplayAlert("Setup Mode Selected", SelectedSetupMode, "OK");
            }
        }
    }

    private void OnInteractionModeCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value) // Only handle when the RadioButton is checked
        {
            var radioButton = sender as Microsoft.Maui.Controls.RadioButton;
            if (radioButton != null)
            {
                SelectedInteractionMode = radioButton.Content.ToString();
                DisplayAlert("Interaction Mode Selected", SelectedInteractionMode, "OK");
            }
        }
    }
    private void LoadApplyMentor()
    {
        int userId = App.UserID;
        string? aboutMe = mentor.GetAboutMe(userId);
        string? mentorFee = mentor.GetMentorFee(userId);

        MainThread.BeginInvokeOnMainThread(() =>
        {
            aboutMeEntry.Text = aboutMe;
            MentorFeeEntry.Text = mentorFee;

        });
    }


    private async void GoBackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Profile", animate: true);
    }

    private async void ApplyButton_Clicked(object sender, EventArgs e)
    {

        string? aboutMe = aboutMeEntry.Text;
        string? mentorFee = MentorFeeEntry.Text;
       // string? academic = aboutMeEntry.Text; // PLACEHOLDER
       // string? nonacademic = aboutMeEntry.Text;  // PLACEHOLDER
       // string? day = aboutMeEntry.Text;  // PLACEHOLDER
        
       


        try
        {
            int userID = App.UserID;
            //profile.ApplyAsMentor(userID);

            mentor.SetAboutMe(aboutMe);
            mentor.SetMentorFee(mentorFee);
            //mentor.SetAcademic(academic); 
            //mentor.SetNonAcademic(nonacademic);
            //mentor.SetDay(day);
             
            mentor.InsertApplyAsMentor(userID);
            scheduleViewModel.SaveSelectedDaysToDatabase(userID);
            subjectsViewModel.SaveSubjects(userID);

            //await scheduleViewModel.SaveSchedules(userID);
            await DisplayAlert("Success", "You're now a mentor bish.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    private async void btnWithdrawMentor_Clicked(object sender, EventArgs e)
    {
        try
        {
            int userID = App.UserID;
            profile.WithdrewAsMentor(userID);
            await DisplayAlert("Success", "You're now not a mentor bish", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
    //Test Apply Button Clicked
}