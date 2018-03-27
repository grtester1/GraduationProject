using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AgentVI.Models
{
    public class PagesDataSource
    {
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
        public Dictionary<eAppPagesNames, Page> m_appPagesCollection { get; private set; }
        public Dictionary<eAppPagesNames, string> m_enumToNameDict { get; private set; }
        public Dictionary<string, eAppPagesNames> m_nameToEnumDict { get; private set; }

        private static PagesDataSource _pagesDataSourceInstance;

        public static PagesDataSource Instance
        {
            get
            {
                if(_pagesDataSourceInstance == null)
                {
                    _pagesDataSourceInstance = new PagesDataSource();
                }
                return _pagesDataSourceInstance;
            }
        }

        private PagesDataSource()
        {
            m_appPagesCollection = new Dictionary<eAppPagesNames, Page>();
            m_enumToNameDict = new Dictionary<eAppPagesNames, string>();
            m_nameToEnumDict = new Dictionary<string, eAppPagesNames>();

            initializePagesCollection();
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
    }
}
