using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AgentVI
{
	public partial class MainPage : ContentPage
	{
        string banana;

		public MainPage()
		{
			InitializeComponent();
		}

        public MainPage(string i_banana) : this()
        {
            banana = i_banana;
        }

        public void bananaShitClicked(object sender, EventArgs e)
        {
            stupidShit.Text = "SHITTTT "+banana;
        }

    }
}
