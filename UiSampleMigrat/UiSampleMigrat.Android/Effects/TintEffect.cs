using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AWImageButton = Android.Support.V7.Widget.AppCompatImageButton;
using UiSampleMigrat.Droid.Effects;
using UiSampleMigrat.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
//Minimo SDK23

[assembly: Xamarin.Forms.ExportEffect(typeof(DroidTintEffect), nameof(TintEffect))]
namespace UiSampleMigrat.Droid.Effects
{
    public class DroidTintEffect : PlatformEffect
    {
            static readonly int[][] _colorStates =
            {
                    new[] { global::Android.Resource.Attribute.StateEnabled },
                    new[] { -global::Android.Resource.Attribute.StateEnabled }, //disabled state
                    new[] { global::Android.Resource.Attribute.StatePressed } //pressed state
                };
            protected override void OnAttached()
            {
                UpdateTintColor();
            } 
            protected override void OnDetached()
            {
                UpdateTintColor();
            }
            protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
            {
                UpdateTintColor();
            }
            private void UpdateTintColor()
            {
                try
                {
                    if (this.Control is AWImageButton button)
                    {
                        var androidColor = UiSampleMigrat.Effects.TintEffectExtension.GetTintColor(this.Element).ToAndroid();
                        var disabledColor = androidColor;
                        disabledColor.A = 0x1C; //140
                        var pressedColor = androidColor;
                        pressedColor.A = 0x24; //180
                        button.ImageTintList = new ColorStateList(_colorStates, new[] { androidColor.ToArgb(), disabledColor.ToArgb(), pressedColor.ToArgb() });
                        button.ImageTintMode = PorterDuff.Mode.SrcIn;
                }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"An error occurred when setting the {typeof(UiSampleMigrat.Effects.TintEffect)} effect: {ex.Message}\n{ex.StackTrace}");
                }
            }   

    }
}