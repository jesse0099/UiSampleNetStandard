using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiSampleMigrat.Models;

namespace UiSampleMigrat.ViewModels
{
    public class HistoricPageViewModel : NotificationObject
    {
        private ObservableCollection<Orden> ordenes;

        public ObservableCollection<Orden> Ordenes
        {
            get { return ordenes; }
            set { ordenes = value;
                onPropertyChanged();
            }
        }

        private Orden selectedOrder;

        public Orden SelectedOrder
        {
            get { return selectedOrder; }
            set { selectedOrder = value;
                onPropertyChanged();
            }
        }




        public HistoricPageViewModel()
        {
            Ordenes = new ObservableCollection<Orden>();
            Ordenes.Add(new Orden() { Vendedor = "AM-PM",Ordenada = DateTime.Now ,ID = 7696,Total=4333});
            Ordenes.Add(new Orden() { Vendedor = "AM-PM",Ordenada = DateTime.Now ,ID = 7696,Total=5655});

        }


    }
}
