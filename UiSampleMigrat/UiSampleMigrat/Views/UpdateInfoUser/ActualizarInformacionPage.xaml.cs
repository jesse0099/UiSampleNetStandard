using Android.Widget;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiSampleMigrat.Effects;
using UiSampleMigrat.Models;
using UiSampleMigrat.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace UiSampleMigrat.Views.UpdateInfoUser
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActualizarInformacion : ContentPage
    {
        public ActualizarInformacion()
        {
            InitializeComponent();
            aicImageLoad.Color = Color.FromHex("#3699de");
            Bussy(false);
        }

        private void Bussy(bool state) {
            aicImageLoad.IsVisible = state;
            aicImageLoad.IsRunning = state;
        }

        private bool AndPermissionChecker(List<PermissionStatus> inputs) {
           var  returned = inputs.TrueForAll((x)=> x==PermissionStatus.Granted);
           return returned;
        }

        private async void BtnImg_Tapped(object sender, EventArgs e)
        {
            var cameraPermission = await Permissions.CheckStatusAsync<Permissions.Camera>();
            var storagePermission = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            var storageReadPermission =await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if (!AndPermissionChecker(new List<PermissionStatus>() { cameraPermission, storagePermission, storageReadPermission }))
            {
                //Permisos denegados,pedir permisos
                var askCamePermission = await  Permissions.RequestAsync<Permissions.Camera>();
                var askStrgPermission = await Permissions.RequestAsync<Permissions.StorageWrite>();
                var askStrgReadPermission =await  Permissions.RequestAsync<Permissions.StorageWrite>();
                if (AndPermissionChecker(new List<PermissionStatus>() { askCamePermission, askStrgPermission, askStrgReadPermission }))
                {
                    await ChangeProfileImage();
                }
                else
                {
                    Toast.MakeText(Android.App.Application.Context,Languages.PermissionsRequired, ToastLength.Long).Show();
                }

            }
            else
            {
                  await ChangeProfileImage();
            }
        }

        private async Task<bool> ChangeProfileImage() {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsTakePhotoSupported && !CrossMedia.Current.IsPickPhotoSupported )
            {
                return false;
            }
            else
            {
                Bussy(true);
                await Task.Delay(1000);
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Images",
                    Name = DateTime.Now.ToString() + "_new.jpg"
                });
                if (file == null)
                {
                    Bussy(false);
                    return false;
                }

                UpdateProfileViewModel.GetInstance().Profile.ProfileImage = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });

                imgProfile.Source = UpdateProfileViewModel.GetInstance().Profile.ProfileImage;

                Bussy(false);
                return true;
            }
        }

        void Handle_tapped(object sender,EventArgs e) {
            foreach (var item in gridMaster.Children)
            {
                if (TooltipEffect.GetHasTooltip(item)) {
                    TooltipEffect.SetHasTooltip(item,false);
                    TooltipEffect.SetHasTooltip(item,true);
                }
            }
        }

    }
}