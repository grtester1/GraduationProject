#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using AgentVI.Services;
using AgentVI.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace AgentVI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilterPage : CarouselPage
	{
        private FilterViewModel m_FilterViewModel;
        private FilterIndicatorViewModel m_FilterIndicatorViewModel;

        private ListView currentListView;
        private SearchBar currentSearchBar;
        private System.Collections.IEnumerable unfilteredFoldersList;

        private FilterPage()
        {
            InitializeComponent();
            ServiceManager.Instance.FilterService.InitServiceModule();
        }

        public FilterPage(FilterIndicatorViewModel i_FilterIndicatorViewModel):this()
        {
            m_FilterIndicatorViewModel = i_FilterIndicatorViewModel;
            m_FilterViewModel = new FilterViewModel();
            BindingContext = m_FilterViewModel;
            CurrentPageChanged += FilterPage_CurrentPageChanged;
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
            m_FilterIndicatorViewModel.UpdateCurrentPath();
            OnSelectionClickedButton(null , EventArgs.Empty);
            return base.OnBackButtonPressed();
        }

        private void Handle_FilterListItemSelected(object i_Sender, SelectedItemChangedEventArgs i_ItemEventArgs)
        {
            Folder selectedFolder = i_ItemEventArgs.SelectedItem as Folder;
            //updateFilterLevelView(i_Sender as ListView, selectedFolder.Depth);   //Indicator of filtering level
            m_FilterViewModel.FetchNextFilteringDepth(selectedFolder);
            ConsiderSwipeRightSwipeUp();
        }

        private void updateFilterLevelView(ListView i_ListView, int i_CurrenDepth)
        {
            Label filterDepthLabel = ((ListView)i_ListView).Parent.FindByName<Label>("filterNumLabel");
            if(Int32.TryParse(filterDepthLabel.Text, out i_CurrenDepth))
            {
                using (Converters.FilterPageIDConverter a = new Converters.FilterPageIDConverter())
                {
                    i_CurrenDepth = (int)a.ConvertBack(i_CurrenDepth, null, null, null);
                }
            }
        }

        private void ConsiderSwipeRightSwipeUp()
        {
            int numberOfPages = m_FilterViewModel.FilteringPagesContent.Count;
            int numberOfSelectedFolders = m_FilterViewModel.SelectedFoldersCache.Count;
            FilteringPageViewModel nextPage = m_FilterViewModel.FilteringPagesContent[numberOfPages - 1];

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
                unfilteredFoldersList = currentListView.ItemsSource;
            }
            if (String.IsNullOrWhiteSpace(i_TextChangeEventArgs.NewTextValue))
                currentListView.ItemsSource = unfilteredFoldersList;
            else
                currentListView.ItemsSource = unfilteredFoldersList.Cast<Folder>().Where(item => item.Name.StartsWith(i_TextChangeEventArgs.NewTextValue));
        }

        private void OnSelectionClickedButton(object sender, EventArgs e)
        {

        }
    }
}