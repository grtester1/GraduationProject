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
        private Color ThumbOnColor = Color.Rgb(47, 192, 255);
        private Color ThumbOffColor = Color.Rgb(231, 231, 231);
        private Color TrackOnColor = Color.Blue;
        private Color TrackOffColor = Color.Gray;

        public CustomSwitchRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {

                if (this.Control.Checked)
                {
                    this.Control.ThumbDrawable.SetColorFilter(ThumbOnColor, PorterDuff.Mode.SrcAtop);
                    this.Control.TrackDrawable.SetColorFilter(TrackOnColor, PorterDuff.Mode.SrcAtop);

                }
                else
                {
                    this.Control.ThumbDrawable.SetColorFilter(ThumbOffColor, PorterDuff.Mode.SrcAtop);
                    this.Control.TrackDrawable.SetColorFilter(TrackOffColor, PorterDuff.Mode.SrcAtop);
                }

                this.Control.CheckedChange += OnCheckedChange;

            }
        }

        private void OnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if(this.Control.Checked)
            {
                this.Control.ThumbDrawable.SetColorFilter(ThumbOnColor, PorterDuff.Mode.SrcAtop);
                this.Control.TrackDrawable.SetColorFilter(TrackOnColor, PorterDuff.Mode.SrcAtop);
            }
            else
            {
                this.Control.ThumbDrawable.SetColorFilter(ThumbOffColor, PorterDuff.Mode.SrcAtop);
                this.Control.TrackDrawable.SetColorFilter(TrackOffColor, PorterDuff.Mode.SrcAtop);
            }
        }
    }
}