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

        void loginButton_Clicked(object sender, EventArgs e)
        {
            loadingData.IsRunning = true;
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;
            bool isUsernameEmpty = string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username);
            bool isPasswordEmpty = string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password);
            if (isUsernameEmpty || isPasswordEmpty)
            {
                DisplayAlert("Login Error", "Please enter your username and password.", "Retry");
            }
            else
            {
                if (m_loginResult == null)
                {
                    try
                    {
                        m_loginResult = User.Login(username, password);
                    }
                    catch (Exception ex)
                    {
                        DisplayAlert("Exception", ex.Message, "Close");
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
                        Navigation.PopAsync();
                    }
                    else
                    {
                        DisplayAlert("Error Message", m_loginResult.ErrorMessage.convertEnumToString(), "retry");
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