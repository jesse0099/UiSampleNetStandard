using Android.Content;
using UiSampleMigrat.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(UiSampleMigrat.Controls.ExtendedLabel), typeof(UiSampleMigrat.Droid.Renderers.ExtendedLabelRenderer))]
namespace UiSampleMigrat.Droid.Renderers
{

    public class ExtendedLabelRenderer : Xamarin.Forms.Platform.Android.LabelRenderer
    {

        public ExtendedLabelRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            var el = (Element as ExtendedLabel);

            if (el != null && el.JustifyText)
            {
                if(Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
                {
                    Control.JustificationMode = Android.Text.JustificationMode.InterWord;
                }
            }
        }
    }
}