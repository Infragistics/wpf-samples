using System.Windows;
using System.Windows.Media;
using Infragistics.Controls.Charts;

namespace IGDataChart.Samples.Display.Series
{
    public partial class CustomSeriesHighlighting : Infragistics.Samples.Framework.SampleContainer
    {
        public CustomSeriesHighlighting()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void CategorySeries_OnAssigningCategoryStyle(object sender, AssigningCategoryStyleEventArgs args)
        {
            double minOpacity = .3, opacity = 1.0;
            if (args.SumAllSeriesHighlightingProgress > 0.0)
            {
                var progress = 0.0;
                if (args.HighlightingInfo != null)
                {
                    progress = args.HighlightingInfo.Progress;
                }

                progress = progress - args.SumAllSeriesHighlightingProgress;

                opacity = minOpacity + (1.0 + progress) * (1.0 - minOpacity);
                args.Opacity = opacity;
                args.HighlightingHandled = true;
                
                for (var i = 0; i < this.DataChart.Series.Count; i++)
                {
                    var curr = this.DataChart.Series[i];
                    var series = sender as Infragistics.Controls.Charts.Series;
                    if (series != null && series.Name != curr.Name)
                    {
                        curr.NotifyVisualPropertiesChanged();
                    }
                }
            }
        }
    }
}
