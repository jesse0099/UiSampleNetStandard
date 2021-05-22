using System;
using UiSampleMigrat.Services;
using Xamarin.Forms;


namespace UiSampleMigrat.Views.Logins
{

    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaGeraldLogin : ContentPage
    {
        RestServiceConsumer proc = new RestServiceConsumer();

        public PaginaGeraldLogin()
        {
            InitializeComponent();

        }



        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //Reconocimiento de Gestos 

        }

    }
}