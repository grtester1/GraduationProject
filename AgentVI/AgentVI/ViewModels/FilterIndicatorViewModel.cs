using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AgentVI.ViewModels
{
    public class FilterIndicatorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<String> _SelectedFoldersNamesCache = null;
        public List<String> SelectedFoldersNamesCache
        {
            get
            {
                return _SelectedFoldersNamesCache;
            }
            set
            {
                _SelectedFoldersNamesCache = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public FilterIndicatorViewModel()
        {
            SelectedFoldersNamesCache = new List<String> { "Filter is not set" };
        }
    }
}
