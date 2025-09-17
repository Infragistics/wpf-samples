using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Infragistics.Samples.Controls
{
    public class BrushPaletteViewer2 : ItemsControl
    {
        public BrushPaletteViewer2()
        { 
            string stylePath = "/Infragistics.Samples.Services;component/Controls/BrushPaletteViewer.xaml";
            
            this.DefaultStyleKey = typeof(BrushPaletteViewer);
            
            // Add the default style to the control's resources
            ResourceDictionary style = new ResourceDictionary();
            style.Source = new System.Uri(stylePath, System.UriKind.Relative);
            this.Resources.MergedDictionaries.Add(style);
        }
    }
	

    
 
}