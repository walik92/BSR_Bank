using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClientApp.Helpers
{
    /// <summary>
    ///     Implementacja interfejsu INotifyPropertyChanged
    /// </summary>
    public abstract class PropertyChangedNotification : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}