#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AgentVI.Utils;
using AgentVI.ViewModels;
using AgentVI.Services;
using System.Threading.Tasks;

namespace AgentVI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private LoginPageViewModel loginPageViewModel { get; set; }

        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            loginPageViewModel = new LoginPageViewModel();
            BindingContext = loginPageViewModel;
        }

        private async void onLoginButtonClicked(object i_Sender, EventArgs i_EventArgs)
        {
            try
            {
                loginPageViewModel.IsBusyLoading = true;
                await Task.Factory.StartNew(() => loginPageViewModel.TryLogin());
                loginPageViewModel.IsBusyLoading = false;
                await Navigation.PushAsync(new LoadingPage());
            }
            catch (ArgumentException e)
            {
                await DisplayAlert("Error", e.Message, "Retry");
            }
            catch (Exception e)
            {
                await DisplayAlert("Critical!", e.Message, "Contact Developer");
            }
            finally
            {
                loginPageViewModel.IsBusyLoading = false;
            }
        }

        async void onForgotPasswordButtonTapped(object i_Sender, EventArgs i_EventArgs)
        {
            var response = await DisplayActionSheet("forgot your password?", "Cancel", null, "TODO: Implement action for password reset");
            await DisplayAlert("Response", response, "OK");
        }

        private void onUsernameEntryCompleted(object sender, EventArgs e)
        {
            passwordEntry.Focus();
        }

        private void onPasswordEntryCompleted(object sender, EventArgs e)
        {
            onLoginButtonClicked(sender, e);
        }
    }
}