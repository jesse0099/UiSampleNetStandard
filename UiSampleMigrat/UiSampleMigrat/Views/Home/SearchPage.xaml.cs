using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UiSampleMigrat.ViewModels;
using UiSampleMigrat.Models;


namespace UiSampleMigrat.Views.Home
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : ContentPage
	{
		public SearchPage ()
		{
			InitializeComponent ();

            //MessagingCenter.Subscribe<RootExplorePageViewModel>(this, "Goto", (a) => {
            //    //Iniciar navegacion en el stack
            //    if (lstCats.SelectedItem != null)
            //        gotoEnterprise(((Categoria)lstCats.SelectedItem).NombreCategoria);
            //});

        }


    }
}