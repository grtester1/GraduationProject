#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Collections.Generic;
using AgentVI.Services;
using Xamarin.Forms;
using AgentVI.ViewModels;
using AgentVI.Models;
using System.Threading.Tasks;

namespace AgentVI.Views
{
    public partial class LoadingPage : ContentPage
    {
        public LoadingPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //Task.Factory.StartNew(setNextPage);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(500);
            await Task.Factory.StartNew(setNextPage);
            //check if 3g is enabled
        }

        private void setNextPage()
        {
            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += ReportProgress;

            //ServiceManager.Instance.LoginService.DeleteCredentials();
            if (ServiceManager.Instance.LoginService.DoCredentialsExist())
            {
                LoginResult loginResult = InnoviApiService.Connect(ServiceManager.Instance.LoginService.AccessToken);

                if (loginResult.ErrorMessage == LoginResult.eErrorMessage.Empty ||
                    loginResult.ErrorMessage == LoginResult.eErrorMessage.LoggedInUserAlreadyExists)
                {
                    loadMainPage(loginResult.User, progress);
                }
                else
                {
                    loadLoginPage();
                }
            }
            else
            {
                loadLoginPage();
            }
        }

        private void loadLoginPage()
        {
            Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new LoginPage()));
        }

        private async void loadMainPage(User i_User, Progress<ProgressReportModel> i_Progress)
        {
            ServiceManager.Instance.LoginService.InitServiceModule(i_User);
            MainPage mainAppPage = null;
            await Task.Factory.StartNew(() =>
            {
                mainAppPage = new MainPage(i_Progress);
            }
                                        );
            Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(mainAppPage));
        }

        private async void ReportProgress(object sender, ProgressReportModel e)
        {
            if (e != null)
            {
                await LoadingProgressBar.ProgressTo(e.PercentageComplete, 1000, Easing.Linear);
            }
        }
    }
}