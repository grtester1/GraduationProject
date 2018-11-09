#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using AgentVI.Services;
using AgentVI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using AgentVI.Models;

namespace AgentVI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterPage : CarouselPage
    {
        private FilterViewModel filterVM { get; set; }
        private FilterIndicatorViewModel filterIndicatorVM { get; set; }

        private ListView currentListView;
        private SearchBar currentSearchBar;
        private IEnumerable<FolderModel> unfilteredFoldersList;

        private FilterPage()
        {
            InitializeComponent();
            ServiceManager.Instance.FilterService.InitServiceModule();
        }

        public FilterPage(FilterIndicatorViewModel i_FilterIndicatorViewModel) : this()
        {
            filterIndicatorVM = i_FilterIndicatorViewModel;
            filterVM = new FilterViewModel();
            BindingContext = filterVM;
            CurrentPageChanged += FilterPage_CurrentPageChanged;
        }

        internal void ResetVMToRootLevel()
        {
            filterVM = new FilterViewModel();
        }

        private void FilterPage_CurrentPageChanged(object sender, EventArgs e)
        {
            if (unfilteredFoldersList != null)
            {
                var selectedItem = currentListView.SelectedItem;
                currentListView.ItemsSource = unfilteredFoldersList;
                currentListView.SelectedItem = selectedItem;

                currentListView.Focus();
                currentSearchBar.TextChanged -= filterSearchBar_TextChanged;
                currentSearchBar.Text = String.Empty;
                currentSearchBar.TextChanged += filterSearchBar_TextChanged;
                unfilteredFoldersList = null;
                currentSearchBar = null;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            filterIndicatorVM.UpdateCurrentPath();
            return base.OnBackButtonPressed();
        }

        private async void button_Clicked(object sender, EventArgs e)
        {
            Button menuItem = sender as Button;
            Folder selectedFolder = (menuItem.CommandParameter as FolderModel).ProxyFolder;
            await Task.Factory.StartNew(() =>
            {
                filterVM.FetchNextFilteringDepth(selectedFolder);
            });
            considerSwipeRightSwipeUp();
        }

        private async void handle_FilterListItemSelected(object i_Sender, ItemTappedEventArgs i_ItemEventArgs)
        {
            filterVM.CurrentlySelectedFolder = (i_ItemEventArgs.Item as FolderModel).ProxyFolder;

            await Task.Factory.StartNew(() =>
            {
                filterVM.FetchCurrentFilteringDepth(filterVM.CurrentlySelectedFolder);
            });
            OnBackButtonPressed();
        }

        private void updateFilterLevelView(ListView i_ListView, int i_CurrenDepth)
        {
            Label filterDepthLabel = ((ListView)i_ListView).Parent.FindByName<Label>("filterNumLabel");
            if (Int32.TryParse(filterDepthLabel.Text, out i_CurrenDepth))
            {
                using (Converters.FilterPageIDConverter a = new Converters.FilterPageIDConverter())
                {
                    i_CurrenDepth = (int)a.ConvertBack(i_CurrenDepth, null, null, null);
                }
            }
        }

        private void considerSwipeRightSwipeUp()
        {
            int numberOfPages = filterVM.FilteringPagesContent.Count;
            int numberOfSelectedFolders = filterVM.SelectedFoldersCache.Count;
            FilteringPageViewModel nextPage = filterVM.FilteringPagesContent[numberOfPages - 1];

            if (nextPage != null && numberOfPages != numberOfSelectedFolders)
            {
                SelectedItem = nextPage;
                OnBindingContextChanged();
            }
            else
            {
                OnBackButtonPressed();
            }
        }

        private void filterSearchBar_TextChanged(object i_Sender, TextChangedEventArgs i_TextChangeEventArgs)
        {
            if (unfilteredFoldersList == null)
            {
                currentSearchBar = i_Sender as SearchBar;
                currentListView = currentSearchBar.Parent.FindByName<ListView>("filteredItemsListView");
                unfilteredFoldersList = currentListView.ItemsSource.Cast<FolderModel>();
            }
            if (String.IsNullOrWhiteSpace(i_TextChangeEventArgs.NewTextValue))
                currentListView.ItemsSource = unfilteredFoldersList;
            else
                currentListView.ItemsSource = unfilteredFoldersList.Cast<FolderModel>().Where(item => item.FolderName.StartsWith(i_TextChangeEventArgs.NewTextValue));
        }

        private async void onBackButtonTapped(object sender, EventArgs e)
        {
            Console.WriteLine("###Logger###   -   in FilterPage.onBackButtonTapped main thread @ begin");
            int currentPage = 0;
            if (filterVM.CurrentPageNumber == 0)
            {
                Console.WriteLine("###Logger###   -   in FilterPage.onBackButtonTapped main thread @ before first Task");
                await Task.Factory.StartNew(() => currentPage = filterVM.GetPreviousPageIndex());
                Console.WriteLine("###Logger###   -   in FilterPage.onBackButtonTapped main thread @ after first Task");
                OnBackButtonPressed();
            }
            else
            {
                Console.WriteLine("###Logger###   -   in FilterPage.onBackButtonTapped main thread @ before second Task");
                await Task.Factory.StartNew(() => currentPage = filterVM.GetPreviousPageIndex());
                Console.WriteLine("###Logger###   -   in FilterPage.onBackButtonTapped main thread @ after(1) second Task");
                SelectedItem = filterVM.FilteringPagesContent[currentPage];
                Console.WriteLine("###Logger###   -   in FilterPage.onBackButtonTapped main thread @ after(2) second Task");
                OnBindingContextChanged();
            }
            Console.WriteLine("###Logger###   -   in FilterPage.onBackButtonTapped main thread @ end");
        }
    }
}