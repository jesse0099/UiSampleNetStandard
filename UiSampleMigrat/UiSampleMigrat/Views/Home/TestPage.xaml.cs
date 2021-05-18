using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UiSampleMigrat.ViewModels;

namespace UiSampleMigrat.Views.Home
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestPage : ContentPage
	{
		public TestPage()
		{
			InitializeComponent ();
            MessagingCenter.Subscribe<TestPageViewModel>(this, "toggleDrawer", (a) => {
                naviDrawer.ToggleDrawer();
            });
        }
	}
}