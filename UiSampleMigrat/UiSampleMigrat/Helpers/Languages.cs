using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiSampleMigrat.Interfaces;
using Xamarin.Forms;
using UiSampleMigrat.Resources;

namespace UiSampleMigrat.Models
{
    public class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Test => Resource.Test;
        public static string PasswordPH => Resource.passwordPH;
        public static string PermissionsRequired => Resource.permissionRequired;

        public static string UpdatedProfile => Resource.updatedProfile;
        public static string AllDataNeeded => Resource.alldataNeeded;
        public static string WronWGivenNames => Resource. wrongWrittenGivenNames;
        public static string WronWGNames => Resource.wrongWrittenNames;
        public static string PasswordsShouldMatch => Resource.passwordsshouldmatch;
        public static string PasswordsDontMatch => Resource.PasswordsNomatch_;



    }
}