using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch.ViewModels
{
    public class ViewModels : INotifyPropertyChanged
    {
        public ScheduleViewModel Schedule { get; set; }
        public SubjectsViewModel Subjects { get; set; }
        public ProfileViewModel Profile { get; set; }
        public ViewModels()
        {
            Schedule = new ScheduleViewModel();
            Subjects = new SubjectsViewModel();
            Profile = new ProfileViewModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
