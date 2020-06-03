using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace UiSampleMigrat.Effects
{
        public static class TintEffectExtension {
            public static readonly BindableProperty TintColorProperty = BindableProperty.CreateAttached
                ("TintColor", typeof(Color), typeof(TintEffect), Color.Black, propertyChanged: OnTintColorPropertyChanged);
            public static Color GetTintColor(BindableObject bindable)
            {
                return (Color)bindable.GetValue(TintColorProperty);
            }
            public static void SetTintColor(BindableObject bindable, Color value)
            {
                bindable.SetValue(TintColorProperty, value);
            }
            private static void OnTintColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
            {
                if (bindable is ImageButton current)
                {
                    if ((Color)newValue != Color.Black)
                    {
                        if (!current.Effects.Any(e => e is TintEffect))
                            current.Effects.Add(Effect.Resolve(nameof(TintEffect)));
                    }
                    else
                    {
                        if (current.Effects.Any(e => e is TintEffect))
                        {
                            var existingEffect = current.Effects.FirstOrDefault(e => e is TintEffect);
                            current.Effects.Remove(existingEffect);
                        }
                    }
                }
            }

        }
        public class TintEffect : RoutingEffect
        {
            public TintEffect() : base("YoniEffects.TintEffect")
            {

            }
        }
}
