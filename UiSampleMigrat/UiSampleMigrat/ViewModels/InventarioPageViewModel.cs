using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiSampleMigrat.Models;
using System.Collections.ObjectModel;
using UiSampleMigrat.Services;
using UiSampleMigrat.Api_Models;
using Xamarin.Forms;
using System.IO;
using System.Windows.Input;
using UiSampleMigrat.Views.PopUps;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Services;
using UiSampleMigrat.Helpers;

namespace UiSampleMigrat.ViewModels
{
    public class InventarioPageViewModel : NotificationObject 
    {

        #region Propiedades
        public Producto OperationProduct { get; set; }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                onPropertyChanged();
            }
        }

        private Boolean _isVisibleMessage;
        public Boolean IsVisibleMessage
        {
            get { return _isVisibleMessage; }
            set
            {
                _isVisibleMessage = value;
                onPropertyChanged();
            }
        }

        private Boolean _isRefreshing;
        public Boolean IsRefreshing
        {
            get { return _isRefreshing; }
            set { _isRefreshing = value;
                onPropertyChanged();
            }
        }

        RestServiceConsumer service;

        private ObservableCollection<GroupedProducts> _unfilteredGroupedProducts;
        public ObservableCollection<GroupedProducts> UnfilteredGroupedProducts
        {
            get { return _unfilteredGroupedProducts; }
            set { _unfilteredGroupedProducts = value;
                onPropertyChanged();
            }
        }

        private ObservableCollection<GroupedProducts> _groupedProducts;
        public ObservableCollection<GroupedProducts> GroupedProducts
        {
            get { return _groupedProducts; }
            set { _groupedProducts = value;
                onPropertyChanged();
            }
        }

        public int CurrentCommer { get; set; }

        private ICommand _refreshingCommand;

        public ICommand RefreshingCommand
        {
            get { return _refreshingCommand; }
            set { _refreshingCommand = value;
                onPropertyChanged();
            }
        }

        private ICommand _addCommmand;

        public ICommand AddCommand
        {
            get {
                return new Command((e) =>
                {
                    var item = (e as Producto);
                    this.OperationProduct = item;
                    var popUpPoperties = new TestPop();
                    var scaleAnimation =new ScaleAnimation
                    {
                        PositionIn = MoveAnimationOptions.Right,
                        PositionOut = MoveAnimationOptions.Left
                    };
                    PopupNavigation.Instance.PushAsync(popUpPoperties);
                });
            }
            set { _addCommmand = value;
                onPropertyChanged();
            }
        }

        #endregion

        #region Constructores
        public InventarioPageViewModel()
        {
            _instance = this;
        }

        public InventarioPageViewModel(int currentCommer)
        {
            _instance = this;
            GroupedProducts = new ObservableCollection<GroupedProducts>();
            this.IsVisibleMessage = false;
            this._refreshingCommand = new Command(RefreshingCommandExecute);
            this.CurrentCommer = currentCommer;
            loadProducts(currentCommer).SafeFireAndForget(false);
        }
        #endregion

        #region Metodos y eventos
        public async Task loadProducts(int commer) {
            Device.BeginInvokeOnMainThread(() =>{
                this.IsRefreshing = true;
            });

            service = new RestServiceConsumer();
            var connection = await service.CheckConnection();
            if (!connection.IsSuccesFull) {
                //Conexion no establecida
                Device.BeginInvokeOnMainThread(()=> {
                    this.IsRefreshing = false;
                });
                this.IsVisibleMessage = true;
                this.Message = connection.Message;
                return;
            }

            this.IsVisibleMessage = false;
            var products = await service.Get<List<ApiProducto>>(Constantes.BASEURL,Constantes.PRODUCTSPREFIX,$"{Constantes.PRODUCTGETBYCOMME}{commer}");
            var sucs = await service.Get<List<ApiSucursal>>(Constantes.BASEURL,Constantes.COMMEPREFIX,$"{Constantes.COMMEGETSUCBYCOMME}{commer}");

            if (!products.IsSuccesFull || !sucs.IsSuccesFull) {
                //Error en la extraccion de datos
                return;
            }

            List<ApiProducto> productos = (List<ApiProducto>)products.Result;
            List<ApiSucursal> sucursales  = (List<ApiSucursal>)sucs.Result;
            List<ApiInventarioSucursal> inventarioSucursals = new List<ApiInventarioSucursal>();

            foreach (var suc in sucursales)
            {
                ApiInventarioSucursal tempIn = new ApiInventarioSucursal();
                tempIn.Sucursal = suc;
                foreach (var prods in productos)
                {
                    if (prods.sucursal == suc.idSucursal) {
                        tempIn.Productos.Add(prods);
                    }
                }
                inventarioSucursals.Add(tempIn);
            }

            foreach (var item in inventarioSucursals)
            {
                List<Producto> tempPro = new List<Producto>();

                foreach (var temItem in item.Productos)
                {
                    ImageSource imgSource;
                    var byteArray = Convert.FromBase64String(Convert.ToString(temItem.ilustracion));

                    if (byteArray.Length != 0)
                        imgSource = ImageSource.FromStream(() => new MemoryStream(byteArray));
                    else
                        imgSource = ImageSource.FromFile("inven.png");


                    tempPro.Add(new Producto() {
                        Sucursal = item.Sucursal,
                        ID = temItem.idProducto,
                        IdSucursal = temItem.sucursal,
                        Descripcion = temItem.descripcion,
                        FechaVencimiento = temItem.fechaVencimiento,
                        Existencias = temItem.existencias,
                        NombreProducto = temItem.nombreProducto,
                        Precio = Convert.ToDouble(temItem.precio),
                        Ilustracion = imgSource
                    });
                }
                this.GroupedProducts.Add(new Models.GroupedProducts(item.Sucursal,tempPro));
            }

            this.UnfilteredGroupedProducts = GroupedProducts;

            Device.BeginInvokeOnMainThread(() => {
                this.IsRefreshing = false;
            });

        }
        public void RefreshingCommandExecute() {
            this.GroupedProducts.Clear();
            loadProducts(CurrentCommer).SafeFireAndForget(false);

        }
        public void filter(string filter) {
            if (string.IsNullOrEmpty(filter))
            {
                this.GroupedProducts = UnfilteredGroupedProducts;
                onPropertyChanged();
            }
            else {
                var filteredGroupedProducts = new ObservableCollection<GroupedProducts>();
                foreach (var item in GroupedProducts)
                {
                    filteredGroupedProducts.Add(new GroupedProducts(item.NombreSucursal,
                        item.Where((x) => x.NombreProducto.ToLower().Trim().Contains(filter)
                        || x.Precio.ToString().Contains(filter)
                        || x.Sucursal.nombreSucursal.Trim().ToLower().Contains(filter))));
                }
                this.GroupedProducts = filteredGroupedProducts;
                onPropertyChanged();
            }
        }
        #endregion

        #region Singleton
        private static InventarioPageViewModel _instance;
        public static InventarioPageViewModel GetInstance() {
            if (_instance == null)
                return new InventarioPageViewModel();
            else
                return _instance;
        } 
        #endregion

    }
}
