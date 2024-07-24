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
    public class ScheduleViewModel : INotifyPropertyChanged
    {
        ProfileModels profile;
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
            profile = new ProfileModels();
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
        }

        public List<string> GetSelectedDaysNames()
        {
            return Days.Where(day => day.IsSelected).Select(day => day.Day).ToList();
        }

        public List<TimeSpan> GetSelectedFromTimes()
        {
            return Days.Where(day => day.IsSelected).Select(day => day.FromTime).ToList();
        }

        public List<TimeSpan> GetSelectedToTimes()
        {
            return Days.Where(day => day.IsSelected).Select(day => day.ToTime).ToList();
        }

        // CALL THIS TO GETSELECTED SCHEDULE
        public List<DaySchedule> GetSelectedDays()
        {
            return Days.Where(day => day.IsSelected).ToList();
        }

        public async Task LoadSchedules(int userID)
        {
            var schedules = await Task.Run(() => profile.GetSchedules(userID));
            foreach (var schedule in schedules)
            {
                var daySchedule = Days.FirstOrDefault(d => d.Day == schedule.Day);
                if (daySchedule != null)
                {
                    daySchedule.IsSelected = true;
                    daySchedule.FromTime = schedule.FromTime;
                    daySchedule.ToTime = schedule.ToTime;
                }
            }
        }

        public async Task SaveSchedules(int userID)
        {
            var selectedSchedules = GetSelectedDays();
            await Task.Run(() => profile.SaveSchedule(userID, selectedSchedules));
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
