using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms.Extended;
using System.Threading.Tasks;

namespace AgentVI.ViewModels
{
    public abstract class FilterDependentViewModel<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected IEnumerable<T> enumerableCollection;
        private const int pageSize = 10;
        private IEnumerator<T> collectionEnumerator;
        protected bool canLoadMore = false;
        protected bool IsFilterStateChanged { get; set; }
        public ObservableCollection<T> ObservableCollection { get; set; }
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        private bool _isEmptyFolder = true;
        public bool IsEmptyFolder
        {
            get => _isEmptyFolder;
            set
            {
                _isEmptyFolder = value;
                OnPropertyChanged();
            }
        }

        private void updateFolderState()
        {
            if (ObservableCollection.Count == 0)
            {
                IsEmptyFolder = true;
            }
            else
            {
                IsEmptyFolder = false;
            }
        }


        public FilterDependentViewModel()
        {
            ObservableCollection = new InfiniteScrollCollection<T>()
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;
                    await Task.Factory.StartNew(() => FetchCollection());
                    IsBusy = false;
                    return null;
                },
                OnCanLoadMore = () =>
                {
                    return canLoadMore;
                }
            };
        }

        protected virtual void FetchCollection()
        {
            bool hasNext = true;
            int fetchedItems = 0;

            if(collectionEnumerator == null || IsFilterStateChanged)
            {
                IsFilterStateChanged = false;
                collectionEnumerator = enumerableCollection.GetEnumerator();
            }

            try
            {
                while (hasNext = collectionEnumerator.MoveNext() && canLoadMore)
                {
                    ObservableCollection.Add(collectionEnumerator.Current);
                    if (fetchedItems++ == pageSize)
                    {
                        break;
                    }
                }
            }catch(ArgumentOutOfRangeException)
            {
                hasNext = false;
            }

            if(hasNext == false)
            {
                canLoadMore = false;
            }

            updateFolderState();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void PopulateCollection()
        {
            ObservableCollection.Clear();
            IsFilterStateChanged = true;
            canLoadMore = true;
        }

        public virtual void OnFilterStateUpdated(object source, EventArgs e)
        {
            IsFilterStateChanged = true;
        }
    }
}
