using AgentVI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace AgentVI.ViewModels
{
    public class FilterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Command MyCommand { get; protected set; }
        private ObservableCollection<TabbedPageIconModel> _myItemsSource;
        public ObservableCollection<TabbedPageIconModel> MyItemsSource
        {
            get
            {
                return _myItemsSource;
            }
            set
            {
                _myItemsSource = value;
                OnPropertyChanged(nameof(MyItemsSource));
            }
        }

        public FilterViewModel()
        {
            MyItemsSource = new ObservableCollection<TabbedPageIconModel>()
            {
                new TabbedPageIconModel() { MyImageURL = "https://picsum.photos/201", MyImage = new Image(){Source = "https://picsum.photos/201" }, IconName="Banana1" },
                new TabbedPageIconModel() { MyImageURL = "https://picsum.photos/202", MyImage = new Image(){Source = "https://picsum.photos/202" }, IconName="Banana2" },
                new TabbedPageIconModel() { MyImageURL = "https://picsum.photos/203", MyImage = new Image(){Source = "https://picsum.photos/203" }, IconName="Banana3" }
            };

            MyCommand = new Command(() => { Debug.WriteLine("Position selected."); });
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
