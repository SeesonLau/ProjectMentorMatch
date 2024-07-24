using ProjectMentorMatch.Models;

namespace ProjectMentorMatch.Views;

public partial class Booking : ContentPage
{
    public Command GoBackCommand { get; }

    public string SelectedFullName { get; set; }

    public Booking(string selectedFullName, int selectedProfileID)
    {
        InitializeComponent();
        // Set the binding context or directly set the property
        SelectedFullName = selectedFullName;
        BindingContext = this;
        bookName.Text = selectedFullName;
        bookProfileID.Text = selectedProfileID.ToString();
    }


    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("/Dashboard", animate: true);
    }

    private void OnSetupModeCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                // Logic when a setup mode radio button is selected
                string selectedSetupMode = radioButton.Content.ToString();
                Console.WriteLine("Selected Setup Mode: " + selectedSetupMode);
            }
        }
    }

    private void OnInteractionModeCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                // Logic when an interaction mode radio button is selected
                string selectedInteractionMode = radioButton.Content.ToString();
                Console.WriteLine("Selected Interaction Mode: " + selectedInteractionMode);
            }
        }
    }

    private void btnBookNow_Clicked(object sender, EventArgs e)
    {
        // Retrieve values from UI elements
        string fullname = bookName.Text ?? string.Empty;
        string subject = lblSubs.Text ?? string.Empty;
        string schedule = lblSched.Text ?? string.Empty;

        // Retrieve selected setup mode and interaction mode from radio buttons
        string setup = rbnF2F.IsChecked ? rbnF2F.Content.ToString() : rbnOnline.IsChecked ? rbnOnline.Content.ToString() : string.Empty;
        string interactionMode = rbnOneToOne.IsChecked ? rbnOneToOne.Content.ToString() : rbnGroup.IsChecked ? rbnGroup.Content.ToString() : string.Empty;

        // Create a new Booking object
        BookingModels booking = new BookingModels
        {
            // Set the properties with retrieved values
            Picture = "sample_profile2.png", // Default picture or fetch from a source if available
            Fullname = fullname,
            MentorFee = 0.0f, // Set default or fetch from somewhere
            Subject = subject,
            Address = string.Empty, // Default or fetch from somewhere
            Ratings = 0.0f, // Default or fetch from somewhere
            HeartReact = 0, // Default or fetch from somewhere
            Setup = setup // or interactionMode if that's what you need
        };

        // Insert the booking into the database
        booking.InsertBooking();
    }






}
