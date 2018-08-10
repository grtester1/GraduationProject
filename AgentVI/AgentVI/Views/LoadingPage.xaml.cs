
using DummyProxy;

//using InnoviApiProxy;

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
        }

        protected override async void OnAppearing()
        {
			base.OnAppearing();
            await Task.Factory.StartNew(setNextPage);
        }

		private async void setNextPage()
		{
            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += ReportProgress;

            if (ServiceManager.Instance.LoginService.DoCredentialsExist())
            {
				LoginResult loginResult = InnoviApiService.Connect(ServiceManager.Instance.LoginService.AccessToken);

                if (loginResult.ErrorMessage == LoginResult.eErrorMessage.Empty)
                {
                    ServiceManager.Instance.LoginService.setLoggedInUser(loginResult.User);
                    MainPage mainAppPage = null;
                    await Task.Factory.StartNew(() => mainAppPage = new MainPage(progress));
                    Device.BeginInvokeOnMainThread(()=>Navigation.PushAsync(mainAppPage));
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() => Navigation.PushAsync(new LoginPage()));
                }
            }
            else
            {
                Device.BeginInvokeOnMainThread(() => Navigation.PushAsync(new LoginPage()));
            }
		}

        private async void ReportProgress(object sender, ProgressReportModel e)
        {
            if (e != null)
                await LoadingProgressBar.ProgressTo(e.PercentageComplete, 1000, Easing.Linear);
        }
    }
}