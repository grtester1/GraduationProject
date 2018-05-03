using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentVI.Services;
using AgentVI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgentVI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page1 : ContentPage
	{
        private LoginPageViewModel m_VM = null;

		public Page1 ()
		{
			InitializeComponent ();
            m_VM = new LoginPageViewModel();
            m_VM.InitializeFields(ServiceManager.Instance.LoginService.LoggedInUser);
            BindingContext = m_VM;
		}
	}
}