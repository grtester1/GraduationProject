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
	public partial class Page3Ca : CarouselPage
	{
		public Page3Ca ()
		{
            ItemsSource = (new ViewModels.FilterViewModel()).MyItemsSource;
			InitializeComponent ();
		}
	}
}