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
            Categorias = new ObservableCollection<Categoria>();
            RefreshCommeCommand = new Xamarin.Forms.Command(RefreshCommeExecute);
            CatsAndCommeAsync();
        }

        #region Metodos
        private async Task<List<Categoria>> CatsAsyncLoad()
        {

            try
            {
                Dao = new CategoriaDao();
                Categorias = new ObservableCollection<Categoria>(await ((CategoriaDao)Dao).GetList());
            }
            catch (ConnectionException Cex)
            {
                ErrorToasts(Cex.Message);
            }
            catch (CategoryException Caex)
            {
                ErrorToasts(Caex.Message);
            }

            return new List<Categoria>(Categorias);
        }

        private async Task<List<Enterprise>> CommeByCatsAsyncLoad(List<Categoria> cats)
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
            return new List<Enterprise>(Comercios);
        }

        private async void CatsAndCommeAsync()
        {
            var catsTask = CatsAsyncLoad();
            var commeByCatsTask = CommeByCatsAsyncLoad(await catsTask);
            await commeByCatsTask;
            IsRefreshingView = false;
        }

        #endregion

        #region Comandos
        //Refrescar empresas - segun categorias seleccionadas
        public async void RefreshCommeExecute()
        {
            await CommeByCatsAsyncLoad(new List<Categoria>(Categorias));
            IsRefreshingView = false;
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
