using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace UiSampleMigrat.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootHomePage : Xamarin.Forms.TabbedPage
    {

        public RootHomePage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            this.SelectedTabColor = Color.White;
            this.UnselectedTabColor = Color.White;

        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

    }

}