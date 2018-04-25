using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InnoviApiProxy;

namespace AgentVI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginResult m_loginResult = null;

        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        void loginButton_Clicked(object sender, EventArgs e)
        {
            string userName = usernameEntry.Text;
            string password = passwordEntry.Text;
            bool isUserNameEmpty = string.IsNullOrEmpty(userName);
            bool isPasswordEmpty = string.IsNullOrEmpty(password);

            if (isUserNameEmpty || isPasswordEmpty)
            {
                DisplayAlert("Login Error", "Please enter your user name and your password.", "OK");
            }
            else
            {
                if (m_loginResult == null)
                {
                    try
                    {
                        m_loginResult = User.Login(userName, password);
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
                        ViewModels.TemporaryShit.getInstance().Username = m_loginResult.User.Username;
                        ViewModels.TemporaryShit.getInstance().LoggedInUser = m_loginResult.User;
                        ViewModels.TemporaryShit.getInstance().InitializeFields();
                        Navigation.PushModalAsync(new MainPage());
                    }
                    else
                    {
                        DisplayAlert("Login Error", m_loginResult.ErrorMessage.toString(), "OK");
                        m_loginResult = null;
                    }
                }
            }
        }

        async void forgotPwdButton_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayActionSheet("Forgot your password?", "Cancel", "Delete", "Copy Link", "Duplicate Link");
            await DisplayAlert("Response", response, "OK");
        }

    }
}