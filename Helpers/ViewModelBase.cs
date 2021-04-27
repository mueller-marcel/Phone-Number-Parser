using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PhoneNumberParser.Helpers
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Events
        /// <summary>
        /// Implementation of <see cref="INotifyPropertyChanged"/>. 
        /// Notifies the UI to change when the ViewModel is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods
        /// <summary>
        /// Fires the <see cref="PropertyChanged"/> event with the name of the property that has changed
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
