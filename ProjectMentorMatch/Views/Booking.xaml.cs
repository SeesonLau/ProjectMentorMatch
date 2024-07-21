namespace ProjectMentorMatch.Views;

public partial class Booking : ContentPage
{
    public Command GoBackCommand { get; }

    public Booking()
	{
		InitializeComponent();
        GoBackCommand = new Command(async () => await GoBack());
        this.BindingContext = this;
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
}