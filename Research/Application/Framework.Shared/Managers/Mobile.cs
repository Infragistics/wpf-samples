using System;
using System.Globalization;
using Xamarin.Forms;

namespace System.Globalization
{
    public static class CultureInfoEx
    {
        public static bool IsJapanese(this CultureInfo culture)
        {
            return culture.Name.ToLower().StartsWith("ja"); 
        }
    }
}

namespace Infragistics.Framework
{
    //TODO consolidate with DeviceEx.cs
    /// <summary>
    /// Represents a manager with useful methods and events about current state of mobile device
    /// </summary>
    public static class Mobile
    {
        static Mobile()
        {
            ScreenDensity = 1;
            //  Orientation = DeviceOrientation.Unknown;
        }
        #region Properties
       
        /// <summary> Gets orientation of mobile device based on size of app page </summary>
        // public static DeviceOrientation Orientation { get; private set; }

        public static TargetIdiom Idiom { get { return Device.Idiom; } }
        //public static TargetPlatform Platform { get { return  Device.OS; } }
          
        public static int ScreenWidth { get; set; }
        public static int ScreenHeight { get; set; }
        public static float ScreenDensity { get; set; }

        public static CultureInfo Culture { get; set; }
        public static bool HasJapaneseCulture
        {
            get { return Culture.IsJapanese(); }
        }

        private static ContentPage _appPage;
        /// <summary> Gets or sets app page of mobile device </summary>
        /// <remarks> Note this property should be set in constructor of an application </remarks>
        public static ContentPage AppPage
        {
            get { return _appPage; }
            set
            {
                if (_appPage == value) return;

                if (_appPage != null)
                {
                    //_appPage.SizeChanged -= OnPageSizeChanged;
                    _appPage.LayoutChanged -= OnPageLayoutChanged;
                }

                _appPage = value;
                UpdateOrientation();
              
                if (_appPage != null)
                {
                    //_appPage.SizeChanged += OnPageSizeChanged; 
                    _appPage.LayoutChanged += OnPageLayoutChanged;
                }
            }
        }

        #endregion
        #region Methods
        /// <summary> Selects one of passed values based on platform of the device </summary> 
        public static T OnPlatform<T>(T iOs, T android, T winPhone)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS: return iOs;
                case Device.Android: return android;
                case Device.UWP: return winPhone;
                default: return iOs;
            } 
        }
        /// <summary> Selects one of passed values based on screen type of the device </summary> 
        public static T OnScreen<T>(T phone, T tablet, T desktop)
        {
            switch (Device.Idiom)
            {
                case TargetIdiom.Phone:
                    return phone;
                case TargetIdiom.Tablet:
                    return tablet;
                case TargetIdiom.Desktop:
                    return desktop;
                default:
                    return phone;
            }
        }

        ///// <summary> Checks if the device is in Landscape orientation </summary>
        //public static bool IsLandscape() { return Orientation == DeviceOrientation.Landscape; }
        ///// <summary> Checks if the device is in Portrait orientation </summary>
        //public static bool IsPortrait() { return Orientation == DeviceOrientation.Portrait; }
       
        private static void UpdateOrientation()
        {
            if (AppPage == null)
            {
                Logs.Trace("Mobile orientation is unknown because app page is not set");
            }
            //var newOrientation = AppPage.Orientation();
            //if (newOrientation != Orientation)
            //{
            //    Orientation = newOrientation;
            //    OnOrientationChanged();
            //}
        }

        #endregion

        #region Events
        //public static event EventHandler<OrientationChangedEventArgs> OrientationChanged;

        public static void OnOrientationChanged()
        {
            //Logs.Trace("Mobile.Orientation changed to: " + Orientation);
      
            //if (OrientationChanged != null)
            //    OrientationChanged(AppPage, new OrientationChangedEventArgs(Orientation));
        }
        private static void OnPageSizeChanged(object sender, EventArgs e)
        {
           // Logs.Trace("Mobile.OnPageSizeChanged   and orientation: " + AppPage.Orientation());
            //UpdateOrientation();
        }
        static void OnPageLayoutChanged(object sender, EventArgs e)
        {
           // Logs.Trace("Mobile.OnPageLayoutChanged and orientation: " + AppPage.Orientation());
            UpdateOrientation();
      
        }
        #endregion
    }
    public class OrientationChangedEventArgs : EventArgs
    {
        //public OrientationChangedEventArgs(DeviceOrientation orientation)
        //{
        //    Orientation = orientation;
        //}

        ///// <summary> Gets or sets PropertyName </summary>
        //public DeviceOrientation Orientation { get; set; }
    }
   
}