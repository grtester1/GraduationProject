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
//using DummyProxy;
using InnoviApiProxy;

namespace AgentVI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilterPage : CarouselPage
	{
        private FilterViewModel m_FilterViewModel;
        private FilterIndicatorViewModel m_FilterIndicatorViewModel;

        public FilterPage(FilterIndicatorViewModel i_FilterIndicatorViewModel)
        {
            InitializeComponent ();
            m_FilterIndicatorViewModel = i_FilterIndicatorViewModel;
            m_FilterViewModel = new FilterViewModel(ServiceManager.Instance.FilterService);
            BindingContext = m_FilterViewModel;
        }

        protected override bool OnBackButtonPressed()
        {
            m_FilterIndicatorViewModel.SelectedFoldersNamesCache = ServiceManager.Instance.FilterService.getSelectedFoldersHirearchy();
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

            m_FilterViewModel.fetchNextFilteringDepth(selectedFolder, ++filterDepthLabelValue);
        }
    }
}