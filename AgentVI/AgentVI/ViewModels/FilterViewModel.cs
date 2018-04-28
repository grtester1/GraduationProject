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

        //<begin>temp
        //public List<List<Folder>> AccountFolders { get; set; }
        public List<Folder> AccountFolders { get; set; }
        //<end>temp

        private ObservableCollection<TabButtonModel> _tabButtons;
        public ObservableCollection<TabButtonModel> TabButtons
        {
            get
            {
                return _tabButtons;
            }
            set
            {
                _tabButtons = value;
                OnPropertyChanged(nameof(TabButtons));
            }
        }

        public FilterViewModel()
        {
            /*
            TabButtons = new ObservableCollection<TabButtonModel>()
            {
                new TabButtonModel() { MyImageURL = "https://picsum.photos/201", Image = new Image(){Source = "https://picsum.photos/201" }, IconName="Events" },
                new TabButtonModel() { MyImageURL = "https://picsum.photos/202", Image = new Image(){Source = "https://picsum.photos/202" }, IconName="Cameras" },
                new TabButtonModel() { MyImageURL = "https://picsum.photos/203", Image = new Image(){Source = "https://picsum.photos/203" }, IconName="Health" },
                new TabButtonModel() { MyImageURL = "https://picsum.photos/204", Image = new Image(){Source = "https://picsum.photos/204" }, IconName="Settings" }
            };*/
            //AccountFolders = new List<List<Folder>>();
            m_filterService = new FilterService();
            //AccountFolders.Add(m_filterService.getAccountFolders(LoginService.Instance.LoggedInUser));
            AccountFolders = m_filterService.getAccountFolders(LoginService.Instance.LoggedInUser);
            /*
            All = new ObservableCollection<ColorModel>
            {
                new ColorModel{Name="red", Color=Color.Red},
                new ColorModel{Name="green", Color=Color.Green},
                new ColorModel{Name="yellow", Color=Color.Yellow},
                new ColorModel{Name="blue", Color=Color.Blue}
            };
            */
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
