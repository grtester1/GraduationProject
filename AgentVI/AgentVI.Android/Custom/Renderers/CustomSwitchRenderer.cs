using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgentVI.Droid.Custom.Renderers;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Xamarin.Forms.Switch), typeof(CustomSwitchRenderer))]
namespace AgentVI.Droid.Custom.Renderers
{
    public class CustomSwitchRenderer : SwitchRenderer
    {
        private Color blue = Color.Blue;
        private Color yellow = Color.Yellow;

        CustomSwitchRenderer(Context i_Context) : base(i_Context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {

                if (this.Control.Checked)
                {
                    this.Control.ThumbDrawable.SetColorFilter(yellow, PorterDuff.Mode.SrcAtop);
                    this.Control.TrackDrawable.SetColorFilter(yellow, PorterDuff.Mode.SrcAtop);

                }
                else
                {
                    this.Control.ThumbDrawable.SetColorFilter(blue, PorterDuff.Mode.SrcAtop);
                    this.Control.TrackDrawable.SetColorFilter(blue, PorterDuff.Mode.SrcAtop);
                }

            }
        }
    }
}