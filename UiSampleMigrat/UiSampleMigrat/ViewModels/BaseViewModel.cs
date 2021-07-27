using Android.Graphics;
using Android.Widget;
using System;
using UiSampleMigrat.Helpers;
using UiSampleMigrat.Models;

namespace UiSampleMigrat.ViewModels
{
    public class BaseViewModel : NotificationObject
    {
        protected bool _isEnabled;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                onPropertyChanged();
            }
        }

        protected bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                onPropertyChanged();
            }
        }

        protected bool _isRefreshingView;

        public bool IsRefreshingView
        {
            get { return _isRefreshingView; }
            set
            {
                _isRefreshingView = value;
                onPropertyChanged();
            }
        }

        protected void InvokeOnMainThread(Action a)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(a);
        }

        protected void ErrorToasts(string errmessaahe) {
            Commons.CustomizedToast(Color.White, Color.Black,
            errmessaahe, ToastLength.Long, iconResource: "error64", textSize: 16);
        }
    }
}
