using AgentVI.Models;
using AgentVI.Services;
using AgentVI.Utils;
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
        private IProgress<ProgressReportModel> m_ProgressReporter = null;

        private Dictionary<String, Tuple<ContentPage, Button, Image, Label>> pageCollection;
        private const String k_TabSelectionColor = "bababa";
        private const short k_NumberOfInitializations = 8;


        public MainPage(IProgress<ProgressReportModel> i_ProgressReporter)
        {
            m_ProgressReporter = i_ProgressReporter;
            ProgressReportModel report = new ProgressReportModel(k_NumberOfInitializations);

            updateReporter("Initializing filter...", report);
            m_FilterIndicatorViewModel = new FilterIndicatorViewModel();
            m_FilterPage = new FilterPage(m_FilterIndicatorViewModel);
            InitializeComponent();
            FilterStateIndicatorListView.BindingContext = m_FilterIndicatorViewModel;
            updateReporter("Filter initialized correctly", report);

            updateReporter("Fetching account...", report);
            m_LoginPageViewModel = new LoginPageViewModel();
            m_LoginPageViewModel.InitializeFields(ServiceManager.Instance.LoginService.LoggedInUser);
            BindingContext = m_LoginPageViewModel;
            updateReporter("Account initialized.", report);

            initPagesCollectionHelper(report);

            NavigationPage.SetHasNavigationBar(this, false);

            PlaceHolder.Content = pageCollection["EventsPage"].Item1.Content;
            markSelectedTab(pageCollection["EventsPage"].Item2);
        }

        private void updateReporter(String i_StageCompleted, ProgressReportModel i_Report)
        {
            if(m_ProgressReporter != null && i_Report != null)
            {
                i_Report.Progress();
                i_Report.AddStage(i_StageCompleted);
                m_ProgressReporter.Report(i_Report);
            }
        }

        private void initPagesCollectionHelper(ProgressReportModel i_Report)
        {
            updateReporter("Initializing app pages...", i_Report);
            pageCollection = new Dictionary<String, Tuple<ContentPage, Button, Image, Label>>();

            updateReporter("Fetching Cameras...", i_Report);
            pageCollection.Add("CamerasPage", new Tuple<ContentPage, Button, Image, Label>(new CamerasPage(), FooterBarCamerasButton, FooterBarCamerasImage, FooterBarCamerasLabel));

            updateReporter("Fetching Settings...", i_Report);
            pageCollection.Add("SettingsPage", new Tuple<ContentPage, Button, Image, Label>(new SettingsPage(), FooterBarSettingsButton, FooterBarSettingsImage, FooterBarSettingsLabel));

            updateReporter("Fetching Events...", i_Report);
            pageCollection.Add("EventsPage", new Tuple<ContentPage, Button, Image, Label>(new EventsPage(), FooterBarEventsButton, FooterBarEventsImage, FooterBarEventsLabel));
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
                    kvPair.Value.Item4.BackgroundColor = Color.FromHex(k_TabSelectionColor);
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
            DependencyService.Get<IBackButtonPressed>().NativeOnBackButtonPressed();
            return true;
        }
    }
}