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

namespace ProjectMentorMatch.ViewModels
{
    public class ScheduleViewModel : Database, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<DaySchedule> _days = new ObservableCollection<DaySchedule>
        {
        new DaySchedule { Day = "Monday" },
        new DaySchedule { Day = "Tuesday" },
        new DaySchedule { Day = "Wednesday" },
        new DaySchedule { Day = "Thursday" },
        new DaySchedule { Day = "Friday" },
        new DaySchedule { Day = "Saturday" },
        new DaySchedule { Day = "Sunday" }
        };

        private string _selectedDaysText;
        public ObservableCollection<DaySchedule> Days
        {
            get => _days;
            set
            {
                _days = value;
                OnPropertyChanged();
            }
        }

        public string SelectedDaysText
        {
            get => _selectedDaysText;
            set
            {
                _selectedDaysText = value;
                OnPropertyChanged();
            }
        }
        public ICommand SelectedDaysCommand { get; }

        public ScheduleViewModel()
        {
            SelectedDaysCommand = new Command(OnSelectedDays);
        }

        private void OnSelectedDays()
        {
            var selectedDays = Days.Where(day => day.IsSelected).ToList();
            var sb = new StringBuilder();
            foreach (var day in selectedDays)
            {
                sb.AppendLine($"{day.Day}: From {day.FromTime:hh\\:mm} to {day.ToTime:hh\\:mm}");
            }
            SelectedDaysText = sb.ToString();
            //SaveSelectedDaysToDatabase(selectedDays);
        }

        private void InsertProfileMenteeSchedule(List<DaySchedule> selectedDays)
        {
            Account account = new Account();

            using (var connection = GetConnection())
            {
                connection.Open();

                foreach (var day in selectedDays)
                {
                    var command = new SqlCommand("INSERT INTO MenteeSchedule (UserID, Day, FromTime, ToTime) VALUES (@UserID, @Day, @FromTime, @ToTime)", connection);
                    command.Parameters.AddWithValue("@UserID", account.GetUserID());
                    command.Parameters.AddWithValue("@Day", day.Day);
                    command.Parameters.AddWithValue("@FromTime", day.FromTime);
                    command.Parameters.AddWithValue("@ToTime", day.ToTime);

                    command.ExecuteNonQuery();
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class DaySchedule : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _day;
        private bool _isSelected;
        private TimeSpan _fromTime;
        private TimeSpan _toTime;

        public string Day
        {
            get => _day;
            set
            {
                _day = value;
                OnPropertyChanged();
            }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan FromTime
        {
            get => _fromTime;
            set
            {
                _fromTime = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan ToTime
        {
            get => _toTime;
            set
            {
                _toTime = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
