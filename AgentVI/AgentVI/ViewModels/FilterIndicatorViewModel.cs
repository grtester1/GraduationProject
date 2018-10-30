﻿using AgentVI.Interfaces;
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
        public event PropertyChangedEventHandler PropertyChanged;
        private string _selectedFoldersNamesCacheStr = null;
        public string SelectedFoldersNamesCacheStr
        {
            get => _selectedFoldersNamesCacheStr;
            private set
            {
                _selectedFoldersNamesCacheStr = value;
                OnPropertyChanged(nameof(SelectedFoldersNamesCacheStr));
            }
        }
        private ObservableCollection<Folder> _selectedFoldersNamesCache = null;
        public ObservableCollection<Folder> SelectedFoldersNamesCache
        {
            get => _selectedFoldersNamesCache;
            set
            {
                _selectedFoldersNamesCache = new ObservableCollection<Folder>(value);
                //SelectedFoldersNamesCacheStr = currenPathToString(_selectedFoldersNamesCache);
                OnPropertyChanged(nameof(SelectedFoldersNamesCache));
            }
        }

        

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            ServiceManager.Instance.FilterService.SelectRootLevel(true);
            UpdateCurrentPath();
        }
    }
}
