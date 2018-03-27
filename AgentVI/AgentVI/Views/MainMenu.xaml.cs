using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AgentVI.Models;
using AgentVI.additionals;
using AgentVI.ViewModels;

namespace AgentVI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : MasterDetailPage
	{
        public NavigationPage m_navigationPage { get; set; }


        public MainMenu()
        {
            m_navigationPage = new NavigationPage(PagesDataSource.Instance.m_appPagesCollection[PagesDataSource.eAppPagesNames.loginPage]) { BarBackgroundColor = Color.FromHex(Constants.kr_navBarColor) };
            Detail = m_navigationPage;

            InitializeComponent();
        }

        public void MainMenuItem_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            PagesDataSource.eAppPagesNames enumOfPageMenuItemSelected;
            MainMenuItem item = e.SelectedItem as MainMenuItem;

            if(item != null)
            {
                if (PagesDataSource.Instance.m_nameToEnumDict.TryGetValue(item.m_Title, out enumOfPageMenuItemSelected))
                {
                    Detail = new NavigationPage(PagesDataSource.Instance.m_appPagesCollection[enumOfPageMenuItemSelected]) { BarBackgroundColor = Color.FromHex(Constants.kr_navBarColor) };
                }
                MenuListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}