using ProjectMentorMatch.Models;


namespace ProjectMentorMatch.Views;

public partial class Booking : ContentPage
{
    public Command GoBackCommand { get; }

    public string SelectedFullName { get; set; }

    public Booking(string selectedFullName, int selectedProfileID, string subject, string day, string picture, string province, string city, string mentorFee)
    {
        InitializeComponent();
        // Set the binding context or directly set the property
        SelectedFullName = selectedFullName;
        BindingContext = this;
        bookName.Text = selectedFullName;
        bookProfileID.Text = selectedProfileID.ToString();
        lblSubs.Text = subject;
        lblSched.Text = day;
        bookPicture.Source = picture;
        lblLocation.Text = province + ", " + city;
        lblFee.Text = mentorFee;

        CreateSubjectCheckboxes(subject);
        CreateScheduleCheckboxes(day);
    }

    private void CreateSubjectCheckboxes(string subject)
    {
        if (string.IsNullOrEmpty(subject)) return;

        // Split the subject string into individual subjects
        var subjects = subject.Split(',');

        foreach (var subj in subjects)
        {
            var checkBoxContainer = new HorizontalStackLayout
            {
                Spacing = 10,
                Children =
                    {
                        new CheckBox
                        {
                            IsChecked = false
                        },
                        new Label
                        {
                            Text = subj.Trim(),
                            VerticalOptions = LayoutOptions.Center
                        }
                    }
            };

            chkSubsContainer.Children.Add(checkBoxContainer);
        }
    }
    private void CreateScheduleCheckboxes(string day)
    {
        if (string.IsNullOrEmpty(day)) return;

        // Split the day string into individual days
        var days = day.Split(',');

        foreach (var d in days)
        {
            var checkBoxContainer = new HorizontalStackLayout
            {
                Spacing = 10,
                Children =
                    {
                        new CheckBox
                        {
                            IsChecked = false
                        },
                        new Label
                        {
                            Text = d.Trim(),
                            VerticalOptions = LayoutOptions.Center
                        }
                    }
            };

            chkSchedContainer.Children.Add(checkBoxContainer);
        }
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
        string picture = bookPicture.Source.ToString().Replace("File:", string.Empty).Trim();
        string province = lblLocation.Text.Split(',')[0].Trim();
        string city = lblLocation.Text.Split(',')[1].Trim();
        string mentorFee = lblFee.Text ?? string.Empty;
        string day = string.Empty;

        // Retrieve selected subjects
        var selectedSubjects = chkSubsContainer.Children
            .OfType<HorizontalStackLayout>()
            .Where(container => container.Children.OfType<CheckBox>().FirstOrDefault()?.IsChecked == true)
            .Select(container => container.Children.OfType<Label>().FirstOrDefault()?.Text)
            .Where(subj => !string.IsNullOrEmpty(subj))
            .ToList();

        // Retrieve selected schedules
        var selectedSchedules = chkSchedContainer.Children
            .OfType<HorizontalStackLayout>()
            .Where(container => container.Children.OfType<CheckBox>().FirstOrDefault()?.IsChecked == true)
            .Select(container => container.Children.OfType<Label>().FirstOrDefault()?.Text)
            .Where(day => !string.IsNullOrEmpty(day))
            .ToList();


        // Retrieve selected setup mode from radio buttons
        string setup = string.Empty;
        if (rbnF2F.IsChecked)
        {
            setup = rbnF2F.Content.ToString();
        }
        else if (rbnOnline.IsChecked)
        {
            setup = rbnOnline.Content.ToString();
        }

        // Retrieve selected interaction mode from radio buttons
        string interactionMode = string.Empty;
        if (rbnOneToOne.IsChecked)
        {
            interactionMode = rbnOneToOne.Content.ToString();
        }
        else if (rbnGroup.IsChecked)
        {
            interactionMode = rbnGroup.Content.ToString();
        }

        // Create a new Booking object
        BookingModels booking = new BookingModels
        {
            // Set the properties with retrieved values
            Picture = picture,
            Fullname = fullname,
            MentorFee = mentorFee,
            Subject = string.Join(", ", selectedSubjects),
            Day = string.Join(", ", selectedSchedules),
            Address = province + ", " + city,
            Setup = setup,
            Interaction = interactionMode
        
        };

        // Insert the booking into the database
        booking.InsertBooking();

        DisplayAlert("Thank you!", $"You successfully booked {fullname}", "OK");

    }

    private void chkSubs_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {

    }
}
