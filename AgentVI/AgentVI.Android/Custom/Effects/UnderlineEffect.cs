using System;
using Android.Widget;
using Xamarin.Forms;
using AgentVI.Droid.Custom.Effects;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;

[assembly: ResolutionGroupName(AgentVI.Custom.Effects.UnderlineEffect.EffectNamespace)]
[assembly: ExportEffect(typeof(UnderlineEffect), nameof(UnderlineEffect))]
namespace AgentVI.Droid.Custom.Effects
{
    public class UnderlineEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            SetUnderline(true);
        }

        protected override void OnDetached()
        {
            SetUnderline(false);
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);
            if (args.PropertyName == Label.TextProperty.PropertyName || args.PropertyName == Label.FormattedTextProperty.PropertyName)
            {
                SetUnderline(true);
            }
        }
        private void SetUnderline(bool i_Underlined)
        {
            try
            {
                var textView = (TextView)Control;
                if(i_Underlined)
                {
                    textView.PaintFlags |= Android.Graphics.PaintFlags.UnderlineText;
                }
                else
                {
                    textView.PaintFlags &= ~Android.Graphics.PaintFlags.UnderlineText;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}