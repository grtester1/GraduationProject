using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InnoviApiProxy;
//<debugAmi>using DummyProxy;
using AgentVI.Utils;
using AgentVI.ViewModels;
using AgentVI.Services;

namespace AgentVI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private LoginPageViewModel m_LoginPageViewModel = null;
        private LoginResult m_LoginResult = null;

        public LoginPage()
        {
            InitializeComponent();
            usernameEntry.Completed += (s, e) => passwordEntry.Focus();
            passwordEntry.Completed += (s, e) => loginButton_Clicked(s, e);
            NavigationPage.SetHasNavigationBar(this, false);
        }

        void loginButton_Clicked(object i_Sender, EventArgs i_EventArgs)
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
                if (m_LoginResult == null)
                {
                    try
                    {
						m_LoginResult = InnoviApiService.Login(username, password);
                    }
                    catch (Exception ex)
                    {
                        DisplayAlert("Exception", ex.Message, "Close");
                    }
                }
                if (m_LoginResult != null)
                {
                    if (m_LoginResult.ErrorMessage == LoginResult.eErrorMessage.Empty)
                    {
                        m_LoginPageViewModel = new LoginPageViewModel();
                        m_LoginPageViewModel.InitializeFields(m_LoginResult.User);
						ServiceManager.Instance.LoginService.SaveCredentials(m_LoginPageViewModel.AccessToken);
						Navigation.InsertPageBefore(new MainPage(), this);
                        Navigation.PopAsync();
                    }
                    else
                    {
                        DisplayAlert("Error Message", m_LoginResult.ErrorMessage.convertEnumToString(), "retry");
                        m_LoginResult = null;
                    }
                }
            }
            loadingData.IsRunning = false;
        }

        async void forgotPwdButton_Clicked(object i_Sender, EventArgs i_EventArgs)
        {
            var response = await DisplayActionSheet("forgot your password?", "Cancel", "Delete", "Copy Link", "Duplicate Link");
            await DisplayAlert("Response", response, "OK");
        }
    }
}