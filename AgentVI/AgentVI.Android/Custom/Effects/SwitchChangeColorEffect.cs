using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Color = Xamarin.Forms.Color;
using Switch = Android.Widget.Switch;
using AndroidSwitchChangeColorEffect = AgentVI.Droid.Custom.Effects.SwitchChangeColorEffect;
using SharedSwitchChangeColorEffect = AgentVI.Custom.Effects.SwitchChangeColorEffect;
using Xamarin.Forms.Platform.Android;
using Android.Support.V7.Widget;

[assembly: ExportEffect(typeof(AndroidSwitchChangeColorEffect), "SwitchChangeColorEffect")]
namespace AgentVI.Droid.Custom.Effects
{
    [Android.Runtime.Preserve(AllMembers = true)]
    public class SwitchChangeColorEffect : PlatformEffect
    {
        private Color m_TrueColor, m_FalseColor, m_ThumbColor, m_FalseColorDarker, m_TrueColorDarker;

        protected override void OnAttached()
        {
            if(Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.JellyBean)
            {
                m_ThumbColor = (Color)Element.GetValue(SharedSwitchChangeColorEffect.ThumbCOlorProperty);
                m_TrueColor = (Color)Element.GetValue(SharedSwitchChangeColorEffect.TrueColorProperty);
                m_FalseColor = (Color)Element.GetValue(SharedSwitchChangeColorEffect.FalseColorProperty);

                m_FalseColorDarker = m_FalseColor.AddLuminosity(-0.25);
                m_TrueColorDarker = m_TrueColor.AddLuminosity(-0.25);

                ((SwitchCompat)Control).CheckedChange += OnCheckedChange;
                ((SwitchCompat)Control).TrackDrawable.SetColorFilter(m_FalseColorDarker.ToAndroid(), Android.Graphics.PorterDuff.Mode.Multiply );
                ((SwitchCompat)Control).TrackDrawable.SetColorFilter(m_ThumbColor.ToAndroid(), Android.Graphics.PorterDuff.Mode.Multiply);
            }
        }

        private void OnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if(e.IsChecked)
            {
                ((SwitchCompat)Control).TrackDrawable.SetColorFilter(m_TrueColorDarker.ToAndroid(), Android.Graphics.PorterDuff.Mode.Multiply);
            }
            else
            {
                ((SwitchCompat)Control).TrackDrawable.SetColorFilter(m_FalseColorDarker.ToAndroid(), Android.Graphics.PorterDuff.Mode.Multiply);
            }
        }

        protected override void OnDetached()
        {
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.JellyBean)
            {
                ((Switch)Control).CheckedChange -= OnCheckedChange;
            }
        }
    }
}