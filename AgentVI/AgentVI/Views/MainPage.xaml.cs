using AgentVI.Services;
using AgentVI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AgentVI.Views
{
    public partial class MainPage : ContentPage
    {
        private LoginPageViewModel m_LoginPageViewModel = null;
        private FilterIndicatorViewModel m_FilterIndicatorViewModel = null;
        private Page m_FilterPage = null;
        private Dictionary<String, ContentPage> pageCollection = new Dictionary<String, ContentPage>()
        {
            {"Page1", new Page1()},
            {"Page2", new Page2()},
            {"CamerasPage", new CamerasPage()},
            {"SettingsPage", new SettingsPage()},
            {"EventsPage", new EventsPage()}
        };

        public MainPage()
        {
            m_FilterIndicatorViewModel = new FilterIndicatorViewModel();
            m_FilterPage = new FilterPage(m_FilterIndicatorViewModel);
            InitializeComponent();
            FilterStateIndicatorListView.BindingContext = m_FilterIndicatorViewModel;//ServiceManager.Instance.FilterService.getSelectedFoldersHirearchy();
            m_LoginPageViewModel = new LoginPageViewModel();
            m_LoginPageViewModel.InitializeFields(ServiceManager.Instance.LoginService.LoggedInUser);
            BindingContext = m_LoginPageViewModel;
            NavigationPage.SetHasNavigationBar(this, false);
        }


        void FilterStateIndicator_Tapped(object i_Sender, EventArgs i_EventArgs)
        {
            Navigation.PushModalAsync(m_FilterPage);
        }

        void FooterBarEvents_Clicked(object i_Sender, EventArgs i_EventArgs)
        {
            PlaceHolder.Content = pageCollection["EventsPage"].Content;
        }
        
        void FooterBarCameras_Clicked(object i_Sender, EventArgs i_EventArgs)
        {
            //Gil - you should take care of updating the content of camera's list. Updated content by current filtration could be achieved through -> ServiceManager.Instance.FilterService.GetFilteredSensorCollection()
            PlaceHolder.Content = pageCollection["CamerasPage"].Content;
        }

        void FooterBarHealth_Clicked(object i_Sender, EventArgs i_EventArgs)
        {
            
        }

        void FooterBarSettings_Clicked(object i_Sender, EventArgs i_EventArgs)
        {
            PlaceHolder.Content = pageCollection["SettingsPage"].Content;
        }

        protected override bool OnBackButtonPressed()
        {
            //Handle back button pressed in MainPage
            return true;
        }
    }
}