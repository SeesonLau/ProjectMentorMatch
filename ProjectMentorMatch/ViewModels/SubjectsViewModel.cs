using Microsoft.Data.SqlClient;
using ProjectMentorMatch.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UraniumUI.Material.Controls;

namespace ProjectMentorMatch.ViewModels
{
    public class SubjectsViewModel : INotifyPropertyChanged
    {
        ProfileModels profile = new ProfileModels();
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

        public void SaveSubjects(int userId)
        {
            using (SqlConnection connection = Database.GetConnection())
            {
                bool profileExists = profile.CheckSchedExist(userId);
                string query;
                if (profileExists)
                {
                    // Update the existing profile
                    query = "UPDATE [Mentor] SET [Academic] = @Academic, [NonAcademic] = @NonAcademic WHERE [UserID] = @UserID";
                }
                else
                {
                    // Insert a new profile
                    query = "INSERT INTO [Mentor] ([UserID], [Academic], [NonAcademic]) VALUES (@UserID, @Academic, @NonAcademic)";
                }
                connection.Open();

                // Concatenate the selected days into a single string
                SelectedSubjectsText = string.Join(", ", SelectedSub);
                SelectedNonSubjectsText = string.Join(", ", SelectedNonSub);

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@Academic", SelectedSubjectsText);
                    command.Parameters.AddWithValue("@NonAcademic", SelectedNonSubjectsText);
                    command.ExecuteNonQuery();
                }
            }
        }

        public string GetAcademicSubs(int userId)
        {
            string query = "SELECT [Academic] FROM [Mentor] WHERE [UserID] = @UserID";
            using (var connection = Database.GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userId);
                connection.Open();
                return (string)command.ExecuteScalar();
            }
        }

        public void LoadAcademicSubs(int userId, MultiplePickerField schedulePick)
        {
            string selectedSubs = GetAcademicSubs(userId);

            if (selectedSubs == null)
            {
                SelectedSub.Clear();
            }
            else
            {
                var acadSub = selectedSubs.Split(new[] { ", " }, StringSplitOptions.None).ToList();

                SelectedSub.Clear();
                foreach (var sub in acadSub)
                {
                    SelectedSub.Add(sub);
                }
            }
        }

        public string GetNonAcademicSubs(int userId)
        {
            string query = "SELECT [NonAcademic] FROM [Mentor] WHERE [UserID] = @UserID";
            using (var connection = Database.GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userId);
                connection.Open();
                return (string)command.ExecuteScalar();
            }
        }

        public void LoadNonAcademicSubs(int userId, MultiplePickerField schedulePicker)
        {
            string selectedNonSubs = GetNonAcademicSubs(userId);

            if (selectedNonSubs == null)
            {
                SelectedSub.Clear();
            }
            else
            {
                var nonacadSub = selectedNonSubs.Split(new[] { ", " }, StringSplitOptions.None).ToList();

                SelectedSub.Clear();
                foreach (var sub in nonacadSub)
                {
                    SelectedSub.Add(sub);
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        //okay
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
