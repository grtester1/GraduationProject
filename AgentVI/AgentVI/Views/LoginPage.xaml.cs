using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InnoviApiProxy;
using AgentVI.Utils;
using AgentVI.ViewModels;
using AgentVI.Services;

namespace AgentVI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private LoginPageViewModel LoginPageViewModel = null;
        private LoginResult m_loginResult = null;

        public LoginPage()
        {
            InitializeComponent();
            usernameEntry.Completed += (s, e) => passwordEntry.Focus();
            passwordEntry.Completed += (s, e) => loginButton_Clicked(s, e);
            NavigationPage.SetHasNavigationBar(this, false);
        }

        async void loginButton_Clicked(object sender, EventArgs e)
        {
            loadingData.IsRunning = true;
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;
            bool isUsernameEmpty = string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username);
            bool isPasswordEmpty = string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password);
            if (isUsernameEmpty || isPasswordEmpty)
            {
                await DisplayAlert("Login Error", "Please enter your username and password.", "Retry");
            }
            else
            {
                if (m_loginResult == null)
                {
                    try
                    {
                        //m_loginResult = User.Login(username, password);
                        //
                        //
                        //
                        //
                        //
                        string accessToken = "eyJhbGciOiJIUzI1NiJ9.eyJhY2NvdW50SWQiOiI2Iiwicm9sZSI6IkFETUlOIiwidXNlclN0YXR1cyI6IkFDVElWRSIsInVzZXJUeXBlIjoiVVNFUiIsImV4cCI6MTUyNjQ5NjE0OSwidXNlcklkIjoiNTU1In0._Z8l175eiAEPYHIvOMTRDL16cUq48s8Xws5zmUlwyFc";

                        m_loginResult = User.Connect(accessToken);


                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Exception", ex.Message, "Close");
                    }
                }
                if (m_loginResult != null)
                {
                    if (m_loginResult.ErrorMessage == LoginResult.eErrorMessage.Empty)
                    {
                        LoginPageViewModel = new LoginPageViewModel();
                        LoginPageViewModel.InitializeFields(m_loginResult.User);
                        ServiceManager.Instance.LoginService.SaveCredentials(LoginPageViewModel.AccessToken);
                        Navigation.InsertPageBefore(new MainPage(), this);
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Error Message", m_loginResult.ErrorMessage.convertEnumToString(), "retry");
                        m_loginResult = null;
                    }
                }
            }
            loadingData.IsRunning = false;
        }

        async void forgotPwdButton_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayActionSheet("forgot your password?", "Cancel", "Delete", "Copy Link", "Duplicate Link");
            await DisplayAlert("Response", response, "OK");
        }
    }
}