using Android.Widget;
using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UiSampleMigrat.Helpers;
using UiSampleMigrat.Models;
using UiSampleMigrat.MyExceptions;
using UiSampleMigrat.Services;

namespace UiSampleMigrat.ViewModels
{
    public class RootExplorePageViewModel : BaseViewModel
    {
        #region Propiedades
        private BaseDao Dao = null;

        private ObservableCollection<Categoria> categorias;

        public ObservableCollection<Categoria> Categorias
        {
            get { return categorias; }
            set {
                categorias = value;
                onPropertyChanged();
            }
        }

        private ObservableCollection<Enterprise> comercios;

        public ObservableCollection<Enterprise> Comercios
        {
            get { return comercios; }
            set { comercios = value;
                onPropertyChanged();
            }
        }

        #endregion



        public RootExplorePageViewModel()
        {
            _instance = this;
             CatsAsyncLoad();
            
        }
        #region Metodos
        private async Task CatsAsyncLoad() {
            IsBusy = true;
            try
            {
                Dao = new CategoriaDao();
                Categorias = new ObservableCollection<Categoria>(await ((CategoriaDao)Dao).GetList());

                await CommeByCatsAsyncLoad(new List<Categoria>(Categorias));
            }
            catch (ConnectionException Cex) {
                Commons.CustomizedToast(Color.White, Color.Black,
                     Cex.Message, ToastLength.Long, iconResource: "error64", textSize: 16);
            }
            catch (CategoryException Caex)
            {
                Commons.CustomizedToast(Color.White,Color.Black,
                    Caex.Message, ToastLength.Long, iconResource: "error64", textSize: 16);
            }
            IsBusy = false;
        }

        private async Task CommeByCatsAsyncLoad(List<Categoria> cats) {
            try
            {
                Dao = new EnterpriseDao();
                Comercios = new ObservableCollection<Enterprise>(await ((EnterpriseDao)Dao).GetListByCats(cats));
            }
            catch (ConnectionException coEx) {
                Commons.CustomizedToast(Color.White, Color.Black,
                    coEx.Message, ToastLength.Long, iconResource: "error64", textSize: 16);
            }
            catch (ComerException cEx)
            {
                Commons.CustomizedToast(Color.White, Color.Black,
                    cEx.Message, ToastLength.Long, iconResource: "error64", textSize: 16);
            }
            
        }
        #endregion

        #region Singleton 
        private static RootExplorePageViewModel _instance;

        public static RootExplorePageViewModel GetInstance()
        {
            if (_instance == null)
                return new RootExplorePageViewModel();
            return _instance;

        }

        #endregion



    }
}
