using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AgentVI.ViewModels;

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

/*
        private void testerMethod(object sender, EventArgs e)
        {
            DisplayAlert("TEST", AgentVI.pages.MainMenu, "OK");
        }
*/

        private void forgotPasswordButton_clicked(object sender, EventArgs e)
        {
            DisplayAlert("Forgot Password", "Forgot password message text", "OK");
        }

        private void bananaButtonClicked(object sender, EventArgs e)
        {
            //bananaField.Text = innoviApiProxy.Class1.banana();
        }

        private void tapuahButtonClicked(object sender, EventArgs e)
        {
            //tapuahField.Text = innoviApiProxy.Class1.tapuah();
        }

    }
}