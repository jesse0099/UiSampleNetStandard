using System;
using Xamarin.Forms;

namespace UiSampleMigrat.Controls
{

    public class ExtendedLabel : Label
    {
        public static readonly BindableProperty JustifytextProperty = (
            BindableProperty.Create(
                propertyName: nameof(JustifyText),
                returnType: typeof(Boolean),
                declaringType: typeof(ExtendedLabel),
                defaultValue: false,
                defaultBindingMode: BindingMode.OneWay
                )
        );

        public bool JustifyText
        {
            get { return (Boolean)GetValue(JustifytextProperty); }
            set { SetValue(JustifytextProperty, value); }
        }
    }
}
