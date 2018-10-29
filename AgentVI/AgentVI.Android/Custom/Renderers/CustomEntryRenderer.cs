using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Xamarin.Forms;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using AgentVI.Droid.Custom.Renderers;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Xamarin.Forms.Entry), typeof(CustomEntryRenderer))]
namespace AgentVI.Droid.Custom.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        Android.Graphics.Color m_UnderlineColor = Android.Graphics.Color.Rgb(0,63,127);

        public CustomEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                this.Control.Background.Mutate().SetColorFilter(m_UnderlineColor, PorterDuff.Mode.SrcAtop);
            }
        }


    }
}