using System;
using System.Collections.Generic;
using AgentVI.Services;
using Xamarin.Forms;

namespace AgentVI.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            ArmDisarmSwitch.IsToggled = ServiceManager.Instance.LoginService.ArmCamersSettings;
            NotificationsSwitch.IsToggled = ServiceManager.Instance.LoginService.PushNotificationsSettings;

            if (ArmDisarmSwitch.IsToggled)
            {
                DescriptionArmDisarm.Text = "<nums> cameras of <Network Datacom Solutions>, <Site name> are Armed.";
            }
            else
            {
                DescriptionArmDisarm.Text = "Disarmed.";
            }

            if (NotificationsSwitch.IsToggled)
            {
                DescriptionNotifications.Text = "You will receive push notifications for <Network Datacom Solutions>, <Site name>.";
            }
            else
            {
                DescriptionNotifications.Text = "Push notifications is off.";
            }
        }

        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            ServiceManager.Instance.LoginService.DeleteCredentials();

            //Navigation.InsertPageBefore(new LoginPage(), this);
            //await Navigation.PopAsync();
        }

        void arm_Toggled(object sender, EventArgs e)
        {
            ServiceManager.Instance.LoginService.ArmCamersSettings = ArmDisarmSwitch.IsToggled;
            if(ArmDisarmSwitch.IsToggled)
            {
                DescriptionArmDisarm.Text = "<nums> cameras of <Network Datacom Solutions>, <Site name> are Armed.";
            }
            else
            {
                DescriptionArmDisarm.Text = "Disarmed.";
            }
        }

        void Notifications_Toggled(object sender, EventArgs e)
        {
            ServiceManager.Instance.LoginService.PushNotificationsSettings = NotificationsSwitch.IsToggled;
            if(NotificationsSwitch.IsToggled)
            {
                DescriptionNotifications.Text = "You will receive push notifications for <Network Datacom Solutions>, <Site name>.";
            }
            else
            {
                DescriptionNotifications.Text = "Push notifications is off.";
            }
        }
    }
}