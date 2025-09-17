using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace IGDataChart.Custom.AnnotationLayers
{
     public class HighlightTypes
        : List<string>
    {
        public HighlightTypes()
        {
            Add("Auto");
            Add("Marker");
            Add("Shape");
        }
    }
    }
