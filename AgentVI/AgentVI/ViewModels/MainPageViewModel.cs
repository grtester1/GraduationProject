using AgentVI.Models;
using AgentVI.Services;
using AgentVI.Utils;
using FFImageLoading.Svg.Forms;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using AgentVI.Interfaces;
using AgentVI.Views;
using Plugin.DeviceOrientation;
using Plugin.DeviceOrientation.Abstractions;
using EContentUpdateType = AgentVI.Utils.UpdatedContentEventArgs.EContentUpdateType;

namespace AgentVI.ViewModels
{
    public partial class MainPageViewModel
    {
        public MainPageViewModel(IProgress<ProgressReportModel> i_ProgressReporter)
        {
            waitingPage = new WaitingPage();
            progressReporter = i_ProgressReporter;
            ProgressReportModel report = new ProgressReportModel(k_NumberOfInitializations);

            updateReporter("Initializing filter...", report);
            FilterIndicator = new FilterIndicatorViewModel();
            FilterPage = new FilterPage(FilterIndicator);
            updateReporter("Filter initialized correctly", report);

            updateReporter("Fetching account...", report);
            LoginContext = new LoginPageViewModel();
            LoginContext.InitializeFields(ServiceManager.Instance.LoginService.LoggedInUser);
            updateReporter("Account initialized.", report);

            initPagesCollectionHelper(report);
        }

        private void initPagesCollectionHelper(ProgressReportModel i_Report)
        {
            initializeTabIcons();
            updateReporter("Initializing app pages...", i_Report);
            PagesCollection = new Dictionary<EAppTab, Tuple<ContentPage, SvgCachedImage>>();

            updateReporter("Fetching Cameras...", i_Report);
            CamerasPage camerasPageInstance = new CamerasPage();
            camerasPageInstance.RaiseContentViewUpdateEvent += OnContentViewUpdateEvent;
            PagesCollection.Add(EAppTab.SensorsPage, new Tuple<ContentPage, SvgCachedImage>(camerasPageInstance, FooterBarCamerasIcon));

            updateReporter("Fetching Settings...", i_Report);
            PagesCollection.Add(EAppTab.SettingsPage, new Tuple<ContentPage, SvgCachedImage>(new SettingsPage(), FooterBarSettingsIcon));

            updateReporter("Fetching Events...", i_Report);
            EventsPage eventsPageBuf = new EventsPage();
            eventsPageBuf.RaiseContentViewUpdateEvent += OnContentViewUpdateEvent;
            PagesCollection.Add(EAppTab.EventsPage, new Tuple<ContentPage, SvgCachedImage>(eventsPageBuf, FooterBarEventsIcon));
        }

        private void addToContentViewStack(ContentPage i_updatedContent)
        {
            if (contentViewStack == null)
            {
                contentViewStack = new Stack<ContentPage>();
            }
            contentViewStack.Push(i_updatedContent);
        }

        private void resetContentViewStack()
        {
            contentViewStack = null;
            CrossDeviceOrientation.Current.LockOrientation(DeviceOrientations.Portrait);
        }

        private void popFromControlViewStack()
        {
            ContentPage stackTop = contentViewStack.Pop();
            if (stackTop != null)
            {
                currentPageInContentView = stackTop;
                ContentView = stackTop.Content;
            }
            else
            {
                throw new Exception("MainPage.PopFromControlViewStack called unexpectedely");
            }
        }

        private void markSelectedTab(EAppTab i_SelectedTab)
        {
            PagesCollection[i_SelectedTab].Item2.Source = EnumToSVGPath(i_SelectedTab, ESelection.Active);
            foreach (KeyValuePair<EAppTab, Tuple<ContentPage, SvgCachedImage>> kvPair in PagesCollection)
            {
                if (kvPair.Key != i_SelectedTab)
                {
                    kvPair.Value.Item2.Source = EnumToSVGPath(kvPair.Key, ESelection.Passive);
                }
            }
        }

        private void updateReporter(string i_StageCompleted, ProgressReportModel i_Report)
        {
            if (progressReporter != null && i_Report != null)
            {
                i_Report.Progress();
                i_Report.AddStage(i_StageCompleted);
                progressReporter.Report(i_Report);
            }
        }

        private void OnContentViewUpdateEvent(object sender, UpdatedContentEventArgs e)
        {
            lock (contentViewUpdateLock)
            {
                if (e == null || e.ContentUpdateType == EContentUpdateType.Buffering)
                {
                    addToContentViewStack(currentPageInContentView);
                    ContentView = waitingPage.Content;
                }
                else if (e.ContentUpdateType == EContentUpdateType.Pop)
                {
                    popFromControlViewStack();
                }
                else if (e.ContentUpdateType == EContentUpdateType.PushAsync)
                {
                    addToContentViewStack(currentPageInContentView);
                    try
                    {
                        (e.UpdatedContent as IFocusable).Refocus();
                    }
                    catch (NullReferenceException ex) { Console.WriteLine(ex.Message); }
                    Navigation.PushAsync(e.UpdatedContent);
                }
                else if (e.ContentUpdateType == EContentUpdateType.PopAsync)
                {
                    Navigation.PopAsync();
                    ContentPage stackTop = contentViewStack.Pop();
                    try
                    {
                        (stackTop as IFocusable).Refocus();
                    }
                    catch (NullReferenceException ex) { Console.WriteLine(ex.Message); }
                }
                else                                        //UpdatedContentEventArgs.EContentUpdateType.Push
                {
                    currentPageInContentView = e.UpdatedContent;
                    ContentView = currentPageInContentView.Content;
                }
            }
        }
        //--------------------------------------------------------------------------------------------------------------

        private void initializeTabIcons()
        {
            FooterBarEventsIcon = new SvgCachedImage();
            FooterBarEventsIcon.Source = Settings.EventsTabSVGPath;

            FooterBarCamerasIcon = new SvgCachedImage();
            FooterBarCamerasIcon.Source = Settings.SensorsTabSVGPath;

            FooterBarHealthIcon = new SvgCachedImage();
            FooterBarHealthIcon.Source = Settings.SettingsTabSVGPath;

            FooterBarSettingsIcon = new SvgCachedImage();
            FooterBarSettingsIcon.Source = Settings.SettingsTabSVGPath;
        }

        private string EnumToSVGPath(EAppTab i_TabPage, ESelection i_SelectionStatus)
        {
            string res;

            if (i_TabPage == EAppTab.EventsPage && i_SelectionStatus == ESelection.Active)
            {
                res = Settings.EventsTabSelectedSVGPath;
            }
            else if (i_TabPage == EAppTab.EventsPage && i_SelectionStatus == ESelection.Passive)
            {
                res = Settings.EventsTabSVGPath;
            }
            else if (i_TabPage == EAppTab.SensorsPage && i_SelectionStatus == ESelection.Active)
            {
                res = Settings.SensorsTabSelectedSVGPath;
            }
            else if (i_TabPage == EAppTab.SensorsPage && i_SelectionStatus == ESelection.Passive)
            {
                res = Settings.SensorsTabSVGPath;
            }
            else if (i_TabPage == EAppTab.SettingsPage && i_SelectionStatus == ESelection.Active)
            {
                res = Settings.SettingsTabSelectedSVGPath;
            }
            else if (i_TabPage == EAppTab.SettingsPage && i_SelectionStatus == ESelection.Passive)
            {
                res = Settings.SettingsTabSVGPath;
            }
            else
            {
                res = null;
            }

            return res;
        }
    }
}
