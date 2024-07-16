using ProjectMentorMatch.Models;

namespace ProjectMentorMatch.Views;

public partial class Dashboard : ContentPage
{
    private bool _isRefreshing;


    // INSTRUCTIONS HOW TO BIND DATA: 
    // 0. Make sure that the attributes/fields are public from the classes and initialized it in GET AND SET in order the binding to recognize it as a property in the xaml
    // 1. Create a List<> to initilize the attributes from the classes you want to bind
    // 2. Call the data from the database
    // 3. Set up the binding attributes to be called in the xaml
    // 4. Bind the data to the xaml


    //  List to call the attributes from the Account.cs
    public List<Account> CarouselItems { get; set; } = new List<Account>();

    // Add profile list once we had the profile page

    public Dashboard()
	{
		InitializeComponent();
        InitializeCarouselAsync();
    }


    private async void OnFilterButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Filter());
    }

    private async Task InitializeCarouselAsync()
    {
        try
        {
            //Call the accounts from the database
            var accounts = await Task.Run(() => Account.GetAllAccounts());

            foreach (var account in accounts)
            {
                CarouselItems.Add(new Account
                {
                    // Setting up the binding attribute name to be called in the xaml
                    fullname = account.GetFullname(),
                    email = account.GetEmail(),
                }

                // Once we have profile page, we can add the other attributes to the list AKA CarouselItems.Add(new Profile
                );

            }

            // Binding the accounts to the xaml carouselview
            carouselView.ItemsSource = CarouselItems;
        }
        catch (Exception ex)
        {
            await DisplayAlert($"Error: {ex.Message}", "Failed to load data. Please try again later.", "OK");
        }

    }

    private async void OnRefresh(object sender, EventArgs e)
    {
        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await DisplayAlert("No Internet", "Please check your internet connection and try again.", "OK");
            refreshView.IsRefreshing = false;
            return;
        }

        await InitializeCarouselAsync();
        refreshView.IsRefreshing = false;
    }



    // Show a message if there is no internet connection
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Check for internet connectivity when the page appears
        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            // No internet connection, show a blank page with message
            Content = new StackLayout
            {
                Children =
                    {
                        new Label
                        {
                            Text = "No Internet Connection",
                            FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.CenterAndExpand
                        }
                    }
            };
        }
        else
        {
            // Internet connection available, initialize the carousel
            if (CarouselItems.Count == 0)
                await InitializeCarouselAsync();
        }
    }


}