namespace ProjectMentorMatch.Views;

public partial class Search : ContentPage
{
	public Search()
	{
		InitializeComponent();
	}
    private void OnClickedFilter(object sender, EventArgs e)
    {
        NavigationDrawer.ToggleDrawer();
    }
    private void OnClickedMentor(object sender, EventArgs e)
    {
        var mentorName = (sender as Element).BindingContext as string;
        Navigation.PushAsync(new MentorSearch());
    }
    private void OnClickedMentee(object sender, EventArgs e)
    {
        var mentorName = (sender as Element).BindingContext as string;
        Navigation.PushAsync(new MenteeSearch());
    }
    private void OnGenderCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                // Logic when a gender radio button is selected
                // For example, you can store the selected gender value
                string selectedGender = radioButton.Content.ToString();
                Console.WriteLine("Selected Gender: " + selectedGender);
            }
        }
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
}