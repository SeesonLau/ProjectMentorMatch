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
        
            LoadItems();

       
            CurrentItem = ItemList.FirstOrDefault();
        }

       private void LoadItems()
    {
        var accounts = Account.GetAllAccounts();

            foreach (var account in accounts)
            {
                var profile = new ProfileModels();

                var itemInfo = new ItemInfo
                {
                    ItemId = account.GetUserID(),
                    ItemName = account.GetFullname(),
                    addressCity = profile.GetAddressCity(account.GetUserID()),  // Fetch address city
                    addressProvince = profile.GetAddressProvince(account.GetUserID()),  // Fetch address province
                    aboutMe = profile.GetAboutMe(account.GetUserID()),  // Fetch about me
                                                                        //   subjects = profile.GetSubjectsAsync(account.GetUserID()),  // Fetch subjects
                    qualifications = profile.GetQualification(account.GetUserID()),  // Fetch qualifications
                                                                                     //   availability = profile.GetAvailability(account.GetUserID()),  // Fetch availability
                    ImageSource = "dotnet_bot.png"
                    //   ImageSource = profile.GetProfileImage(account.GetUserID())

                };

                ItemList.Add(itemInfo);
            }


            // ALSO TRIED TO USE GETALLPROFILES FROM PROFILE BUT WONT WORK
            //var profiles = ProfileModels.GetAllProfiles();

            //foreach (var profile in profiles)
            //{
            //    var mentor = new ProfileModels();

            //    var itemInfo = new ItemInfo
            //    {
            //        ItemId = mentor.GetUserID(),
            //        ItemName = mentor.GetFullname(),
            //        addressCity = mentor.GetAddressCity(mentor.GetUserID()),  // Fetch address city
            //        addressProvince = profile.GetAddressProvince(mentor.GetUserID()),  // Fetch address province
            //        aboutMe = profile.GetAboutMe(mentor.GetUserID()),  // Fetch about me
            //                                                            //   subjects = profile.GetSubjectsAsync(account.GetUserID()),  // Fetch subjects
            //        qualifications = profile.GetQualification(mentor.GetUserID()),  // Fetch qualifications
            //                                                                         //   availability = profile.GetAvailability(account.GetUserID()),  // Fetch availability


            //        ImageSource = "dotnet_bot.png"
            //        //   ImageSource = profile.GetProfileImage(account.GetUserID())

            //    };

            //    ItemList.Add(itemInfo);
            //}


        }


  

    }
}
