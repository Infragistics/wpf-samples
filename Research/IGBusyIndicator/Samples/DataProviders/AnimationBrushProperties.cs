namespace IGBusyIndicator.Samples.DataProviders
{
    public class AnimationBrushProperties
    {
        public string Name { get; private set; }

        public System.Windows.Media.Brush Brush { get; set; }

        public AnimationBrushProperties(string name, System.Windows.Media.Brush brush)
        {
            this.Name = name;
            this.Brush = brush;
        }
    }
}