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
        private Dictionary<String, Tuple<ContentPage, Button, Image, Label>> pageCollection;
        private const String k_TabSelectionColor = "bababa";

        public MainPage()
        {
            m_FilterIndicatorViewModel = new FilterIndicatorViewModel();
            m_FilterPage = new FilterPage(m_FilterIndicatorViewModel);
            InitializeComponent();
            FilterStateIndicatorListView.BindingContext = m_FilterIndicatorViewModel;
            m_LoginPageViewModel = new LoginPageViewModel();
            m_LoginPageViewModel.InitializeFields(ServiceManager.Instance.LoginService.LoggedInUser);
            BindingContext = m_LoginPageViewModel;
            NavigationPage.SetHasNavigationBar(this, false);
            initPagesCollection();
        }
        

        private void initPagesCollection()
        {
            pageCollection = new Dictionary<String, Tuple<ContentPage, Button, Image, Label>>();
            pageCollection.Add("Page1",             new Tuple<ContentPage, Button, Image, Label>(new Page1(), null, null, null));
            pageCollection.Add("Page2",             new Tuple<ContentPage, Button, Image, Label>(new Page2(), null, null, null));
            pageCollection.Add("CamerasPage",       new Tuple<ContentPage, Button, Image, Label>(new CamerasPage(), FooterBarCamerasButton, FooterBarCamerasImage, FooterBarCamerasLabel));
            pageCollection.Add("SettingsPage",      new Tuple<ContentPage, Button, Image, Label>(new SettingsPage(), FooterBarSettingsButton, FooterBarSettingsImage, FooterBarSettingsLabel));
            pageCollection.Add("EventsPage",        new Tuple<ContentPage, Button, Image, Label>(new EventsPage(), FooterBarEventsButton, FooterBarEventsImage, FooterBarEventsLabel));
        }


        private void markSelectedTab(Button i_SelectedTab)
        {
            foreach(var kvPair in pageCollection)
            {
                if (kvPair.Value.Item2 != i_SelectedTab)
                {
                    if (kvPair.Value.Item4 != null)
                        kvPair.Value.Item4.BackgroundColor = Color.Transparent;
                }
                else
                    kvPair.Value.Item4.BackgroundColor = Color.FromHex(k_TabSelectedColor);
            }
        }


        void FilterStateIndicator_Tapped(object i_Sender, EventArgs i_EventArgs)
        {
            Navigation.PushModalAsync(m_FilterPage);
        }

        void FooterBarEvents_Clicked(object i_Sender, EventArgs i_EventArgs)
        {
            PlaceHolder.Content = pageCollection["EventsPage"].Item1.Content;
            markSelectedTab(i_Sender as Button);
        }
        
        void FooterBarCameras_Clicked(object i_Sender, EventArgs i_EventArgs)
        {
            PlaceHolder.Content = pageCollection["CamerasPage"].Item1.Content;
            markSelectedTab(i_Sender as Button);
        }

        void FooterBarHealth_Clicked(object i_Sender, EventArgs i_EventArgs)
        {
            markSelectedTab(i_Sender as Button);
        }

        void FooterBarSettings_Clicked(object i_Sender, EventArgs i_EventArgs)
        {
            PlaceHolder.Content = pageCollection["SettingsPage"].Item1.Content;
            markSelectedTab(i_Sender as Button);
        }

        protected override bool OnBackButtonPressed()
        {
            //Handle back button pressed in MainPage
            return true;
        }
    }
}