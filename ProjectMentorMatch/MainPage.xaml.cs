namespace ProjectMentorMatch
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }


        //You can delete OnCounterClicked
        //the function below is just an example
        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void OnCreateAccountClicked(object sender, EventArgs e)
        {
            // Navigate to Page1.xaml
            await Navigation.PushAsync(new CreateAccount());

        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignIn());
        }
    }

}
