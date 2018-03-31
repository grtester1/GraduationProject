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
}