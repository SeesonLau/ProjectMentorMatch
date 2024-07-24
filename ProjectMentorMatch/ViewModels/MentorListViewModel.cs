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
                    OnPropertyChanged(nameof(CurrentItemContactNumber)); // Notify change of the contact number
                    OnPropertyChanged(nameof(CurrentItemEmail)); // Notify change of the email
                    OnPropertyChanged(nameof(CurrentItemSubjects)); // Notify change of the subjects
                    OnPropertyChanged(nameof(CurrentItemImageSource)); // Notify change of the image source
                    OnPropertyChanged(nameof(CurrentItemDay)); // Notify change of the day
                    OnPropertyChanged(nameof(CurrentItemRate)); // Notify change of the rate

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
        public ImageSource? CurrentItemImageSource => CurrentItem?.ImageSource;

        public string? CurrentItemDay => CurrentItem?.Day;

        public string? CurrentItemRate => CurrentItem?.Rate;






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

        // Helper method to read the entire stream into a byte array
        private byte[] ReadFully(Stream input)
        {
            using (var memoryStream = new MemoryStream())
            {
                input.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }


        private void LoadItems()
    {
        var mentors = Account.GetAllMentors();

            foreach (var mentor in mentors)
            {
                var profile = new ProfileModels();
                var imageFilename = mentor.GetProfileImageByUserID(mentor.ProfileID);

                byte[] resizedImageData = null;

                // Assuming imageFilename is the name of an image file in your Resources folder
                using (var stream = GetType().Assembly.GetManifestResourceStream($"ProjectMentorMatch.Resources.Images.{imageFilename}"))
                {
                    if (stream != null)
                    {
                        byte[] imageData = ReadFully(stream);
                        resizedImageData = Methods.CompressImage.ResizeImage(imageData, 400, 300); // Resize to desired dimensions
                    }
                }


                var itemInfo = new ItemInfo
                {
                     ItemName = mentor.GetFullname(),
                     ProfileID = mentor.ProfileID,
                    aboutMe = mentor.GetAboutMeByUserID(mentor.ProfileID),
                    addressCity = mentor.GetCityByUserID(mentor.ProfileID),  
                    addressProvince = mentor.GetProvinceByUserID(mentor.ProfileID),
                    contactNumber = mentor.GetContactNumberByUserID(mentor.ProfileID),
                    subjects = $"{mentor.GetSubjectsByUserID(mentor.ProfileID).Academic}, {mentor.GetSubjectsByUserID(mentor.ProfileID).Nonacademic}",
                    qualifications = mentor.GetQualificationByUserID(mentor.ProfileID),
                   ImageSource = mentor.GetProfileImageByUserID(mentor.ProfileID), // Ensure this is correctly set
                   Day = mentor.GetDayByUserID(mentor.ProfileID),
                   Rate = mentor.GetRateByUserID(mentor.ProfileID),





                    //  ImageSource = "Resources/Images/" + imageFilename : null
                    //  ImageSource = imageFilename != null ? $"Resources/Images/{imageFilename}" : null
                    // ImageSource = imageFilename != null ? $"Resources/Images/jennie.jpg" : null // Set the image path

                };

                ItemList.Add(itemInfo);
            }


         

        }


  

    }
}
