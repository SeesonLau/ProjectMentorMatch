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
                    // Binding Declaration
                    _currentItem = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CurrentItemName)); // Notify change of the ItemName
                    OnPropertyChanged(nameof(CurrentItemAddressCity)); // Notify change of the address city
                    OnPropertyChanged(nameof(CurrentItemAddressProvince)); // Notify change of the address province
                    OnPropertyChanged(nameof(CurrentItemAboutMe)); // Notify change of the about me
                    OnPropertyChanged(nameof(CurrentItemQualifications)); // Notify change of the qualifications

                }
            }
        }


        // To be put in the Binding of the Dashboard
        public string CurrentItemName => CurrentItem?.ItemName;
        public string CurrentItemAddressCity => CurrentItem?.addressCity;
        public string CurrentItemAddressProvince => CurrentItem?.addressProvince;
        public string CurrentItemAboutMe => CurrentItem?.aboutMe;
        public string CurrentItemQualifications => CurrentItem?.qualifications;




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
        var mentors = Account.GetAllMentors();

            foreach (var mentor in mentors)
            {
                var profile = new ProfileModels();

                var itemInfo = new ItemInfo
                {
                     ItemName = mentor.GetFullname(),
                    ProfileID = mentor.ProfileID,
                    addressCity = mentor.GetCityByUserID(mentor.ProfileID),  
                    addressProvince = mentor.GetProvinceByUserID(mentor.ProfileID),
                    
                 //   addressCity = profile.GetAddressCity(App.UserID),  // Fetch address city
               //     addressProvince = profile.GetAddressProvince(account.GetUserID()),  // Fetch address province
                 //   aboutMe = profile.GetAboutMe(account.GetUserID()),  // Fetch about me
                                                                        //   subjects = profile.GetSubjectsAsync(account.GetUserID()),  // Fetch subjects
               //     qualifications = profile.GetQualification(account.GetUserID()),  // Fetch qualifications
                                                                                     //   availability = profile.GetAvailability(account.GetUserID()),  // Fetch availability
                    ImageSource = "dotnet_bot.png"
                    //   ImageSource = profile.GetProfileImage(account.GetUserID())

                };

                ItemList.Add(itemInfo);
            }


            // ALSO TRIED TO USE GETALLPROFILES FROM PROFILE BUT WONT WORK
            var profiles = ProfileModels.GetAllProfiles();

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
