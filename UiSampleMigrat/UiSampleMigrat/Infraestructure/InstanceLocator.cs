using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiSampleMigrat.ViewModels;

namespace UiSampleMigrat.Infraestructure
{
    public class InstanceLocator
    {
        public MainViewModel  Main{ get; set; }

        public InstanceLocator() {
            this.Main = new MainViewModel();
        }

    }
}
