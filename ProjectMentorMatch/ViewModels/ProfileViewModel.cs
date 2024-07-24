using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private string _profileImageSource;
        public string ProfileImageSource
        {
            get => _profileImageSource;
            set
            {
                if (_profileImageSource != value)
                {
                    _profileImageSource = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
