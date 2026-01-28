using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows; 

namespace Infragistics.Framework
{
    public class FontFamilyComboBox : ComboBoxBase
    {
        public FontFamilyComboBox()
        {
            this.ItemsSource = new List<string>
            {
                "Roboto Medium",
                "Verdana",
                "Consolas",
                "Bauhaus 93",
                "Bodoni MT",
                "Euphemia",
                "Lucida Calligraphy",
                "Rockwell",
            };
        }
    }

    public class FontWeightComboBox : EnumComboBox
    {
        public FontWeightComboBox()
        {
            this.ItemsSource = new List<FontWeight>
            {
                FontWeights.Normal,
                FontWeights.Bold,
                FontWeights.ExtraBold,
                FontWeights.DemiBold,
                FontWeights.ExtraBlack,
                FontWeights.ExtraLight,
                FontWeights.Heavy,
                FontWeights.Thin,
                FontWeights.UltraLight,
                FontWeights.Medium,
            };
        }
    }

    public class FontStyleComboBox : EnumComboBox
    {
        public FontStyleComboBox()
        {
            this.ItemsSource = new List<FontStyle>
            {
                FontStyles.Normal,
                FontStyles.Italic,
                FontStyles.Oblique
            };
        }
    }

    public class FontStretchComboBox : EnumComboBox
    {
        public FontStretchComboBox()
        {
            this.ItemsSource = new List<FontStretch>
            {
                FontStretches.Normal,
                FontStretches.ExtraExpanded,
                FontStretches.Expanded,
                FontStretches.ExtraCondensed,
                FontStretches.UltraCondensed,
                FontStretches.Condensed,
            };
        }
    }

}
