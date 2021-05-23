using UiSampleMigrat.Models;

namespace UiSampleMigrat.ViewModels
{
    public class BaseViewModel: NotificationObject
    {
        private bool _isEnabled;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value;
                onPropertyChanged();
            }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value;
                onPropertyChanged();
            }
        }

    }
}
