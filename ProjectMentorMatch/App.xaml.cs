using CommunityToolkit.Maui.Core;
using Plugin.LocalNotification;
using ProjectMentorMatch.Views;

namespace ProjectMentorMatch
{
    public partial class App : Application
    {
        public static int UserID { get; set; }
        public App()
        {
            InitializeComponent();
         
            //DO NOT CHANGE! Adding Syncfusion License
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzM4MDgwNEAzMjM2MmUzMDJlMzBIK0tOQ1Jnd1ByTExNeklSZEkxZ0Fzb3VwYXdvRzk4SkNvRHZ0bzJxMzhnPQ==");

            //MainPage = new AppShell(); 
            MainPage = new NavigationPage(new Settings());

        }
    }
}
