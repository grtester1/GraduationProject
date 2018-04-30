using AgentVI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using InnoviApiProxy;
using AgentVI.Services;

namespace AgentVI.ViewModels
{
    public class FilterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private FilterService m_filterService;

        private ObservableCollection<FilteringPageViewModel> _FilteringPagesContent;
        public ObservableCollection<FilteringPageViewModel> FilteringPagesContent
        {
            get
            {
                return _FilteringPagesContent;
            }
            private set
            {
                _FilteringPagesContent = value;
                OnPropertyChanged();
            }
        }
        public List<Folder> AccountFolders { get; private set; }


        public FilterViewModel()
        {
            FilteringPagesContent = new ObservableCollection<FilteringPageViewModel>();
            m_filterService = new FilterService();
            AccountFolders = m_filterService.getAccountFolders(LoginService.Instance.LoggedInUser);

            FilteringPagesContent.Add(new FilteringPageViewModel(m_filterService.getAccountFolders(LoginService.Instance.LoggedInUser), 0));
        }


        public void addPage()
        {

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
