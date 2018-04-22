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
		public LoginPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        void loginButton_Clicked(object sender, EventArgs e)
        {
            LoginResult m_loginResult = User.Login(usernameEntry.Text, passwordEntry.Text);
            if(m_loginResult.ErrorMessage == LoginResult.eErrorMessage.Empty)
            {
                ViewModels.TemporaryShit.getInstance().Username = m_loginResult.User.Username;
                ViewModels.TemporaryShit.getInstance().LoggedInUser = m_loginResult.User;
                ViewModels.TemporaryShit.getInstance().InitializeFields();
                Navigation.PushModalAsync(new MainPage());
            }
            else
            {
                DisplayAlert("Error Message", m_loginResult.ErrorMessage.toString() , "Retry");
            }
        }

        async void forgotPwdButton_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayActionSheet("Title", "Cancel", "Delete", "Copy Link", "Duplicate Link");
            await DisplayAlert("Response", response, "OK");
        }

    }
}