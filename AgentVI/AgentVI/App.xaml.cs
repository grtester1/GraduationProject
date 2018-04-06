﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgentVI.Views;
using Xamarin.Forms;

namespace AgentVI
{
	public partial class App : Application
	{
        public App ()
		{
			InitializeComponent();
            //MainPage = new MainMenu();
            MainPage = new NavigationPage(new loginPage());
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
