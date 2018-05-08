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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await ProgressBarLine.ProgressTo(0.1, 1000, Easing.SinIn);

            if (ServiceManager.Instance.LoginService.DoCredentialsExist())
            {
                //string accessToken = "eyJhbGciOiJIUzI1NiJ9.eyJhY2NvdW50SWQiOiI2Iiwicm9sZSI6IkFETUlOIiwidXNlclN0YXR1cyI6IkFDVElWRSIsInVzZXJUeXBlIjoiVVNFUiIsImV4cCI6MTUyNjQ5NjE0OSwidXNlcklkIjoiNTU1In0._Z8l175eiAEPYHIvOMTRDL16cUq48s8Xws5zmUlwyFc";
                LoginResult loginResult = User.Connect(ServiceManager.Instance.LoginService.AccessToken);
                //LoginResult loginResult = User.Login("gilgilronen@gmail.com", "password");

                await ProgressBarLine.ProgressTo(0.7, 2000, Easing.CubicIn);

                if (loginResult.ErrorMessage == LoginResult.eErrorMessage.Empty)
                {
                    ServiceManager.Instance.LoginService.setLoggedInUser(loginResult.User);
                    await ProgressBarLine.ProgressTo(1, 2000, Easing.Linear);
                    Navigation.InsertPageBefore(new MainPage(), this);
                    await Navigation.PopAsync();
                    //await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    await ProgressBarLine.ProgressTo(1, 1000, Easing.Linear);
                    await Navigation.PushAsync(new LoginPage());
                }
            }
            else
            {
                await ProgressBarLine.ProgressTo(1, 1000, Easing.Linear);
                await Navigation.PushAsync(new LoginPage());
            }




            /*
            if (ServiceManager.Instance.LoginService.DoCredentialsExist())
            {
                await ProgressBarLine.ProgressTo(0.4, 2000, Easing.SinIn);

                string accessToken = "eyJhbGciOiJIUzI1NiJ9.eyJhY2NvdW50SWQiOiI2Iiwicm9sZSI6IkFETUlOIiwidXNlclN0YXR1cyI6IkFDVElWRSIsInVzZXJUeXBlIjoiVVNFUiIsImV4cCI6MTUyNjQ5NjE0OSwidXNlcklkIjoiNTU1In0._Z8l175eiAEPYHIvOMTRDL16cUq48s8Xws5zmUlwyFc";

                LoginResult loginResult = User.Connect(accessToken);
                //LoginResult loginResult = User.Login("gilgilronen@gmail.com", "password");

                await ProgressBarLine.ProgressTo(0.7, 2000, Easing.CubicIn);

                if (loginResult.ErrorMessage == LoginResult.eErrorMessage.Empty)
                {
                    ServiceManager.Instance.LoginService.setLoggedInUser(loginResult.User);
                    await ProgressBarLine.ProgressTo(1, 2000, Easing.Linear);
                    Navigation.InsertPageBefore(new MainPage(), this);//-----------
                    await Navigation.PopAsync();
                    //await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    await ProgressBarLine.ProgressTo(1, 1000, Easing.Linear);
                    await Navigation.PushAsync(new LoginPage());
                }
            }
            else
            {
                await ProgressBarLine.ProgressTo(1, 500, Easing.CubicInOut);
                await Navigation.PushAsync(new LoginPage());
            }*/
        }
    }
}