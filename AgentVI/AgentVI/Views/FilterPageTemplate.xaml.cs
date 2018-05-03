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
	public partial class FilterPageTemplate : ContentPage
	{
		public FilterPageTemplate ()
		{
			InitializeComponent ();

            BindingContext = new FilterViewModel();
        }

        public FilterPageTemplate(BindableObject i_ViewModel): this()
        {
            BindingContext = i_ViewModel;
        }
	}
}