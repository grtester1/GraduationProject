using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgentVI.Custom.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LongRowButton : ContentView
	{
        public static readonly BindableProperty LayoutMinimumHeightProperty =
            BindableProperty.Create(nameof(Layout) + "MinimumHeight",
                typeof(int), typeof(LongRowButton), default(int), BindingMode.OneWay);
        public int LayoutMinimumHeight
        {
            get => (int)GetValue(LayoutMinimumHeightProperty);
            set => SetValue(LayoutMinimumHeightProperty, value);
        }
        public static readonly BindableProperty LayoutPaddingProperty =
            BindableProperty.Create(nameof(Layout) + "Padding",
                typeof(int), typeof(LongRowButton), default(int), BindingMode.OneWay);
        public int LayoutPadding
        {
            get => (int)GetValue(LayoutPaddingProperty);
            set => SetValue(LayoutPaddingProperty, value);
        }
        public static readonly BindableProperty LayoutClickedProperty =
            BindableProperty.Create(nameof(LayoutClicked),
                typeof(ICommand), typeof(LongRowButton), null, BindingMode.OneWay,
                propertyChanged: (i_Bindable, i_OldValue, i_NewValue) =>
                {
                    Console.WriteLine("Binding Changed. Error");
                });
        public static readonly BindableProperty LayoutClickedParamsProperty =
            BindableProperty.Create(nameof(LayoutClickedParams),
                typeof(object), typeof(LongRowButton), null, BindingMode.OneWay);
        public event EventHandler<EventArgs> Clicked;
        public ICommand LayoutClicked
        {
            get => (ICommand)GetValue(LayoutClickedProperty);
            set => SetValue(LayoutClickedProperty, value);
        }
        public object LayoutClickedParams
        {
            get => GetValue(LayoutClickedParamsProperty);
            set => SetValue(LayoutClickedParamsProperty, value);
        }
        public static readonly BindableProperty DescribingImageSourceProperty =
            BindableProperty.Create(nameof(DescribingImageSource),
                typeof(string), typeof(LongRowButton), default(string), BindingMode.OneWay);
        public string DescribingImageSource
        {
            get => (string)GetValue(DescribingImageSourceProperty);
            set => SetValue(DescribingImageSourceProperty, value);
        }
        public static readonly BindableProperty DescribingTextProperty =
            BindableProperty.Create(nameof(describingText),
                typeof(string), typeof(LongRowButton), default(string), BindingMode.OneWay);
        public string DescribingText
        {
            get => (string)GetValue(DescribingTextProperty);
            set => SetValue(DescribingTextProperty, value);
        }
        public static readonly BindableProperty ActionButtonClickedProperty =
            BindableProperty.Create(nameof(ActionButtonClicked),
                typeof(ICommand), typeof(LongRowButton), null, BindingMode.OneWay,
                propertyChanged: (i_Bindable, i_OldValue, i_NewValue) =>
                {
                    Console.WriteLine("Binding Changed. Error");
                });
        public static readonly BindableProperty ActionButtonClickedParamsProperty =
            BindableProperty.Create(nameof(ActionButtonClickedParams),
                typeof(object), typeof(LongRowButton), null, BindingMode.OneWay);
        public event EventHandler<EventArgs> ButtonClicked;
        public ICommand ActionButtonClicked
        {
            get => (ICommand)GetValue(ActionButtonClickedProperty);
            set => SetValue(ActionButtonClickedProperty, value);
        }
        public object ActionButtonClickedParams
        {
            get => GetValue(ActionButtonClickedParamsProperty);
            set => SetValue(ActionButtonClickedParamsProperty, value);
        }
        public static readonly BindableProperty ActionButtonWidthProperty =
            BindableProperty.Create(nameof(ActionButtonWidth),
                typeof(int), typeof(LongRowButton), 10, BindingMode.OneWay);
        public int ActionButtonWidth
        {
            get => (int)GetValue(ActionButtonWidthProperty);
            set => SetValue(ActionButtonWidthProperty, value);
        }
        public static readonly BindableProperty ActionButtonIsVisibleProperty =
            BindableProperty.Create(nameof(ActionButtonIsVisible),
                typeof(bool), typeof(LongRowButton), default(bool), BindingMode.OneWay);
        public bool ActionButtonIsVisible
        {
            get => (bool)GetValue(ActionButtonIsVisibleProperty);
            set => SetValue(ActionButtonIsVisibleProperty, value);
        }

        //missing implementation of OnPropertyChanged

        public LongRowButton ()
		{
			InitializeComponent ();
		}

        protected void OnActionButtonClicked(object sender, EventArgs args)
        {
            object resolvedParams;

            if (LayoutClickedParams != null)
            {
                resolvedParams = ActionButtonClickedParams;
            }
            else
            {
                resolvedParams = args;
            }

            if (ActionButtonClicked?.CanExecute(resolvedParams) ?? true)
            {
                ButtonClicked?.Invoke(this, args);
                ActionButtonClicked?.Execute(resolvedParams);
            }
        }

        protected void OnClickGestureRecognizerClicked(object sender, EventArgs args)
        {
            object resolvedParams;

            if (LayoutClickedParams != null)
            {
                resolvedParams = LayoutClickedParams;
            }
            else
            {
                resolvedParams = args;
            }

            if (LayoutClicked?.CanExecute(resolvedParams) ?? true)
            {
                Clicked?.Invoke(this, args);
                LayoutClicked?.Execute(resolvedParams);
            }
        }
    }
}