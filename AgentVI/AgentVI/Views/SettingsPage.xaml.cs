using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace AgentVI.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            ArmDisarmSwitch.IsToggled = false;
            NotificationsSwitch.IsToggled = false;

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

        void logout_Clicked(object sender, EventArgs e)
        {

        }

        void arm_Toggled(object sender, EventArgs e)
        {
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