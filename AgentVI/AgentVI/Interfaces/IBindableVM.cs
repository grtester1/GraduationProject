using AgentVI.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AgentVI.Interfaces
{
    public abstract class IBindableVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _filtrationPath;
        public string FiltrationPath
        {
            get
            {
                if(Services.ServiceManager.Instance.LoginService.LoggedInUser != null &&
                    Services.ServiceManager.Instance.FilterService != null)
                return Services.ServiceManager.Instance.FilterService.CurrentStringPath;
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                _filtrationPath = value;
                IsRootSelectedAndResetClickable =   value != null
                                                    && value.CompareTo(string.Empty) != 0
                                                    && value.Length != 0;
                OnPropertyChanged(nameof(FiltrationPath));
            }
        }

        private bool _IsRootSelectedAndResetClickable;
        public bool IsRootSelectedAndResetClickable
        {
            get => _IsRootSelectedAndResetClickable;
            private set
            {
                IsRootSelectedAndResetClickable = value;
                OnPropertyChanged(nameof(IsRootSelectedAndResetClickable));
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
