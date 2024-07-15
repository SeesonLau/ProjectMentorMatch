namespace ProjectMentorMatch.Views;

public partial class Filter : ContentPage
{
    private Button selectedGenderButton;
    public Filter()
	{
		InitializeComponent();
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
    private void OnChipClicked(object sender, EventArgs e)
    {
        //Button button = sender as Button;
        //if (button != null)
        //{
        //    // Toggle chip selection
        //    if (button.Style == (Style)Application.Current.Resources["ActionChipStyle"])
        //    {
        //        button.Style = (Style)Application.Current.Resources["SelectedActionChipStyle"];
        //    }
        //    else
        //    {
        //        button.Style = (Style)Application.Current.Resources["ActionChipStyle"];
        //    }
        //}
    }

    private void OnMoreSubjectsClicked(object sender, EventArgs e)
    {
        // Implement your logic for handling 'More' subjects button click
        // This can include opening a modal dialog or navigating to another page
        // where additional subject options are presented.
    }
}