using AgentVI.Interfaces;
using AgentVI.Services;
using InnoviApiProxy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AgentVI.ViewModels
{
    public class FilterIndicatorViewModel : IBindableVM, INotifyPropertyChanged
    {
        private ObservableCollection<Folder> _selectedFoldersNamesCache = null;
        public ObservableCollection<Folder> SelectedFoldersNamesCache
        {
            get => _selectedFoldersNamesCache;
            set
            {
                _selectedFoldersNamesCache = new ObservableCollection<Folder>(value);
                OnPropertyChanged(nameof(SelectedFoldersNamesCache));
            }
        }
        
        public FilterIndicatorViewModel()
        {
            SelectedFoldersNamesCache = new ObservableCollection<Folder>();
        }

        internal void UpdateCurrentPath()
        {
            SelectedFoldersNamesCache = new ObservableCollection<Folder>(ServiceManager.Instance.FilterService.CurrentPath);
        }

        internal void ResetHierarchyToRootLevel()
        {
            try
            {
                ServiceManager.Instance.FilterService.SelectRootLevel(true);
            }catch(AggregateException ex)
            {
                HandleExceptionVisibility(ex.InnerException);
            }
            UpdateCurrentPath();
        }
    }
}
