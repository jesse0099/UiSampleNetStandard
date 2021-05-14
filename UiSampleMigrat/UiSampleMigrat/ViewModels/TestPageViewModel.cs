using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UiSampleMigrat.Models;

namespace UiSampleMigrat.ViewModels
{
    public class TestPageViewModel: NotificationObject
    {
        #region Propiedades
        public ObservableCollection<String> LstOpt { get; set; }
        #endregion

        #region Constructor
        public TestPageViewModel()
        {
            _instance = this;
            this.LstOpt = new ObservableCollection<string>() { "Opt_1","Opt_2"};
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
    }
}
