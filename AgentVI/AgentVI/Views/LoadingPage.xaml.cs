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
        public LoadingPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
			base.OnAppearing();
			setNextPage();
        }

		private async void setNextPage()
		{
			await ProgressBarLine.ProgressTo(0.1, 1000, Easing.SinIn);

            if (ServiceManager.Instance.LoginService.DoCredentialsExist()) //if the user is saved
            {
                LoginResult loginResult = User.Connect(ServiceManager.Instance.LoginService.AccessToken);
                //LoginResult loginResult = User.Login("gilgilronen@gmail.com", "password");

                await ProgressBarLine.ProgressTo(0.7, 2000, Easing.CubicIn);

                if (loginResult.ErrorMessage == LoginResult.eErrorMessage.Empty) //if the access token is valid and not expired
                {
                    ServiceManager.Instance.LoginService.setLoggedInUser(loginResult.User);
                    await ProgressBarLine.ProgressTo(1, 2000, Easing.Linear);
                    Navigation.InsertPageBefore(new MainPage(), this);
                    await Navigation.PopAsync();
                }
                else
                {
                    await ProgressBarLine.ProgressTo(1, 1000, Easing.Linear);
                    Navigation.InsertPageBefore(new LoginPage(), this);
                    await Navigation.PopAsync();
                    //await Navigation.PushAsync(new LoginPage());
                }
            }
            else
            {
                await ProgressBarLine.ProgressTo(1, 1000, Easing.Linear);
                Navigation.InsertPageBefore(new LoginPage(), this);
                await Navigation.PopAsync();
                //await Navigation.PushAsync(new LoginPage());
            }
		}
    }
}