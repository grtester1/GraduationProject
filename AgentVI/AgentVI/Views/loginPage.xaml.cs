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
                bool isUserExistInDataBase = xxx(userName, password); ////// API FUNCTION

                if (isUserExistInDataBase)
                {
                    string userId = yyy(userName, password); ////// API FUNCTION
                    Navigation.PushAsync(new MainPage(userId));
                }
                else
                {
                    DisplayAlert("Login Error", "The User Name or Password entered is incorrect. Please try again.", "OK");
                }
            }


        }

        private bool xxx(string i_userName, string i_password) ///// for testing
        {
            return true;
        }

        private string yyy(string i_userName, string i_password) ///// for testing
        {
            return "1";
        }


        private void forgotPasswordButton_clicked(object sender, EventArgs e)
        {
            DisplayAlert("Forgot Password", "Forgot password message text", "OK");
        }

    }
}





/*
namespace AgentVI
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class loginPage : ContentPage
	{
        ImageSource image = ImageSource.FromResource("AgentVI.additional_resources.innovi_logo.png");
        
		public loginPage ()
		{
			InitializeComponent();
		}


        private void loginButton_clicked(object sender, EventArgs e)
        {
            DisplayAlert("Login Message", "Login message text / Login error text", "OK");
        }

        private void forgotPasswordButton_clicked(object sender, EventArgs e)
        {
            DisplayAlert("Forgot Password", "Forgot password message text", "OK");
        }

        private void bananaButtonClicked(object sender, EventArgs e)
        {
<<<<<<< HEAD
            InnoviApiProxy.User.Login("goldami1@gmail.com", "password");
=======

            AgentVIProxy.User.Login("goldami1@gmail.com", "password");
>>>>>>> 414abed5b6abaf599de4786aba251066bd245ae6
            //bananaField.Text = AgentVIProxy.Class1.foo();
        }

        private void tapuahButtonClicked(object sender, EventArgs e)
        {
            //tapuahField.Text = AgentVIProxy.Class1.goo();
        }

    }
}*/