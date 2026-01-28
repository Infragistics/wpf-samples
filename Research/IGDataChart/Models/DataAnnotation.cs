using System;
using System.Windows;
using Infragistics.Samples.Shared.Models;

namespace IGDataChart.Models
{
    public class DataAnnotation : ObservableModel
    {
        // optional for customizing label on DataAnnotationStripLayer, DataAnnotationLineLayer, DataAnnotationRectLayer, DataAnnotationBandLayer
        public string StartLabel { get; set; }

        // optional for customizing label on DataAnnotationStripLayer, DataAnnotationLineLayer, DataAnnotationRectLayer, DataAnnotationBandLayer
        public string EndLabel { get; set; }

        // optional for overlaying text in plot area of Data Annotation layer's OverlayTextMemberPath 
        public string Overlay { get; set; }

        // optional for displaying label on DataAnnotationSliceLayer
        public string Label { get; set; }

        public double Index { get; set; }

        // required for DataAnnotationLineLayer, DataAnnotationRectLayer, DataAnnotationBandLayer
        public double StartY { get; set; }

        // required for DataAnnotationLineLayer, DataAnnotationRectLayer, DataAnnotationBandLayer
        public double EndY { get; set; }

        // required for DataAnnotationLineLayer, DataAnnotationRectLayer, DataAnnotationBandLayer
        public double StartX { get; set; }

        // required for DataAnnotationLineLayer, DataAnnotationRectLayer, DataAnnotationBandLayer
        public double EndX { get; set; }

        // required for DataAnnotationSliceLayer 
        public double Value { get; set; }

        // required for DataAnnotationStripLayer 
        public double Start { get; set; }

        // required for DataAnnotationStripLayer 
        public double End { get; set; }

    }

}