using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiSampleMigrat.Models;

namespace UiSampleMigrat.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants
        //lSUID = last session UID
        private const string clientUID = "ClientUID";
        private const string serializedToken = "SerializedToken";
        private const string logguedInfo = "LogguedInfo";
        private const string fullName = "FullName";
        private const string isRemenbered = "IsRemembered";
        private const string succesfullPassword = "SuccesfullPassword";
        private const string lSUID = "LSUID";  
        private static readonly string stringDefault = string.Empty;
        private static readonly bool boolDefault = false;
        private static readonly int intDefault = -1;


        #endregion
        public static string SuccesfullPassword{
            get {
                return AppSettings.GetValueOrDefault(succesfullPassword,stringDefault);
            }
            set{
                AppSettings.AddOrUpdateValue(succesfullPassword, value);
            }
        }

        public static int LSUID {
            get {
                return AppSettings.GetValueOrDefault(lSUID,intDefault);
            }
            set {
                 AppSettings.AddOrUpdateValue(lSUID, value);
            }

        }

        public static int ClientUID {
            get {
                return AppSettings.GetValueOrDefault(clientUID,intDefault);
            }

            set {
                AppSettings.AddOrUpdateValue(clientUID,value);
            }

        }

        public static string SerializedToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(serializedToken, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(serializedToken, value);
            }
        }

        public static bool IsRemembered
        {
            get
            {
                return AppSettings.GetValueOrDefault(isRemenbered, boolDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(isRemenbered, value);
            }
        }

        public static string FullName
        {
            get
            {
                return AppSettings.GetValueOrDefault(fullName, stringDefault);
            }

            set
            {
                AppSettings.AddOrUpdateValue(fullName, value);
            }
        }

        //Limpiar todas las configuraciones de la aplicacion
        public static void AppSettingsClear()
        {
            AppSettings.Clear();
        }

    }
}