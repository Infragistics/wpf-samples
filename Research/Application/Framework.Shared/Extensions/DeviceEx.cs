namespace Xamarin.Forms
{
    public static class DeviceEx
    {
        public static T OnPlatform<T>(T iOs, T android, T winPhone)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:      return iOs;
                case Device.Android:  return android;
                case Device.UWP: return winPhone;
                default: return iOs;
            }
        }
        public static T OnScreen<T>(T phone, T tablet, T desktop)
        {
            switch (Device.Idiom)
            {
                case TargetIdiom.Phone:   return phone;
                case TargetIdiom.Tablet:  return tablet;
                case TargetIdiom.Desktop: return desktop;
                default:  return phone;
            }
        }
    }
}