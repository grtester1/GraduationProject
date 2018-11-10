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
            get => getCurrentFiltrationStringRep();
            set
            {
                _filtrationPath = getCurrentFiltrationStringRep();
                IsRootSelectedAndResetClickable = _filtrationPath != null
                                                    && _filtrationPath.CompareTo(string.Empty) != 0
                                                    && _filtrationPath.Length != 0;
                OnPropertyChanged(nameof(FiltrationPath));
            }
        }

        private bool _IsRootSelectedAndResetClickable;
        public bool IsRootSelectedAndResetClickable
        {
            get => _IsRootSelectedAndResetClickable;
            private set
            {
                _IsRootSelectedAndResetClickable = value;
                OnPropertyChanged(nameof(IsRootSelectedAndResetClickable));
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string getCurrentFiltrationStringRep()
        {
            string res = null;
            if (Services.ServiceManager.Instance.LoginService.LoggedInUser != null &&
                Services.ServiceManager.Instance.FilterService != null)
            {
                res = Services.ServiceManager.Instance.FilterService.CurrentStringPath;
            }
            else
            {
                res = string.Empty;
            }

            return res;
        }
    }
}
