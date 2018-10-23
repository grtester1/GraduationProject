using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgentVI.Custom.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HeaderControl : Grid
	{
        public static readonly BindableProperty AccountNameProperty =
            BindableProperty.Create(nameof(AccountName), typeof(string), typeof(HeaderControl), default(string), BindingMode.OneWay);
        public string AccountName
        {
            get => (string)GetValue(AccountNameProperty);
            set => SetValue(AccountNameProperty, value);
        }
        public static readonly BindableProperty  AccountNameColorProperty= 
                BindableProperty.Create(nameof(AccountNameColor), typeof(Color), typeof(HeaderControl), default(Color), BindingMode.OneWay);
        public Color AccountNameColor
        {
            get => (Color)GetValue(AccountNameColorProperty);
            set => SetValue(AccountNameColorProperty, value);
        }
        public static readonly BindableProperty AccountNameFontSizeProperty = 
                BindableProperty.Create(nameof(AccountNameFontSize), typeof(double), typeof(HeaderControl), default(double), BindingMode.OneWay);
        public double AccountNameFontSize
        {
            get => (double)GetValue(AccountNameFontSizeProperty);
            set => SetValue(AccountNameFontSizeProperty, value);
        }
        public static readonly BindableProperty LogoSourceProperty =
            BindableProperty.Create(nameof(LogoSource), typeof(ImageSource), typeof(HeaderControl), default(ImageSource), BindingMode.OneWay);
        public ImageSource LogoSource
        {
            get => (ImageSource)GetValue(LogoSourceProperty);
            set => SetValue(LogoSourceProperty, value);
        }
        public static readonly BindableProperty ControlMargineProperty= 
                BindableProperty.Create(nameof(ControlMargine), typeof(Thickness), typeof(HeaderControl), default(Thickness), BindingMode.OneWay);
        public Thickness ControlMargine
        {
            get => (Thickness)GetValue(ControlMargineProperty);
            set => SetValue(ControlMargineProperty, value);
        }

        public HeaderControl ()
		{
			InitializeComponent ();
            this.Margin = ControlMargine;
            accountLabel.Text = AccountName;
            accountLabel.TextColor = AccountNameColor;
            accountLabel.FontSize = AccountNameFontSize;
            logoImage.Source = LogoSource;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if(propertyName == AccountNameProperty.PropertyName)
            {
                accountLabel.Text = AccountName;
            }
            if(propertyName == AccountNameColorProperty.PropertyName)
            {
                accountLabel.TextColor = AccountNameColor;
            }
            if(propertyName == AccountNameFontSizeProperty.PropertyName)
            {
                accountLabel.FontSize = AccountNameFontSize;
            }
            if(propertyName == LogoSourceProperty.PropertyName)
            {
                logoImage.Source = LogoSource;
            }
            if(propertyName == ControlMargineProperty.PropertyName)
            {
                this.Margin = ControlMargine;
            }
        }
    }
}