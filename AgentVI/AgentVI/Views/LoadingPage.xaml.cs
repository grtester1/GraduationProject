using System;
using System.Collections.Generic;
using AgentVI.Services;
using Xamarin.Forms;
using InnoviApiProxy;
using AgentVI.ViewModels;
namespace AgentVI.Views
{
    public partial class LoadingPage : ContentPage
    {
        //public static ICredentialsService CredentialsService { get; private set; }

        public LoadingPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await ProgressBarLine.ProgressTo(1, 3000, Easing.CubicInOut);

            if (ServiceManager.Instance.LoginService.DoCredentialsExist())
            {
                //ServiceManager.Instance.LoginService.------------------------------------------------------------------------------
                LoginResult loginResult = User.Connect(ServiceManager.Instance.LoginService.AccessToken);

                if (loginResult.ErrorMessage == LoginResult.eErrorMessage.Empty)
                {
                    //LoginPageViewModel loginPageViewModel = new LoginPageViewModel();
                    //loginPageViewModel.InitializeFields(loginResult.User);

                    ServiceManager.Instance.LoginService.setLoggedInUser(loginResult.User);
                    Navigation.InsertPageBefore(new MainPage(), this);
                    await Navigation.PopAsync();
                    //await Navigation.PushAsync(new MainPage());
                }
            }
            else
            {
                await Navigation.PushAsync(new LoginPage());
            }
        }
    }
}