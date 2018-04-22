using AgentVI.Models;
using AgentVI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgentVI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilterPage : CarouselPage
	{
        public FilterPage()
        {
            InitializeComponent ();
            //ItemsSource = ColorsViewModel.All;
            BindingContext = new FilterViewModel();
        }
    }
}