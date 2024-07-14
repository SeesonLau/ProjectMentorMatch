namespace ProjectMentorMatch.Views;

public partial class Dashboard : ContentPage
{
    public List<CarouselView> CarouselViews { get; set; }

    public Dashboard()
	{
		InitializeComponent();
        InitializeCarousel();

    }

    
    private void InitializeCarousel()
    {
        var items = new List<CarouselItem>
            {
                // will cause an error if the image file name is in uppercase
                new CarouselItem { ImageSource = "search_icon.png", Title = "Lord Kent F. Dinampo" },
                new CarouselItem { ImageSource = "chat_icon.png", Title = "Item 2" },
                new CarouselItem { ImageSource = "dotnet_bot.png", Title = "Item 3" },
                new CarouselItem { ImageSource = "analytics.png", Title = "Item 4" },
            };

        carouselView.ItemsSource = items;
    }

    public class CarouselItem
    {
        public string ImageSource { get; set; }
        public string Title { get; set; }
    }
}

	

	
