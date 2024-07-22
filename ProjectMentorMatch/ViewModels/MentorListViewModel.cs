using CommunityToolkit.Mvvm.ComponentModel;
using ProjectMentorMatch.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace ProjectMentorMatch.ViewModels
{
    public partial class MentorListViewModel: ObservableObject, INotifyPropertyChanged
    {
        Database database = new Database();
        public ObservableCollection<ItemInfo> ItemList { get; set; } = new();

        private ItemInfo _currentItem;
       
        public ItemInfo CurrentItem
        {
            get => _currentItem;
            //set => SetProperty(ref _currentItem, value);
            set
            {
                if (_currentItem != value)
                {
                    _currentItem = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CurrentItemName)); // Notify change of the ItemName
                }
            }
        }

        public string CurrentItemName => CurrentItem?.ItemName;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MentorListViewModel() 
        {
            ItemList.Add(new ItemInfo() { ItemId = 1, ItemName = "Jamel", ImageSource = "sample_profile.png" });
            ItemList.Add(new ItemInfo() { ItemId = 2, ItemName = "Ana", ImageSource = "dotnet_bot.png" });
            ItemList.Add(new ItemInfo() { ItemId = 3, ItemName = "Lapinig", ImageSource = "model.jpg" });
            ItemList.Add(new ItemInfo() { ItemId = 4, ItemName = "Sison", ImageSource = "model2.jpg" });

     
            LoadItems();


            //ItemList.Add(new ItemInfo() { ItemId = 1, ItemName = "Jamel", ImageSource = "sample_profile.png" });
            //ItemList.Add(new ItemInfo() { ItemId = 2, ItemName = "Ana", ImageSource = "dotnet_bot.png" });
            //ItemList.Add(new ItemInfo() { ItemId = 3, ItemName = "Lapinig", ImageSource = "model.jpg" });
            //ItemList.Add(new ItemInfo() { ItemId = 4, ItemName = "Sison", ImageSource = "model2.jpg" });

            CurrentItem = ItemList.FirstOrDefault();
        }


        // Sa kani na method kay maka next ka sa names pero wla tung IsMentor and Other profile info
        private void LoadItems()
        {
            var accounts = Account.GetAllAccounts();

            foreach (var account in accounts)
            {
                var itemInfo = new ItemInfo
                {
                    ItemId = account.GetUserID(), 
                    ItemName = account.GetFullname(),
                    ImageSource = "dotnet_bot.png" 
                };

                ItemList.Add(itemInfo);
            }

            //var mentors = ProfileModels.GetAllMentors();

        }


        // Sa kani kay ako gi try integrate both tables para makuha ang Profile Table pero dili mag next,

        //private void LoadItems()
        //{


        //    // Fetch mentor data
        //    var mentors = ProfileModels.GetAllMentors();

        //    // Fetch account data and join with mentor data
        //    var accounts = Account.GetAllAccounts();

        //    foreach (var mentor in mentors)
        //    {
        //        // Find the account corresponding to the mentor
        //        var account = accounts.FirstOrDefault(acc => acc.GetUserID() == mentor.GetUserID());

        //        if (account != null)
        //        {
        //            var itemInfo = new ItemInfo
        //            { 
        //                ItemId = mentor.GetUserID(),
        //                ItemName = account.GetFullname(),
        //                ImageSource = "dotnet_bot.png",
        //                Address = mentor.GetAddressCity(mentor.GetUserID())
                            // Other fields here
        //            };

        //            ItemList.Add(itemInfo);
        //        }
        //    }


        }
}
