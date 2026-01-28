using System.Threading.Tasks;
using Infragistics.Framework;

namespace Xamarin.Forms
{
    public static class NavigationEx
    {
        public static Color NavigationBackground = Color.FromHex("#FF333333");
         
        /// <summary> 
        /// Asynchronously adds a new <see cref="Page"/> with specified view to the top of the navigation stack
        /// </summary>
        public static Task PushAsync(this INavigation navigation, View view)
        {
            Logs.Trace("navigating to view: " + view.GetType().Name);
            var page = new ContentPage { BackgroundColor = NavigationBackground, Content = view };
            //NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(page, false);
            return navigation.PushAsync(page);
        } 
    }
}