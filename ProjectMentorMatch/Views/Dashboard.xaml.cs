using ProjectMentorMatch.Models;
using System.Collections.ObjectModel;
using ProjectMentorMatch.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Maui.ApplicationModel.Communication;
using CommunityToolkit.Mvvm.ComponentModel;
using Syncfusion.Maui.Core;


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
    public Dashboard(MentorListViewModel mentorListViewModel)
	{
		InitializeComponent();
       // InitializeCarouselAsync();
       BindingContext = mentorListViewModel;
    }
    private void OnClickedFilter(object sender, EventArgs e)
    {
        NavigationDrawer.ToggleDrawer();
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
    /*  private async Task InitializeCarouselAsync()
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
                      //fullname = account.GetFullname(),
                      //email = account.GetEmail(),
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

      }*/

    /*  private async void OnRefresh(object sender, EventArgs e)
      {
          if (Connectivity.NetworkAccess != NetworkAccess.Internet)
          {
              await DisplayAlert("No Internet", "Please check your internet connection and try again.", "OK");
              refreshView.IsRefreshing = false;
              return;
          }

          await InitializeCarouselAsync();
          refreshView.IsRefreshing = false;
      }*/

    //WILL BE COMPARING THE TWO

    /*
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

    */
    /*  protected override async void OnAppearing()
      {
          base.OnAppearing();

          // Check for internet connectivity when the page appears
          if (Connectivity.NetworkAccess != NetworkAccess.Internet)
          {
              // No internet connection, show a blank page with message
              Content = new Grid
              {
                  Children =
              {
                  new Label
                  {
                      Text = "No Internet Connection",
                      FontSize = 24, // You can adjust the font size as needed
                      HorizontalOptions = LayoutOptions.Center,
                      VerticalOptions = LayoutOptions.Center
                  }
              }
              };
          }
          else
          {
              /*Content = new Grid
              {
                  Children =
              {
                  new Label
                  {
                      Text = $"UserID: { App.UserID}",
                      FontSize = 24, // You can adjust the font size as needed
                      HorizontalOptions = LayoutOptions.Start,
                      VerticalOptions = LayoutOptions.Center
                  }
              }
              };

              // Internet connection available, initialize the carousel
              if (CarouselItems.Count == 0)
                  await InitializeCarouselAsync();
          }
      }*/

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Booking());
    }

    private void btnEx_Clicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as MentorListViewModel;
        if (viewModel != null)
        {
            int currentIndex = viewModel.ItemList.IndexOf(viewModel.CurrentItem);
            int nextIndex = (currentIndex + 1) % viewModel.ItemList.Count;
            viewModel.CurrentItem = viewModel.ItemList[nextIndex];
        }
    }
    // Palihug kog bind aning button sa mentor hihi, gamita lang profileID sa pag bind
    private void btnHeart_Click(object sender, EventArgs e)
    {
   //   int userID = App.UserID;
        int profileID = App.ProfileID;
        Analytics analytics = new Analytics();
 
    //  analytics.UpdateBrainReact(profileID);
    }
}