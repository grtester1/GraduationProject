using AgentVI.Interfaces;
using AgentVI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgentVI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HealthStatPage : ContentPage, IBindable
	{
        public IBindableVM BindableViewModel => HealthStatVM;
        public ContentPage ContentPage => this;
        public HealthStatViewModel HealthStatVM { get; private set; }

        public HealthStatPage ()
		{
			InitializeComponent ();
		}
	}
}