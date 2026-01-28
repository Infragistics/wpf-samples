using Xamarin.Forms;

namespace Infragistics.Framework
{
    /// <summary>
    /// Represents a control that switches between Landscape and Portrait content views 
    /// depending on orientation of mobile device
    /// </summary>
    public class OrientationView : ContentView
    {
        public OrientationView()
        {
            //Mobile.OrientationChanged += OnMobileOrientationChanged;
        }
        public View Landscape { get; set; }
        public View Portrait { get; set; }

        void OnMobileOrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            //Logs.Trace("Content changing to " + e.Orientation);
                
            //if (e.Orientation == DeviceOrientation.Landscape)
            //{
            //    this.Content = Landscape;
            //}
            //else if (e.Orientation == DeviceOrientation.Portrait)
            //{
            //    this.Content = Portrait;
            //}
            //else
            //{
            //    // for unknown orientation default to Landscape view for tablets 
            //    // and Portrait view for phones
            //    this.Content = Mobile.Idiom == TargetIdiom.Tablet ? Landscape : Portrait;
            //}
            this.Content = Landscape;

            //Logs.Trace("Content changing to " + e.Orientation + " done");
        }
    }
}