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
        }

        protected override async void OnAppearing()
        {
			base.OnAppearing();
            await Task.Factory.StartNew(setNextPage);
        }

		private async void setNextPage()
		{
            //bool isServerAvailable = true;
            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += ReportProgress;

            if (ServiceManager.Instance.LoginService.DoCredentialsExist())
            {
                //try
                //{
                    LoginResult loginResult = InnoviApiService.Connect(ServiceManager.Instance.LoginService.AccessToken);
                //}catch(ServerTimeOutException e)
                //{
                //    isServerAvailable = false;
                //    PopupMessage explaining the error;
                //}


                if (loginResult.ErrorMessage == LoginResult.eErrorMessage.Empty)
                {
                    ServiceManager.Instance.LoginService.InitServiceModule(loginResult.User);
                    MainPage mainAppPage = null;
                    await Task.Factory.StartNew(() =>
                        {
                            mainAppPage = new MainPage(progress);
                        }
                                                );
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