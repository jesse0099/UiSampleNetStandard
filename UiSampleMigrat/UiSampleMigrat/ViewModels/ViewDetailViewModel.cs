using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiSampleMigrat.Models;

namespace UiSampleMigrat.ViewModels
{
    public class ViewDetailViewModel : NotificationObject
    {
        private ObservableCollection<Item> items;

        public ObservableCollection<Item> Items
        {
            get { return items; }
            set { items = value;
                onPropertyChanged();
            }
        }

        private Orden orden;

        public Orden Order
        {
            get { return orden; }
            set { orden = value; }
        }


        public ViewDetailViewModel()
        {
            this.Items = new ObservableCollection<Item>();
        }

    }
}
