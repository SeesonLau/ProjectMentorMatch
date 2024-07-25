using Microsoft.Data.SqlClient;
using ProjectMentorMatch.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UraniumUI.Material.Controls;


namespace ProjectMentorMatch.ViewModels
{
    public class ScheduleViewModel : INotifyPropertyChanged
    {
        ProfileModels profile = new ProfileModels();
        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<string> _availableSched;
        private ObservableCollection<string> _selectedSched;
        private string _selectedSchedText;

        public static class SubjectService
        {
            public static ObservableCollection<string> SelectedSched { get; set; } = new ObservableCollection<string>();
        }

        public ObservableCollection<string> AvailableSched
        {
            get => _availableSched;
            set
            {
                _availableSched = value;
                OnPropertyChanged(nameof(AvailableSched));
            }
        }

        public ObservableCollection<string> SelectedSched
        {
            get => SubjectService.SelectedSched;
            set
            {
                SubjectService.SelectedSched = value;
                OnPropertyChanged(nameof(SelectedSched));
            }
        }

        public string SelectedSchedText
        {
            get => _selectedSchedText;
            set
            {
                _selectedSchedText = value;
                OnPropertyChanged(nameof(SelectedSchedText));
            }
        }

        public ICommand DisplaySelectedSchedCommand { get; }
        public ScheduleViewModel()
        {
            _availableSched = new ObservableCollection<string>
            {
                "Monday", "Tuesday", "Wednesday", "Thursday",
                "Friday", "Saturday", "Sunday"
            };
            _selectedSched = new ObservableCollection<string>();

            _selectedSchedText = string.Empty;

            DisplaySelectedSchedCommand = new Command(OnButtonClick);
        }

        private void OnButtonClick()
        {
            SelectedSchedText = string.Join(", ", SelectedSched);
        }
        

        public void SaveSelectedDaysToDatabase(int userId)
        {
            using (SqlConnection connection = Database.GetConnection())
            {
                bool profileExists = profile.CheckSchedExist(userId);
                string query;
                if (profileExists)
                {
                    // Update the existing profile
                    query = "UPDATE [Mentor] SET [Day] = @Day " +
                            "WHERE [UserID] = @UserID";
                }
                else
                {
                    // Insert a new profile
                    query = "INSERT INTO [Mentor] ([UserID], [Day])" +

                            "VALUES (@UserID, @Day)";
                }
                connection.Open();

                // Concatenate the selected days into a single string
                SelectedSchedText = string.Join(", ", SelectedSched);

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@Day", SelectedSchedText);

                    command.ExecuteNonQuery();
                }
            }          
        }

        public string GetSelectedDaysFromDatabase(int userId)
        {
            string query = "SELECT [Day] FROM [Mentor] WHERE [UserID] = @UserID";
            using (var connection = Database.GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userId);
                connection.Open();
                return (string)command.ExecuteScalar();
            }
        }

        public void LoadSelectedDays(int userId, MultiplePickerField schedulePicker)
        {
            string selectedDays = GetSelectedDaysFromDatabase(userId);

            if (selectedDays == null)
            {
                SelectedSched.Clear();
            }
            else
            {
                var daysList = selectedDays.Split(new[] { ", " }, StringSplitOptions.None).ToList();

                SelectedSched.Clear();
                foreach (var day in daysList)
                {
                    SelectedSched.Add(day);
                }
            }
        }

        /*public async Task LoadSchedulesFromDatabase(int userId)
        {
            try
            {
                using (SqlConnection connection = Database.GetConnection())
                {
                    await connection.OpenAsync();

                    string query = "SELECT Day, FromTime, ToTime FROM UserSchedules WHERE UserID = @UserID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userId);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            var days = new ObservableCollection<DaySchedule>();

                            while (await reader.ReadAsync())
                            {
                                days.Add(new DaySchedule
                                {
                                    Day = reader["Day"].ToString(),
                                    FromTime = TimeSpan.Parse(reader["FromTime"].ToString()),
                                    ToTime = TimeSpan.Parse(reader["ToTime"].ToString()),
                                    IsSelected = true // or false, depending on your logic
                                });
                            }

                            Days = days;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine($"Error loading schedules: {ex.Message}");
            }
        }*/
        //Okay
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }  
}
