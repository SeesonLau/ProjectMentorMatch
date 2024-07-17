using ProjectMentorMatch.Views;
using System.Windows.Input;

namespace ProjectMentorMatch
{
    public partial class AppShell : Shell
    {
        public ICommand ProfileTabClickedCommand { get; set; }
        public AppShell()
        {
            InitializeComponent();

            // Routing Codes in order for the app to recognize the pages for navigation
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(CreateAccount), typeof(CreateAccount));
            Routing.RegisterRoute(nameof(HomeScreen), typeof(HomeScreen));
            Routing.RegisterRoute(nameof(SignIn), typeof(SignIn));
            Routing.RegisterRoute(nameof(Chat), typeof(Chat));
            Routing.RegisterRoute(nameof(Dashboard), typeof(Dashboard));
            Routing.RegisterRoute(nameof(Notification), typeof(Notification));
            Routing.RegisterRoute(nameof(Profile), typeof(Profile));
            Routing.RegisterRoute(nameof(Search), typeof(SearchBar));
            Routing.RegisterRoute(nameof(SignIn), typeof(SignIn));
            Routing.RegisterRoute(nameof(Analytics), typeof(Analytics));
            Routing.RegisterRoute(nameof(Settings) , typeof(Settings));
            Routing.RegisterRoute(nameof(ApplyAsMentor), typeof(ApplyAsMentor));

            ProfileTabClickedCommand = new Command<string>(OnProfileTabClicked);
        }
        
        private void OnProfileTabClicked(string username)
        {
            
        }

    }
}
