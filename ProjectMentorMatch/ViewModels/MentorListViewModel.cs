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

            CurrentItem = ItemList.FirstOrDefault();
        }
    }
}
