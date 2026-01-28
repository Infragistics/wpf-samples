 

using System;
using System.Windows;
using Infragistics.Controls.Charts;

namespace Infragistics.Samples.Framework
{
    public static class SampleContainerEx
    { 
        /// <summary>
        /// Synchronizes scale of chart based on size change of a sample container
        /// </summary> 
        public static void SynchronizeScale(this SampleContainer sample, XamScatterSurface3D chart)
        {
            sample.SizeChanged += (s, e) =>
            {
                var min = Math.Min(e.NewSize.Height, e.NewSize.Width);
                var max = Math.Max(e.NewSize.Height, e.NewSize.Width);

                if (chart == null) return;
                
                var scale = (min / max);
                chart.Scale = scale;
            };
        } 
    }
}