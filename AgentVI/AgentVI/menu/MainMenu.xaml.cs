using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AgentVI.menu;
using AgentVI.additionals;

namespace AgentVI.pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : MasterDetailPage
	{
        public NavigationPage m_navigationPage { get; set; }

        public List<MainMenuItem> m_mainMenuItems { get; set; }
        public Dictionary<eAppPagesNames, Page> m_appPagesCollection { get; private set; }

        public enum eAppPagesNames
        {
            splashPage,
            loginPage,
            mainPage,
            allEventsPage,
            allCamerasPage,
            cameEventsTimelinePage,
            camLiveVerticalPage,
            camLiveHorizontalPage,
            settingsPage,
            healthPage,
            aboutPage
        };
        public Dictionary<eAppPagesNames, string> m_enumToNameDict { get; private set; }
        public Dictionary<string, eAppPagesNames> m_nameToEnumDict { get; private set; }

        public MainMenu()
        {
            BindingContext = this;

            m_appPagesCollection = new Dictionary<eAppPagesNames, Page>();
            m_mainMenuItems = new List<MainMenuItem>();
            m_enumToNameDict = new Dictionary<eAppPagesNames, string>();
            m_nameToEnumDict = new Dictionary<string, eAppPagesNames>();

            initializePagesCollection();
            initializeMenuItemsCollection();

            m_navigationPage = new NavigationPage(m_appPagesCollection[eAppPagesNames.loginPage]) { BarBackgroundColor = Color.FromHex(Constants.kr_navBarColor) };
            Detail = m_navigationPage;

            InitializeComponent();
        }

        private void initializeMenuItemsCollection()
        {
            string currentPageName;
            foreach (var enumAndPage in m_appPagesCollection)
            {
                currentPageName = m_enumToNameDict[enumAndPage.Key];
                m_mainMenuItems.Add(new MainMenuItem(
                    currentPageName,
                    currentPageName + ".png",
                    enumAndPage.Value));
            }
        }



        private void initializePagesCollection()
        {
            m_appPagesCollection.Add(eAppPagesNames.loginPage, new loginPage());
            m_enumToNameDict.Add(eAppPagesNames.loginPage, "Log In");
            m_nameToEnumDict.Add("Log In", eAppPagesNames.loginPage);

            m_appPagesCollection.Add(eAppPagesNames.mainPage, new MainPage());
            m_enumToNameDict.Add(eAppPagesNames.mainPage, "Main Page");
            m_nameToEnumDict.Add("Main Page", eAppPagesNames.mainPage);

            //m_appPagesCollection.Add(eAppPagesNames.splashPage, new splashPage());
            //m_appPagesCollection.Add(eAppPagesNames.allCamerasPage, new allCamerasPage());
            //m_appPagesCollection.Add(eAppPagesNames.allEventsPage, new allEventsPage());
            //m_appPagesCollection.Add(eAppPagesNames.camEventsTimelinePage, new camEventsTimelinePage());
            //m_appPagesCollection.Add(eAppPagesNames.camLiveHorizontalPage, new camLiveHorizontalPage());
            //m_appPagesCollection.Add(eAppPagesNames.camLiveVerticalPage, new camLiveVerticalPage());
            //m_appPagesCollection.Add(eAppPagesNames.healthPage, new healthPage());
            //m_appPagesCollection.Add(eAppPagesNames.settingsPage, new settingsPage());
            //m_appPagesCollection.Add(eAppPagesNames.aboutPage, new aboutPage());
        }

        public void MainMenuItem_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            eAppPagesNames enumOfPageMenuItemSelected;
            MainMenuItem item = e.SelectedItem as MainMenuItem;

            if(item != null)
            {
                if (m_nameToEnumDict.TryGetValue(item.m_Title, out enumOfPageMenuItemSelected))
                {
                    m_navigationPage = new NavigationPage(m_appPagesCollection[enumOfPageMenuItemSelected]);
                }
                /*
                if(item.m_Title.Equals("Login Page"))
                {
                    Detail = new NavigationPage(new loginPage()) { BarBackgroundColor = Color.FromHex(Constants.kr_navBarColor) };
                }
                else if(item.m_Title.Equals("About..."))
                {
                    Detail = new NavigationPage(new MainPage()) { BarBackgroundColor = Color.FromHex(Constants.kr_navBarColor) };
                }
                */
                MenuListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}