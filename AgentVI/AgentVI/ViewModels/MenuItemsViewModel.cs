using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using AgentVI.Models;

namespace AgentVI.ViewModels
{
    public static class MenuItemsViewModel
    {
        public static ObservableCollection<MainMenuItem> m_mainMenuItems { get; set; }

        static MenuItemsViewModel()
        {
            m_mainMenuItems = MainMenuItemsDataSource.getMainMenuItems();
        }
    }
}
