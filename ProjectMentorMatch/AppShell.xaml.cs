using ProjectMentorMatch.Views;

namespace ProjectMentorMatch
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Routing Codes in order for the app to recognize the pages for navigation
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(CreateAccount), typeof(CreateAccount));
            Routing.RegisterRoute(nameof(HomeScreen), typeof(HomeScreen));
            Routing.RegisterRoute(nameof(SignIn), typeof(SignIn));
            Routing.RegisterRoute(nameof(Dashboard), typeof(Dashboard));
        }
    }
}
