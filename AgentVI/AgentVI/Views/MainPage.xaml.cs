using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AgentVI.additionals;
using AgentVI.ViewModels;

namespace AgentVI
{
    public partial class MainPage : ContentPage
    {
        string userId;

        public MainPage()
        {
            InitializeComponent();
        }

        public MainPage(string i_userId) : this()
        {
            userId = i_userId;
        }

        public void Button_Clicked(object sender, EventArgs e)
        {
            WelcomeLabel.Text = "Welcome " + nameEntry.Text;
        }
    }
}
