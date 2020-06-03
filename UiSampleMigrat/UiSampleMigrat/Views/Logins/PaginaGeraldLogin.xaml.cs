using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiSampleMigrat.Models;
using UiSampleMigrat.Services;
using UiSampleMigrat.Views.Home;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using UiSampleMigrat.Interfaces;


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