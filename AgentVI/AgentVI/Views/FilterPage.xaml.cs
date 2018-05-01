using AgentVI.Models;
using AgentVI.ViewModels;
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
        FilterViewModel m_VM = new FilterViewModel();

        public FilterPage()
        {
            InitializeComponent ();
            BindingContext = m_VM;
        }

        protected override bool OnBackButtonPressed()
        {
            //TODO implement the update of filters state to MainPage and FilterService members
            return base.OnBackButtonPressed();
        }

        private void Handle_FilterListItemSelected(object i_sender, SelectedItemChangedEventArgs i_itemEventArgs)
        {
            int filterDepthLabelValue = -1;
            InnoviApiProxy.Folder selectedFolder = i_itemEventArgs.SelectedItem as InnoviApiProxy.Folder;
            Label filterDepthLabel = ((ListView)i_sender).Parent.FindByName<Label>("filterNumLabel");
            if(Int32.TryParse(filterDepthLabel.Text, out filterDepthLabelValue))
            {
                using (Converters.FilterPageIDConverter a = new Converters.FilterPageIDConverter())
                {
                    filterDepthLabelValue = (int)a.ConvertBack(filterDepthLabelValue, null, null, null);
                }
            }

            m_VM.fetchNextFilteringDepth(selectedFolder, ++filterDepthLabelValue);
        }
    }
}