using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiSampleMigrat.Models;

namespace UiSampleMigrat.ViewModels
{
    public class MainViewModel
    {
        public InventarioPageViewModel Inventario { get; set; }
        public RootExplorePageViewModel RootExplorer { get; set; }
        public EnterprisePageViewModel  Enterprises { get; set; }
        public LoginViewModel Login { get; set; }
        public RootExplorePageViewModel rootViewModel { get; set; }
        public UpdateProfileViewModel UpdateProfile { get; set; }
        public TestPageViewModel Test { get; set; }
        public ProfileViewModel Profile { get; set; }

        public MainViewModel() {

            this.Login = new LoginViewModel();
            this.Profile = new ProfileViewModel();
            this.UpdateProfile = new UpdateProfileViewModel();
            this.Inventario = new InventarioPageViewModel();
            this.Enterprises = new EnterprisePageViewModel();
            this.Test = new TestPageViewModel();
            this.RootExplorer = new RootExplorePageViewModel();
            
        }


    }
}
