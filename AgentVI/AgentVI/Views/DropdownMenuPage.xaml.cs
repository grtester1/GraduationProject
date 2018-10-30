using Rg.Plugins.Popup.Extensions;
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
	public partial class DropdownMenuPage : Rg.Plugins.Popup.Pages.PopupPage
	{
        private List<Tuple<string, Action>> actionItems { get; set; }

		private DropdownMenuPage ()
		{
			InitializeComponent ();
		}

        public static DropdownMenuPage FactoryMethod()
        {
            DropdownMenuPage res = new DropdownMenuPage();

            return res;
        }

        public DropdownMenuPage Build()
        {
            double widthRequest = 100;
            BoxView separator = new BoxView()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HeightRequest = 1,
                WidthRequest = widthRequest,
                BackgroundColor = Color.Black,
                Color = Color.Black
            };

            foreach (Tuple<string, Action> actionItem in actionItems)
            {
                Label menuActionItem = new Label()
                {
                    Text = actionItem.Item1,
                    WidthRequest = widthRequest,
                    FontSize = AgentVI.Utils.Settings.ActionMenuItemFontSize
                };

                menuActionItem.GestureRecognizers.Add(new TapGestureRecognizer()
                {
                    Command = new Command(() =>
                    {
                        Navigation.PopAllPopupAsync();
                        actionItem.Item2.Invoke();
                    })
                });

                dropdownLayout.Children.Add(menuActionItem);
                //dropdownLayout.Children.Add(separator);

                //dropdownLayout.Children.Add(new Button()
                //{
                //    Text = actionItem.Item1,
                //    WidthRequest = 100,
                //    Command = new Command(() =>
                //    {
                //        Navigation.PopAllPopupAsync();
                //        actionItem.Item2.Invoke();
                //    })
                //});
            }
            return this;
        }

        public DropdownMenuPage AddActionItem(Tuple<string, Action> i_ActionItem)
        {
            if(actionItems == null)
            {
                actionItems = new List<Tuple<string, Action>>();
            }
            actionItems.Add(i_ActionItem);

            return this;
        }
	}
}