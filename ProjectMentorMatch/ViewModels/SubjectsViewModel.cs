using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch.ViewModels
{
    public class SubjectsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> _availableSub;
        private ObservableCollection<string> _selectedSub;

        public ObservableCollection<string> AvailableSub
        {
            get => _availableSub;
            set
            {
                _availableSub = value;
                OnPropertyChanged(nameof(AvailableSub));
            }
        }

        public ObservableCollection<string> SelectedSub
        {
            get => _selectedSub;
            set
            {
                _selectedSub = value;
                OnPropertyChanged(nameof(SelectedSub));
            }
        }

        public SubjectsViewModel()
        {
            AvailableSub = new ObservableCollection<string>
            {
                "Business", "Calculus", "Chemistry", "Computer Programming",
                "Economics", "Engineering", "Foreign Language", "Health/Nutrition",
                "History", "Literacy", "Mathematics", "Medical",
                "Philosophy", "Physics", "Science"
            };
            SelectedSub = new ObservableCollection<string>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
