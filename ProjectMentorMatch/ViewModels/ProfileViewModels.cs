using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch.ViewModels
{
    public class ProfileViewModels : INotifyPropertyChanged
    {
        public ScheduleViewModel Schedule { get; set; }
        public SubjectsViewModel Subjects { get; set; }

        public ProfileViewModels()
        {
            Schedule = new ScheduleViewModel();
            Subjects = new SubjectsViewModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
