using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
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
            set
            {
                categorias = value;
                onPropertyChanged();
            }
        }

        private ObservableCollection<Enterprise> comercios;

        public ObservableCollection<Enterprise> Comercios
        {
            get { return comercios; }
            set
            {
                comercios = value;
                onPropertyChanged();
            }
        }

        public ICommand RefreshCommeCommand { get; set; }

        #endregion



        public RootExplorePageViewModel()
        {
            _instance = this;
            RefreshCommeCommand = new Xamarin.Forms.Command(RefreshCommeExecute);
            //Lanzamiento asincronico sin espera a retornos
            CatsAsyncLoad();

        }

        #region Metodos
        private async Task CatsAsyncLoad()
        {

            try
            {
                Dao = new CategoriaDao();
                Categorias = new ObservableCollection<Categoria>(await ((CategoriaDao)Dao).GetList());

                await CommeByCatsAsyncLoad(new List<Categoria>(Categorias));
            }
            catch (ConnectionException Cex)
            {
                ErrorToasts(Cex.Message);
            }
            catch (CategoryException Caex)
            {
                ErrorToasts(Caex.Message);
            }
            finally
            {
                InvokeOnMainThread(() => IsRefreshingView = false);
            }
        }

        private async Task CommeByCatsAsyncLoad(List<Categoria> cats)
        {

            try
            {
                Dao = new EnterpriseDao();
                Comercios = new ObservableCollection<Enterprise>(await ((EnterpriseDao)Dao).GetListByCats(cats));
            }
            catch (ConnectionException coEx)
            {
                ErrorToasts(coEx.Message);
            }
            catch (ComerException cEx)
            {
                ErrorToasts(cEx.Message);
            }
            finally
            {
                InvokeOnMainThread(() => IsRefreshingView = false);
            }
        }

        #endregion

        #region Comandos
        //Refrescar empresas - segun categorias seleccionadas
        public void RefreshCommeExecute()
        {
            //InvokeOnMainThread(() => IsRefreshingView = true);
            IsRefreshingView = true;
            CommeByCatsAsyncLoad(new List<Categoria>(Categorias));
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
