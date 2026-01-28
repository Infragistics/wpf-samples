using System;
using System.Collections; 

namespace Infragistics.Samples.Controls
{ 
    public partial class ColorsPaletteSelector : ItemsUserControl  
    {
        public ColorsPaletteSelector()
        {
            InitializeComponent();

            this.ItemsSource = this.Resources["ColorPalettes"] as IEnumerable;
        }
        
    }

 }
