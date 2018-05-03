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
        private LoginPageViewModel LoginPageViewModel = null;
        private Dictionary<String, ContentPage> pageCollection = new Dictionary<String, ContentPage>()
        {
            {"Page1", new Page1()},
            {"Page2", new Page2()},
            {"CamerasPage", new CamerasPage()},
            {"SettingsPage", new SettingsPage()},
            {"EventsPage", new EventsPage()}
        };
        private FilterIndicatorViewModel filterIndicatorViewModel = null;
        private Page FilterPage = null;


        public MainPage()
        {
            filterIndicatorViewModel = new FilterIndicatorViewModel();
            FilterPage = new FilterPage(filterIndicatorViewModel);
            InitializeComponent();
            FilterStateIndicatorListView.BindingContext = filterIndicatorViewModel;//ServiceManager.Instance.FilterService.getSelectedFoldersHirearchy();
            LoginPageViewModel = new LoginPageViewModel();
            LoginPageViewModel.InitializeFields(ServiceManager.Instance.LoginService.LoggedInUser);
            BindingContext = LoginPageViewModel;
        }


        void FilterStateIndicator_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(FilterPage);
        }

        void FooterBarEvents_Clicked(object sender, EventArgs e)
        {
            PlaceHolder.Content = pageCollection["EventsPage"].Content;
        }
        void FooterBarCameras_Clicked(object sender, EventArgs e)
        {
            PlaceHolder.Content = pageCollection["CamerasPage"].Content;
        }
        void FooterBarHealth_Clicked(object sender, EventArgs e)
        {
            
        }
        void FooterBarSettings_Clicked(object sender, EventArgs e)
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