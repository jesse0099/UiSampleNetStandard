using Android.Widget;
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
        private CategoriaDao CatDao;

        private ObservableCollection<Categoria> categorias;

        public ObservableCollection<Categoria> Categorias
        {
            get { return categorias; }
            set {
                categorias = value;
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
                CatDao = new CategoriaDao();
                Categorias = new ObservableCollection<Categoria>(await CatDao.GetList());

            }
            catch (ConnectionException Cex) {
                Commons.CustomizedToast(Android.Graphics.Color.White, Android.Graphics.Color.Black,
                     Cex.Message, ToastLength.Long, iconResource: "error64", textSize: 16);
            }
            catch (CategoryException Caex)
            {
                Commons.CustomizedToast(Android.Graphics.Color.White, Android.Graphics.Color.Black,
                    Caex.Message, ToastLength.Long, iconResource: "error64", textSize: 16);
            }
            IsBusy = false;
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
