using AgentVI.Utils;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgentVI.Custom.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailsHeader : Grid
	{
        public static readonly BindableProperty BackButtonImageSourceProperty = 
                BindableProperty.Create(nameof(BackButtonImageSource), typeof(string), typeof(DetailsHeader), default(string), BindingMode.OneWay);
        public string BackButtonImageSource
        {
            get => (string)GetValue(BackButtonImageSourceProperty);
            set => SetValue(BackButtonImageSourceProperty, value);
        }
        public static readonly BindableProperty LabelTextProperty =
        BindableProperty.Create(nameof(LabelText), typeof(string), typeof(DetailsHeader), default(string), BindingMode.OneWay);
        public string LabelText
        {
            get => (string)GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }
        public static readonly BindableProperty LabelFontSizeProperty =
                BindableProperty.Create(nameof(LabelFontSize), typeof(double), typeof(DetailsHeader), default(double), BindingMode.OneWay);
            public double LabelFontSize
        {
            get => (double)GetValue(LabelFontSizeProperty);
            set => SetValue(LabelFontSizeProperty, value);
        }
        public static readonly BindableProperty LabelFontColorProperty =
        BindableProperty.Create(nameof(LabelFontColor), typeof(Color), typeof(DetailsHeader), default(Color), BindingMode.OneWay);
        public Color LabelFontColor
        {
            get => (Color)GetValue(LabelFontColorProperty);
            set => SetValue(LabelFontColorProperty, value);
        }
        public static readonly BindableProperty ContextMenuButtonImageSourceProperty =
        BindableProperty.Create(nameof(ContextMenuButtonImageSource), typeof(string), typeof(DetailsHeader), default(string), BindingMode.OneWay);
        public string ContextMenuButtonImageSource
        {
            get => (string)GetValue(ContextMenuButtonImageSourceProperty);
            set => SetValue(ContextMenuButtonImageSourceProperty, value);
        }
        public static readonly BindableProperty BackButtonTappedProperty =
                BindableProperty.Create(nameof(BackButtonTapped), typeof(EventHandler), typeof(DetailsHeader), default(EventHandler), BindingMode.OneWay);
        public event EventHandler BackButtonTapped;
        public static readonly BindableProperty BackButtonCommandProperty =
                BindableProperty.Create(nameof(BackButtonCommand), typeof(ICommand), typeof(DetailsHeader), default(ICommand), BindingMode.OneWay);
        public ICommand BackButtonCommand
        {
            get => (ICommand)GetValue(BackButtonCommandProperty);
            set => SetValue(BackButtonCommandProperty, value);
        }
        public static readonly BindableProperty BackButtonCommandParametersProperty =
                BindableProperty.Create(nameof(BackButtonCommandParameters), typeof(object), typeof(DetailsHeader), default(object), BindingMode.OneWay);
        public object BackButtonCommandParameters
        {
            get => GetValue(BackButtonCommandParametersProperty);
            set => SetValue(BackButtonCommandParametersProperty, value);
        }
        public static readonly BindableProperty ContextMenuButtonCommandProperty =
        BindableProperty.Create(nameof(ContextMenuButtonCommand), typeof(ICommand), typeof(DetailsHeader), default(ICommand), BindingMode.OneWay);
        public ICommand ContextMenuButtonCommand
        {
            get => (ICommand)GetValue(ContextMenuButtonCommandProperty);
            set => SetValue(ContextMenuButtonCommandProperty, value);
        }
        public static readonly BindableProperty ContextMenuButtonCommandParametersProperty =
                BindableProperty.Create(nameof(ContextMenuButtonCommandParameters), typeof(object), typeof(DetailsHeader), default(object), BindingMode.OneWay);
        public object ContextMenuButtonCommandParameters
        {
            get => GetValue(ContextMenuButtonCommandParametersProperty);
            set => SetValue(ContextMenuButtonCommandParametersProperty, value);
        }
        public static readonly BindableProperty RoutingCommandProperty =
                BindableProperty.Create(nameof(RoutingCommand), typeof(ICommand), typeof(DetailsHeader), default(ICommand), BindingMode.OneWay);
        public ICommand RoutingCommand
        {
            get => (ICommand)GetValue(RoutingCommandProperty);
            set => SetValue(RoutingCommandProperty, value);
        }
        public static readonly BindableProperty RoutingCommandParametersProperty =
                BindableProperty.Create(nameof(RoutingCommandParameters), typeof(object), typeof(DetailsHeader), default(object), BindingMode.OneWay);
        public object RoutingCommandParameters
        {
            get => GetValue(RoutingCommandParametersProperty);
            set => SetValue(RoutingCommandParametersProperty, value);
        }
        public static readonly BindableProperty DropdownMenuPageProperty = 
                BindableProperty.Create(nameof(DropdownMenuPage), typeof(PopupPage), typeof(DetailsHeader), default(PopupPage), BindingMode.OneWay);
        public PopupPage DropdownMenuPage
        {
            get => (PopupPage)GetValue(DropdownMenuPageProperty);
            set => SetValue(DropdownMenuPageProperty, value);
        }

        public DetailsHeader ()
		{
			InitializeComponent ();
            backButton.Source = BackButtonImageSource;
            contextMenuButton.Source = ContextMenuButtonImageSource;
            sensorNameLabel.Text = LabelText;
            sensorNameLabel.FontSize = LabelFontSize;
            sensorNameLabel.TextColor = LabelFontColor;
		}

        private void onBackButtonTapped(object sender, EventArgs e)
        {
            BackButtonCommand?.Execute(BackButtonCommandParameters);
            BackButtonTapped?.Invoke(sender, e);
        }

        private void onContextMenuTapped(object sender, EventArgs e)
        {
            VisualElement buttonClicked = sender as VisualElement;
            Point elementCoordinates = buttonClicked.getCoordinates();
            
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushPopupAsync(DropdownMenuPage);
                DropdownMenuPage.TranslationX = elementCoordinates.X - 4.5*buttonClicked.Width;
                DropdownMenuPage.TranslationY = elementCoordinates.Y + buttonClicked.Height/2;
            }
            );
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if(propertyName == BackButtonImageSourceProperty.PropertyName)
            {
                backButton.Source = BackButtonImageSource;
            }
            if(propertyName == ContextMenuButtonImageSourceProperty.PropertyName)
            {
                contextMenuButton.Source = ContextMenuButtonImageSource;
            }
            if(propertyName == LabelFontSizeProperty.PropertyName)
            {
                sensorNameLabel.FontSize = LabelFontSize;
            }
            if(propertyName == LabelFontColorProperty.PropertyName)
            {
                sensorNameLabel.TextColor = LabelFontColor;
            }
            if(propertyName == LabelTextProperty.PropertyName)
            {
                sensorNameLabel.Text = LabelText;
            }
        }
    }
}