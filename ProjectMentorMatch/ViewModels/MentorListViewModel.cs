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

        public string? CurrentItemContactNumber => CurrentItem?.contactNumber;   

        public string? CurrentItemEmail => CurrentItem?.email;  

        public string? CurrentItemSubjects => CurrentItem?.subjects;
        public string? CurrentItemImageSource => CurrentItem?.ImageSource;



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
                var subjects = mentor.GetSubjectsByUserID(mentor.ProfileID);
                var imageFilename = mentor.GetImageFilenameByUserID(mentor.ProfileID);



                var itemInfo = new ItemInfo
                {
                     ItemName = mentor.GetFullname(),
                     ProfileID = mentor.ProfileID,
                    aboutMe = mentor.GetAboutMeByUserID(mentor.ProfileID),
                    addressCity = mentor.GetCityByUserID(mentor.ProfileID),  
                    addressProvince = mentor.GetProvinceByUserID(mentor.ProfileID),
                    email = mentor.GetEmailByUserID(mentor.ProfileID),
                    subjects = $"{subjects.Academic}, {subjects.Nonacademic}",
                    qualifications = mentor.GetQualificationByUserID(mentor.ProfileID),

                
                  //  ImageSource = "Resources/Images/" + imageFilename : null




                   ImageSource = imageFilename != null ? "Resources/Images/jennie.jpg" + imageFilename : null // Set the image path
                // ImageSource = imageFilename != null ? $"Resources/Images/jennie.jpg" : null // Set the image path

            };

                ItemList.Add(itemInfo);
            }


         

        }


  

    }
}
