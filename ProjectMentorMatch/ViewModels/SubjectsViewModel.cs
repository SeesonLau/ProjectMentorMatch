﻿using System;
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
            get => _selectedNonSub;
            set
            {
                _selectedNonSub = value;
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
            AvailableSub = new ObservableCollection<string>
            {
                "Business", "Calculus", 
                "Chemistry", "Computer Programming",
                "Economics", "Engineering", 
                "Foreign Language", "Health/Nutrition",
                "History", "Literacy", 
                "Mathematics", "Medical",
                "Philosophy", "Physics", "Science"
            };
            SelectedSub = new ObservableCollection<string>();

            AvailableNonSub = new ObservableCollection<string>
            {
                "Art/Design", "Cooking/Culinary Arts", 
                "Creative Writing", "Dance",
                "Fitness Training", "Music", 
                "Performing Arts", "Photography and Photo Editing",
                "Physical Education", "Public Speaking", 
                "Sex Education", "Sign Language", "Video Editing"
            };
            SelectedNonSub = new ObservableCollection<string>();

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