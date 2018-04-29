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
        }

        void logout_Clicked(object sender, EventArgs e)
        {

        }

        void arm_Toggled(object sender, EventArgs e)
        {
            DescriptionArmDisarm.Text = "<nums> cameras of <Network Datacom Solutions>, <Site name> are Armed.";
        }

        void Notifications_Toggled(object sender, EventArgs e)
        {
            DescriptionNotifications.Text = "You will receive push notifications for <Network Datacom Solutions>, <Site name>.";
        }
    }
}