using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using AgentVI.Custom.Renderers;
using AgentVI.Droid.Custom.Renderers;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomViewCell), typeof(CustomViewCellRenderer))]
namespace AgentVI.Droid.Custom.Renderers
{
    public class CustomViewCellRenderer : ViewCellRenderer
    {
        private Android.Views.View unchangedCell;
        private Drawable unselectedBackground;
        private bool isSelected;

        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
        {
            unchangedCell = base.GetCellCore(item, convertView, parent, context);
            isSelected = false;
            unselectedBackground = unchangedCell.Background;

            return base.GetCellCore(item, convertView, parent, context);
        }

        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnCellPropertyChanged(sender, e);

            if(e.PropertyName == "IsSelected")
            {
                isSelected = !isSelected;

                if(isSelected)
                {
                    CustomViewCell customViewCell = sender as CustomViewCell;
                    unchangedCell.SetBackgroundColor(customViewCell.SelectedBackgroundColor.ToAndroid());
                }
                else
                {
                    unchangedCell.SetBackground(unselectedBackground);
                }
            }
        }
    }
}