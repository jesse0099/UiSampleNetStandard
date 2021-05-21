using System;
using System.IO;
using Xamarin.Forms;

namespace UiSampleMigrat.Helpers
{
    public static class Commons
    {
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
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
