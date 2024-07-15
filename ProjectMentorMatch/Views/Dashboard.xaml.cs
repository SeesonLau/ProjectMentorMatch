namespace ProjectMentorMatch.Views;

public partial class Dashboard : ContentPage
{
    public List<CarouselView> CarouselViews { get; set; } = new List<CarouselView>();

    public Dashboard()
	{
		InitializeComponent();
        InitializeCarousel();

    }

    private async void OnFilterButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Filter());
    }
    private void InitializeCarousel()
    {
        var items = new List<CarouselItem>
            {
                // will cause an error if the image file name is in uppercase
                new CarouselItem { ImageSource = "search_icon.png", Title = "Item 1" },
                new CarouselItem { ImageSource = "chat_icon.png", Title = "Item 2" },
                new CarouselItem { ImageSource = "dotnet_bot.png", Title = "Item 3" },
                new CarouselItem { ImageSource = "analytics.png", Title = "Item 4" },
            };

        carouselView.ItemsSource = items;
    }

    public class CarouselItem
    {
        public string ImageSource { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}

	

	
