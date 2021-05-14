
using Xamarin.Forms;
using UiSampleMigrat.Views.SignUps;
using UiSampleMigrat.Views.Logins;
using UiSampleMigrat.Views.UpdateInfoUser;
using UiSampleMigrat.Views.Home;
using UiSampleMigrat.Helpers;
using UiSampleMigrat.Services;
using Xamarin.Forms.Xaml;

//[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace UiSampleMigrat
{


    public partial class App : Application
    {

        static TodoItemDatabase database;

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDQ1OTEyQDMxMzkyZTMxMmUzMGFYUVRyaFV6U1kwWFc4QUhIbWNCOEsxRkpOSjhVSHhsa3dtWDhodDhpY3c9");
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTQ1Njk0QDMxMzcyZTMyMmUzMFFRVk1DcmZkQkFueWtkTUpxaDczRG04cDEzYlkyMTZSZXhLR3B0WlQ1N2s9");
            InitializeComponent();
            #if DEBUG
            HotReloader.Current.Run(this);
            #endif

            MainPage = new NavigationPage(new PaginaGeraldLogin());



        }

        public static TodoItemDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new TodoItemDatabase();
                }
                return database;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            if (Settings.IsRemembered & !Settings.SerializedToken.Equals(string.Empty))
                MainPage = new RootHomePage();
            //var config = new Realms.RealmConfiguration("default.realm");
            //Realms.Realm.DeleteRealm(config);
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
