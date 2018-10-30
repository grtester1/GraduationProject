using AgentVI.Interfaces;
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
	public partial class HealthStatPage : ContentPage//, IBindable, IBindableVM
	{
        //public IBindableVM BindableViewModel => this;
        //public ContentPage ContentPage => this;

        public HealthStatPage ()
		{
			InitializeComponent ();
		}
	}
}