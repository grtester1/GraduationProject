using AgentVI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AgentVI.Views
{
    public partial class MainPage : ContentPage
    {
        private LoginPageViewModel m_VM = null;

        void FooterBarEvents_Clicked(object sender, EventArgs e)
        {
            PlaceHolder.Content = (new EventsPage()).Content;
        }
        void FooterBarCameras_Clicked(object sender, EventArgs e)
        {
            PlaceHolder.Content = (new CamerasPage()).Content;
        }
        void FooterBarHealth_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new FilterPage());
        }
        void FooterBarSettings_Clicked(object sender, EventArgs e)
        {
            PlaceHolder.Content = (new SettingsPage()).Content;
        }


        protected override bool OnBackButtonPressed()
        {
            return true;
        }


        public MainPage()
        {
            InitializeComponent();
            m_VM = new LoginPageViewModel();
            m_VM.InitializeFields(Services.LoginService.Instance.LoggedInUser);
            BindingContext = m_VM;
        }
    }
}