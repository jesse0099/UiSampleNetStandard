using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using UiSampleMigrat.Models;
using Xamarin.Forms;

namespace UiSampleMigrat.ViewModels
{
    public class TestPageViewModel: NotificationObject
    {
        #region Propiedades
        private ObservableCollection<String> lstOpt;

        public ObservableCollection<String> LstOpt 
        {
            get { return lstOpt; }
            set {
                lstOpt = value;
                onPropertyChanged();
            }
        }

        private string _selectedOpt;

        public string SelectedOpt
        {
            get { return _selectedOpt; }
            set {
                _selectedOpt = value;
                onPropertyChanged();
            }
        }

        private ICommand _slideCommand;

        public ICommand SlideCommand
        {
            get { return _slideCommand; }
            set {
                _slideCommand = value;
                onPropertyChanged();
            }
        }

        private string _contentText;

        public string ContentText
        {
            get { return _contentText; }
            set {
                _contentText = value;
                onPropertyChanged();
            }
        }


        #endregion

        #region Constructor
        public TestPageViewModel()
        {
            _instance = this;
            this._slideCommand = new Command(SlideCommandAction);
            this.LstOpt = new ObservableCollection<string>() { "Opt_1","Opt_2"};
            PropertyChanged += OnTestViewModelPropertyChanged;
        }
        #endregion

        #region Singleton
        public static TestPageViewModel _instance { get; set; }
        public static TestPageViewModel GetInstance()
        {
            if (_instance == null)
                return new TestPageViewModel();
            else
                return _instance;
        }
        #endregion

        #region Metodos

        public void SlideCommandAction() {
           MessagingCenter.Send(this, "toggleDrawer");
        }

        private void OnTestViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(SelectedOpt)) {
                if (SelectedOpt != null)
                {
                    //Modificar el contentview con alguna propiedad
                    if (SelectedOpt.Equals("Opt_1"))
                    {
                        this.ContentText = "Opt_1"; 
                    }
                    else
                    {
                        this.ContentText = "Opt_2";
                    }
                }
            }
        }

        #endregion
    }
}
