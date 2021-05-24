using Android.Graphics;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UiSampleMigrat.Helpers
{
    public static class Commons
    {
        public static string IdsWrapper(List<int> ids, string objectName) {
            try
            {
                int i = 0;
                foreach (var item in ids)
                {
                    if(i>0)
                        objectName = $"{objectName}&{objectName}[{i}]={item}";
                    else
                        objectName = $"{objectName}[{i}]={item}";
                    i++;
                }

                return objectName;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ImageSource ObjectToImageSource(object obj) {
            try
            {
                var profileImageBytes = Convert.FromBase64String(obj.ToString());
                ImageSource profileImage;
                if (profileImageBytes.Length != 0)
                    profileImage = ImageSource.FromStream(() => new MemoryStream(profileImageBytes));
                else
                    profileImage = ImageSource.FromFile("userF.png");
                return profileImage;
            }
            catch (Exception) {
                throw;
            }
        }

        public static byte[] StreamToByteArray(Stream input)
        {
            try
            {
                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    return ms.ToArray();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static Stream GetImageSourceStream(ImageSource imgSource)
        {
            if (imgSource is StreamImageSource)
            {
                try
                {
                    StreamImageSource strImgSource = (StreamImageSource)imgSource;
                    System.Threading.CancellationToken cToken = System.Threading.CancellationToken.None;
                    Task<Stream> returned = strImgSource.Stream(cToken);
                    return returned.Result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return null;
        }

        public static void CustomizedToast(Android.Graphics.Color textColor, Android.Graphics.Color backgroundColor, string message, ToastLength length = ToastLength.Long,
            string iconResource = "Elec", float textSize = 16, string resourceFolder = "drawable")
        {

            int resourceId = Android.App.Application.Context.Resources.GetIdentifier(iconResource, resourceFolder, Android.App.Application.Context.PackageName);
            var toast = Toast.MakeText(Android.App.Application.Context, message, length);
            var v = (Android.Views.ViewGroup)toast.View;
            if (v.ChildCount > 0 && v.GetChildAt(0) is TextView)
            {
                TextView tv = (TextView)v.GetChildAt(0);
                tv.SetTextColor(textColor);
                tv.SetCompoundDrawablesRelativeWithIntrinsicBounds(resourceId, 0, 0, 0);
                tv.SetTextSize(Android.Util.ComplexUnitType.Sp, textSize);
            }
            Android.Graphics.Color c = backgroundColor;
            ColorMatrixColorFilter CM = new ColorMatrixColorFilter(new float[]
                {
                        0,0,0,0,c.R,
                        0,0,0,0,c.G,
                        0,0,0,0,c.B,
                        0,0,0,1,0
                });
            toast.View.Background.SetColorFilter(CM);
            toast.Show();
        }
    }
}
