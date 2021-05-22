using Android.Widget;
using System;
using System.Collections.Generic;
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
                    await TakePhotoAsync();
                }
                else
                {
                    Toast.MakeText(Android.App.Application.Context,Languages.PermissionsRequired, ToastLength.Long).Show();
                }

            }
            else
            {
                  await TakePhotoAsync();
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

        async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(photo);
              
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is now supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                return;
            }
            await Task.Run(()=> {
                UpdateProfileViewModel.GetInstance().Profile.ProfileImage = ImageSource.FromStream(() => {
                    return photo.OpenReadAsync().Result;
                });
            });
            imgProfile.Source = UpdateProfileViewModel.GetInstance().Profile.ProfileImage;
        }

    }
}