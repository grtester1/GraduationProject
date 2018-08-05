#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using AgentVI.Models;
using AgentVI.Services;
using AgentVI.ViewModels;
using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public FilterPage(FilterIndicatorViewModel i_FilterIndicatorViewModel)
        {
            InitializeComponent ();
            m_FilterIndicatorViewModel = i_FilterIndicatorViewModel;
            m_FilterViewModel = new FilterViewModel(ServiceManager.Instance.FilterService);
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
            m_FilterIndicatorViewModel.SelectedFoldersNamesCache = ServiceManager.Instance.FilterService.GetSelectedFoldersHirearchy();
            return base.OnBackButtonPressed();
        }

        private void Handle_FilterListItemSelected(object i_Sender, SelectedItemChangedEventArgs i_ItemEventArgs)
        {
            int filterDepthLabelValue = -1;
            Folder selectedFolder = i_ItemEventArgs.SelectedItem as Folder;
            Label filterDepthLabel = ((ListView)i_Sender).Parent.FindByName<Label>("filterNumLabel");
            if(Int32.TryParse(filterDepthLabel.Text, out filterDepthLabelValue))
            {
                using (Converters.FilterPageIDConverter a = new Converters.FilterPageIDConverter())
                {
                    filterDepthLabelValue = (int)a.ConvertBack(filterDepthLabelValue, null, null, null);
                }
            }

            m_FilterViewModel.FetchNextFilteringDepth(selectedFolder, ++filterDepthLabelValue);
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
    }
}