using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;

namespace AgentVI.Custom.Effects
{
    public class SwitchChangeColorEffect : RoutingEffect
    {
        public static readonly BindableProperty  FalseColorProperty= 
                BindableProperty.CreateAttached("FalseColor", typeof(Color), typeof(SwitchChangeColorEffect), Color.Transparent, BindingMode.OneWay, propertyChanged: OnColorChanged);

        public static readonly BindableProperty TrueColorProperty =
                BindableProperty.CreateAttached("TrueColor", typeof(Color), typeof(SwitchChangeColorEffect), Color.Transparent, BindingMode.OneWay, propertyChanged: OnColorChanged);

        public static readonly BindableProperty  ThumbCOlorProperty= 
                BindableProperty.CreateAttached("ThumbColor", typeof(Color), typeof(SwitchChangeColorEffect), Color.Silver, BindingMode.OneWay, propertyChanged: OnColorChanged);

        public SwitchChangeColorEffect() : base("AgentVI.Custom.Effects.SwitchChangeColorEffect")
        {
        }

        public static Color GetFalseColor(BindableObject i_View)
        {
            return (Color)i_View.GetValue(FalseColorProperty);
        }

        public static void SetFalseColor(BindableObject i_View, Color i_Color)
        {
            i_View.SetValue(FalseColorProperty, i_Color);
        }

        public static Color GetTrueColor(BindableObject i_View)
        {
            return (Color)i_View.GetValue(TrueColorProperty);
        }

        public static void SetTrueColor(BindableObject i_View, Color i_Color)
        {
            i_View.SetValue(TrueColorProperty, i_Color);
        }

        public static Color GetThumbColor(BindableObject i_View)
        {
            return (Color)i_View.GetValue(ThumbCOlorProperty);
        }

        public static void SetThumbColor(BindableObject i_View, Color i_Color)
        {
            i_View.SetValue(ThumbCOlorProperty, i_Color);
        }

        private static void OnColorChanged(BindableObject i_Bindable, object i_OldValue, object i_NewValue)
        {
            Switch control = i_Bindable as Switch;
            if (control == null)
            {
                return;
            }

            Color newControlColor = (Color)i_NewValue;

            var controlAttachedEffect = control.Effects.FirstOrDefault(effect => effect is SwitchChangeColorEffect);
            if (newControlColor != Color.Transparent && controlAttachedEffect == null)
            {
                control.Effects.Add(controlAttachedEffect);
            }
            else if(newControlColor == Color.Transparent && controlAttachedEffect != null)
            {
                control.Effects.Remove(controlAttachedEffect);
            }
        }
    }
}
