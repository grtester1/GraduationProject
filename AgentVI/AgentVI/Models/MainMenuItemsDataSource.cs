using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using AgentVI.Models;

namespace AgentVI.Models
{
    public static class MainMenuItemsDataSource
    {
        static MainMenuItemsDataSource()
        {

        }

        public static ObservableCollection<MainMenuItem> getMainMenuItems()
        {
            ObservableCollection<MainMenuItem> mainMenuItems = new ObservableCollection<MainMenuItem>();
            PagesDataSource appPages = PagesDataSource.Instance;
            string currentPageName;
            foreach (var enumAndPage in appPages.m_appPagesCollection)
            {
                currentPageName = appPages.m_enumToNameDict[enumAndPage.Key];
                mainMenuItems.Add(new MainMenuItem(
                    currentPageName,
                    currentPageName + ".png",
                    enumAndPage.Value));
            }

            return mainMenuItems;
        }
    }
}
