using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.CurrentActivity;
using Android.OS;
using Rg.Plugins.Popup;
using Rg.Plugins.Popup.Services;
using UiSampleMigrat.Interfaces;

namespace UiSampleMigrat.Droid
{
    [Activity(Label = "UiSampleMigrat", Icon = "@mipmap/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private Bundle savedInstanceState = null;

        protected override void OnCreate(Bundle bundle)
        {
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            savedInstanceState = bundle;
            //Inicializar Xamarin Essential
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, bundle);

            //Inicializacion de RG.Plugin
            Rg.Plugins.Popup.Popup.Init(this, bundle);

            //Cambio de color de la barra de estado
            Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#0a0a0a"));

            LoadApplication(new App());
        }

        public override void OnBackPressed()
        {
            if (Popup.SendBackPressed(base.OnBackPressed))
            {
                //PopUps en el stack de de popups
                PopupNavigation.Instance.PopAllAsync();
            }
            else
            {
                //Nada en el stack de popups
            }
        }

        public override void OnSaveInstanceState(Bundle outState, PersistableBundle outPersistentState)
        {
            this.savedInstanceState = outState;
            base.OnSaveInstanceState(outState, outPersistentState);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}