#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Collections.Generic;
using AgentVI.Services;
using Xamarin.Forms;

namespace AgentVI.Views
{
    public partial class SettingsPage : ContentPage
    {
		string numOfCameras = "<nums>";
		string siteName = "<Site name>";
		string networkDatacomSolutions = "<Network Datacom Solutions>";
		                                                                                                           
        public SettingsPage()
        {
            InitializeComponent();
            
			numOfCameras = "<123>";
            siteName = "<xyz>";
            networkDatacomSolutions = "<abc>";

            ArmDisarmSwitch.IsToggled = ServiceManager.Instance.LoginService.ArmCamersSettings;
            NotificationsSwitch.IsToggled = ServiceManager.Instance.LoginService.PushNotificationsSettings;

            if (ArmDisarmSwitch.IsToggled)
            {
				DescriptionArmDisarm.Text = numOfCameras + " cameras of " + networkDatacomSolutions + ", " + siteName + " are Armed.";
            }
            else
            {
                DescriptionArmDisarm.Text = "Disarmed.";
            }

            if (NotificationsSwitch.IsToggled)
            {
				DescriptionNotifications.Text = "You will receive push notifications for " + networkDatacomSolutions + ", " + siteName + ".";
            }
            else
            {
                DescriptionNotifications.Text = "Push notifications is off.";
            }
        }

        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            ServiceManager.Instance.LoginService.DeleteCredentials();
			InnoviApiService.Logout();
            //await Navigation.PushAsync(new LoginPage());
            //Navigation.InsertPageBefore(new LoginPage(), this);
            //await Navigation.PopAsync();
            //await Navigation.PopToRootAsync();
            //await Navigation.PushAsync(new LoginPage());
            //await Navigation.PushModalAsync(new LoginPage());
        }

        void arm_Toggled(object sender, EventArgs e)
        {
            ServiceManager.Instance.LoginService.ArmCamersSettings = ArmDisarmSwitch.IsToggled;
            if (ArmDisarmSwitch.IsToggled)
            {
				DescriptionArmDisarm.Text = numOfCameras + " cameras of " + networkDatacomSolutions + ", " + siteName + " are Armed.";
            }
            else
            {
                DescriptionArmDisarm.Text = "Disarmed.";
            }
        }

        void Notifications_Toggled(object sender, EventArgs e)
        {
            ServiceManager.Instance.LoginService.PushNotificationsSettings = NotificationsSwitch.IsToggled;
            if (NotificationsSwitch.IsToggled)
            {
				DescriptionNotifications.Text = "You will receive push notifications for " + networkDatacomSolutions + ", " + siteName + ".";
            }
            else
            {
                DescriptionNotifications.Text = "Push notifications is off.";
            }
        }
    }
}