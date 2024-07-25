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

    public ApplyAsMentor()
	{
		InitializeComponent();
        profile = new ProfileModels();
        scheduleViewModel = new ScheduleViewModel();
        subjectsViewModel = new SubjectsViewModel();
        mentor = new Mentor();

        subjectsViewModel = new SubjectsViewModel();

        int userID = App.UserID;
        subjectsViewModel.LoadAcademicSubs(userID, academicSubjectsPicker);
        subjectsViewModel.LoadNonAcademicSubs(userID, nonAcademicSubjectsPicker);
        scheduleViewModel.LoadSelectedDays(userID, SchedulePicker);
    }

    private async void LoadSchedules()
    {
        int userId = App.UserID;
    }

    private async void GoBackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Profile", animate: true);
    }

    private async void ApplyButton_Clicked(object sender, EventArgs e)
    {

        string? aboutMe = aboutMeEntry.Text;
        float mentorFee = float.Parse(MentorFeeEntry.Text);
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
    private void ApplyButton_Clicked_1(object sender, EventArgs e)
    {
        int userID = App.UserID;
        
    }
}