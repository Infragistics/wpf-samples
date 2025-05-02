using System;
using System.Windows;
using Infragistics.Samples.Shared.Models;

namespace IGDataChart.Models
{
    public class DataAnnotation : ObservableModel
    {
        // optional for customzing labe on DataAnnotationStripLayer, DataAnnotationLineLayer, DataAnnotationRectLayer, DataAnnotationBandLayer
        public string StartLabel { get; set; }
        
        // optional for customzing labe on DataAnnotationStripLayer, DataAnnotationLineLayer, DataAnnotationRectLayer, DataAnnotationBandLayer
        public string EndLabel { get; set; }

        // optional for displaying OverlayText using OverlayTextMemberPath
        //public string Overlay { get; set; }

        // optional for displaying OverlayText using OverlayTextMemberPath
        public string Label { get; set; }

        public double Index { get; set; }

        // required for DataAnnotationLineLayer, DataAnnotationRectLayer, DataAnnotationBandLayer
        private double _StartY;
        public double StartY
        {
            get { return _StartY; }
            set { if (value == _StartY) return; _StartY = value; OnPropertyChanged(); }
        }
        // required for DataAnnotationLineLayer, DataAnnotationRectLayer, DataAnnotationBandLayer
        private double _EndY;
        public double EndY
        {
            get { return _EndY; }
            set { if (value == _EndY) return; _EndY = value; OnPropertyChanged(); }
        }
        // required for DataAnnotationLineLayer, DataAnnotationRectLayer, DataAnnotationBandLayer
        private double _StartX;
        public double StartX
        {
            get { return _StartX; }
            set { if (value == _StartX) return; _StartX = value; OnPropertyChanged(); }
        }
        // required for DataAnnotationLineLayer, DataAnnotationRectLayer, DataAnnotationBandLayer
        private double _EndX;
        public double EndX
        {
            get { return _EndX; }
            set { if (value == _EndX) return; _EndX = value; OnPropertyChanged(); }
        }

        // required for DataAnnotationSliceLayer
        private double _Value;
        public double Value
        {
            get { return _Value; }
            set { if (value == _Value) return; _Value = value; OnPropertyChanged(); }
        }

        // required for DataAnnotationStripLayer
        private double _Start;
        public double Start
        {
            get { return _Start; }
            set { if (value == _Start) return; _Start = value; OnPropertyChanged(); }
        }

        // required for DataAnnotationStripLayer
        private double _End;
        public double End
        {
            get { return _End; }
            set { if (value == _End) return; _End = value; OnPropertyChanged(); }
        }

        //public double Center { get { return Start + ((End - Start) / 2.0); } }

    }

}