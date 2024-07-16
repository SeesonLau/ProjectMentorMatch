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
    public class ScheduleViewModel : INotifyPropertyChanged
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

        public ObservableCollection<DaySchedule> Days
        {
            get => _days;
            set
            {
                _days = value;
                OnPropertyChanged();
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
