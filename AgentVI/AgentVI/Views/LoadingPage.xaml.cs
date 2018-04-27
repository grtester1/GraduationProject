using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace AgentVI.Views
{
    public partial class LoadingPage : ContentPage
    {
        public LoadingPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await ProgressBarLine.ProgressTo(1, 3000, Easing.CubicInOut);

            await Navigation.PushAsync(new LoginPage());
        }
    }
}
