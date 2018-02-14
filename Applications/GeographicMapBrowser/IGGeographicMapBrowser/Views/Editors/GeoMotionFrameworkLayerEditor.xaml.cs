namespace IGShowcase.GeographicMapBrowser.Views
{
    public partial class GeoMotionFrameworkLayerEditor : GeoMapLayerEditor  
    {
        public GeoMotionFrameworkLayerEditor()
        {
            InitializeComponent();
        }

        private void MotionTimeSlider_ThumbValueChanged(object sender, Infragistics.Controls.Editors.ThumbValueChangedEventArgs<System.DateTime> e)
        {
            var min = this.MotionTimeSlider.MinValue;
            var max = this.MotionTimeSlider.MaxValue;
            var value = e.NewValue;
        }
    }
}
