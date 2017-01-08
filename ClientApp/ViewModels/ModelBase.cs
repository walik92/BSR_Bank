using System;
using System.Windows;
using ClientApp.Helpers;

namespace ClientApp.ViewModels
{
    public abstract class ModelBase : PropertyChangedNotification
    {
        private string _error;
        private Visibility _visibilityForm;
        private Visibility _visiblityProgress = Visibility.Collapsed;

        public virtual Action CloseAction { get; set; }

        public Visibility VisibilityForm
        {
            get { return _visibilityForm; }
            set
            {
                _visibilityForm = value;
                NotifyPropertyChanged("VisibilityForm");
            }
        }

        public Visibility VisibilityProgress
        {
            get { return _visiblityProgress; }
            set
            {
                _visiblityProgress = value;
                NotifyPropertyChanged("VisibilityProgress");
            }
        }

        public string Error
        {
            get { return _error; }
            set
            {
                _error = value;
                NotifyPropertyChanged("Error");
            }
        }
    }
}