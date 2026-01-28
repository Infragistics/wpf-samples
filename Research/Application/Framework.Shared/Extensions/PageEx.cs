
using System;
using System.Collections.Generic;

namespace Xamarin.Forms
{
    public static class PageEx
    {
        /// <summary> Gets orientation of the page based on its size  </summary>
        public static DeviceOrientation Orientation(this Page page)
        {
            var orientation = DeviceOrientation.Unknown;
            if (page == null) return orientation;

            if (page.Width > page.Height)
                orientation = DeviceOrientation.Landscape;
            else if (page.Width < page.Height)
                orientation = DeviceOrientation.Portrait;

            return orientation;
        }
        /// <summary> Checks if the page is in Landscape orientation </summary>
        public static bool IsLandscape(this Page page)
        {
            return page.Orientation() == DeviceOrientation.Landscape;
        }
        /// <summary> Checks if the page is in Portrait orientation </summary>
        public static bool IsPortrait(this Page page)
        {
            return page.Orientation() == DeviceOrientation.Portrait;
        }
    }
    /// <summary>
    /// An enumeration defining the orientations that may be supported by an application.
    /// </summary>
    public enum DeviceOrientation
    {
        /// <summary> Specifies unknown orientation of a device that does not support orientation changes </summary>
        Unknown = 0,
        /// <summary> Specifies portrait orientation of a device (width is greater than its height) </summary>
        Portrait = 1,
        /// <summary> Specifies landscape orientation of a device (height is greater than its width) </summary>
        Landscape = 2,
    }

    //TODO move to ViewEx.cs
    public static class ViewEx
    {
        public static void Add(this List<View> views, Command command, int taps = 1)
        {
            foreach (var view in views)
            {
                view.Add(command, taps);
            }
        }
        public static void Add(this View view, Command command, int taps = 1)
        {
            view.GestureRecognizers.Add(
               new TapGestureRecognizer
               {
                   NumberOfTapsRequired = taps,
                   Command = command
               }); 
        } 
        public static void Remove(this View view, Command command)
        {
            for (int i = 0; i < view.GestureRecognizers.Count; i++)
            {
                var gesture = view.GestureRecognizers[i] as TapGestureRecognizer;
                if (gesture != null && gesture.Command == command)
                {
                    view.GestureRecognizers.RemoveAt(i);
                    break;
                }
            }
        }
    }
}