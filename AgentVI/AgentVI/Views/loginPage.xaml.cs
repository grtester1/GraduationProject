using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AgentVI.ViewModels;
using AgentVI.additionals;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Net;

namespace AgentVI
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class loginPage : ContentPage
    {
        //ImageSource image = ImageSource.FromResource("AgentVI.additional_resources.innovi_logo.png");

        public loginPage()
        {
            InitializeComponent();

            var assembly = typeof(loginPage);
            backgroundImage.Source = ImageSource.FromResource("AgentVI.Views.Assets.Images.background.png", assembly);
            logoImage.Source = ImageSource.FromResource("AgentVI.Views.Assets.Images.innovi_logo_update.png", assembly);
        }

        private void loginButton_clicked(object sender, EventArgs e)
        {
            string userName = UserNameEntry.Text;
            string password = PasswordEntry.Text;
            bool isUserNameEmpty = string.IsNullOrEmpty(userName);
            bool isPasswordEmpty = string.IsNullOrEmpty(password);

            if (isUserNameEmpty || isPasswordEmpty)
            {
                DisplayAlert("Login Error", "Please enter your user name and your password.", "OK");
            }
            else
            {
                bool isUserExistInDataBase = authenticateUser(userName, password);

                if (isUserExistInDataBase)
                {
                    string userId = getUserID(userName, password);
                    Navigation.PushAsync(new MainPage(userId));
                }
                else
                {
                    DisplayAlert("Login Error", "The User Name or Password entered is incorrect. Please try again.", "OK");
                }
            }
        }

        private bool authenticateUser(string i_userName, string i_password) ///// for testing
        {
            //InnoviApiProxy.LoginResult loginResult = InnoviApiProxy.User.Login(i_userName, i_password);
            return true;
        }

        private string getUserID(string i_userName, string i_password) ///// for testing
        {
            //InnoviApiProxy.LoginResult loginResult = InnoviApiProxy.User.Login(i_userName, i_password);
            return "1";
        }

        private void forgotPasswordButton_clicked(object sender, EventArgs e)
        {
            DisplayAlert("Forgot Password", "Please go to www.gentVI.com and contact us.", "OK");
        }
    }
}