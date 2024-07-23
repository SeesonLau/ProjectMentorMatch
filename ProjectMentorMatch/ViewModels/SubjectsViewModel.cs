using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectMentorMatch.ViewModels
{
    public class SubjectsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> _availableSub;
        private ObservableCollection<string> _selectedSub;
        private string _selectedSubjectsText;

        private ObservableCollection<string> _availableNonSub;
        private ObservableCollection<string> _selectedNonSub;
        private string _selectedNonSubjectsText;

        public static class SubjectService
        {
            public static ObservableCollection<string> SelectedSub { get; set; } = new ObservableCollection<string>();
            public static ObservableCollection<string> SelectedNonSub { get; set; } = new ObservableCollection<string>();
        }

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
            get => SubjectService.SelectedSub;
            set
            {
                SubjectService.SelectedSub = value;
                OnPropertyChanged(nameof(SelectedSub));
            }
        }

        public string SelectedSubjectsText
        {
            get => _selectedSubjectsText;
            set
            {
                _selectedSubjectsText = value;
                OnPropertyChanged(nameof(SelectedSubjectsText));
            }
        }

        public ObservableCollection<string> AvailableNonSub
        {
            get => _availableNonSub;
            set
            {
                _availableNonSub = value;
                OnPropertyChanged(nameof(AvailableNonSub));
            }
        }

        public ObservableCollection<string> SelectedNonSub
        {
            get => SubjectService.SelectedNonSub;
            set
            {
                SubjectService.SelectedNonSub = value;
                OnPropertyChanged(nameof(SelectedNonSub));
            }
        }

        public string SelectedNonSubjectsText
        {
            get => _selectedNonSubjectsText;
            set
            {
                _selectedNonSubjectsText = value;
                OnPropertyChanged(nameof(SelectedNonSubjectsText));
            }
        }

        public ICommand DisplaySelectedSubjectsCommand { get; }

        public SubjectsViewModel()
        {
            // Initialize fields to ensure they are non-nullable
            _availableSub = new ObservableCollection<string>
        {
            "Business", "Calculus", "Chemistry", "Computer Programming",
            "Economics", "Engineering", "Foreign Language", "Health/Nutrition",
            "History", "Literacy", "Mathematics", "Medical",
            "Philosophy", "Physics", "Science"
        };
            _selectedSub = new ObservableCollection<string>();

            _availableNonSub = new ObservableCollection<string>
        {
            "Art/Design", "Cooking/Culinary Arts", "Creative Writing", "Dance",
            "Fitness Training", "Music", "Performing Arts", "Photography and Photo Editing",
            "Physical Education", "Public Speaking", "Sex Education", "Sign Language", "Video Editing"
        };
            _selectedNonSub = new ObservableCollection<string>();

            _selectedSubjectsText = string.Empty;
            _selectedNonSubjectsText = string.Empty;

            DisplaySelectedSubjectsCommand = new Command(OnButtonClick);
        }

        private void OnButtonClick()
        {
            SelectedSubjectsText = string.Join(", ", SelectedSub);
            SelectedNonSubjectsText = string.Join(", ", SelectedNonSub);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
