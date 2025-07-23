using System;
using System.Collections; 

namespace Infragistics.Samples.Controls
{ 
    public partial class BrushPaletteSelector : ItemsUserControl  
    {
        public BrushPaletteSelector()
        {
            InitializeComponent();

            this.ItemsSource = this.Resources["BrushPalettes"] as IEnumerable;
        }
        
    }

 }
