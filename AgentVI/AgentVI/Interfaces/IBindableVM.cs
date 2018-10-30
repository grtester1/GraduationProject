using AgentVI.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AgentVI.Interfaces
{
    public abstract class IBindableVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private FilterIndicatorViewModel _filterIndicator;
        public FilterIndicatorViewModel FilterIndicator
        {
            get => _filterIndicator;
            protected set
            {
                _filterIndicator = value;
                OnPropertyChanged(nameof(FilterIndicator));
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
